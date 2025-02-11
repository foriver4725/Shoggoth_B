﻿using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace MainGame
{
    public class EnemyMove : MonoBehaviour
    {
        public enum FLOOR { F1, BF1, BF2 };
        [SerializeField] private Animator anim;
        [SerializeField, Header("どの階層の敵か")] private FLOOR _floor;

        private HashSet<Vector2Int> _stokingPos;

        // 敵の向き
        DIR lookingDir = DIR.DOWN;

        // プレイヤーが整数座標まで移動完了しているか
        bool IsStepEnded = true;

        // 敵が徘徊ポイント上にいるか（trueになったら、1回だけ目的座標を更新する。）
        bool isAtStokingPosition = true;
        Vector2Int targetPos = new();

        public bool IsChasing { get; set; } = false; // 現在のフレームで発覚状態であるか
        public bool IsOnChase { get; set; } = false; // このフレームで発覚状態になったか
        public bool IsOffChase { get; set; } = false; // このフレームで発覚状態でなくなったか
        bool isChasingPre = false; // 1つ前のフレームで発覚状態であったか
        public float StopChaseTime { get; set; } = 0;

        private void Start()
        {
            int stokingPosIndex = _floor switch
            {
                FLOOR.F1 => 0,
                FLOOR.BF1 => 1,
                FLOOR.BF2 => 2,
                _ => 0
            };
            _stokingPos = GameManager.Instance.EnemyStokingPositions[stokingPosIndex];
        }

        private bool isRaised => GameManager.Instance.EventState is EventState.ShoggothRaise or EventState.LastShoggoth or EventState.End;

        void Update()
        {
            // クリアまたはゲームオーバーなら動かない
            if (GameManager.Instance.EventState == EventState.End) return;

            // プレイヤーに近づいたら追跡モードになる。
            float chaseRange = SO_Player.Entity.EnemyChaseRange;
            if (isRaised) chaseRange *= 2; // レイズ時は追跡範囲を2倍にする
            if (!IsChasing && ((Vector2)GameManager.Instance.Player.transform.position - (Vector2)transform.position).sqrMagnitude <= chaseRange * chaseRange)
            {
                IsChasing = true;
            }

            if (IsChasing)
            {
                float stopChaseRange = SO_Player.Entity.EnemyStopChaseRange;
                if (isRaised) stopChaseRange *= 2; // レイズ時は追跡停止範囲を2倍にする
                if (((Vector2)GameManager.Instance.Player.transform.position - (Vector2)transform.position).sqrMagnitude > stopChaseRange * stopChaseRange)
                {
                    StopChaseTime += Time.deltaTime;
                }
                else
                {
                    StopChaseTime = 0;
                }

                if (StopChaseTime >= SO_Player.Entity.EnemyStopChaseDuration)
                {
                    StopChaseTime = 0;
                    IsChasing = false;

                    SelectNewStokingPoint();
                }
                else
                {
                    targetPos = GameManager.Instance.Player.transform.position.ToVec2I();
                }
            }
            else if (isAtStokingPosition)
            {
                SelectNewStokingPoint();
                isAtStokingPosition = false;
            }

            if (IsStepEnded)
            {
                List<Vector2Int> pathPositionsToPlayer = Ex.AStar.Pathfinding.FindPath
                        (
                        transform.position.ToVec2I(),
                        targetPos,
                        GameManager.Instance.PathPositions
                        );
                Vector2 moveDir = Vector2.zero;
                if (pathPositionsToPlayer != null && pathPositionsToPlayer.Count > 1)
                {
                    Vector3 _moveDir = pathPositionsToPlayer[1].ToVec3() - transform.position;
                    moveDir = new(_moveDir.x, _moveDir.y);
                }

                // （動いているなら）向いている方向を判断する。
                if (moveDir != Vector2.zero)
                {
                    lookingDir = moveDir.ToDir();
                }

                // アニメーションの遷移を発火させる。
                anim.SetInteger("LookingDirection", (int)lookingDir);

                // 必ず向いている方向の次の整数座標まで移動する。
                if (moveDir != Vector2.zero)
                {
                    IsStepEnded = false;
                    StartCoroutine(MoveTo(lookingDir.ToVector3()));
                }
            }
        }

        private void LateUpdate()
        {
            IsOnChase = IsChasing && !isChasingPre;
            IsOffChase = !IsChasing && isChasingPre;
            isChasingPre = IsChasing;
        }

        IEnumerator MoveTo(Vector3 dir)
        {
            Vector3 fromPos = transform.position;
            Vector3 toPos = fromPos + dir;

            // pathの所しか動けないので、目的地がpathでなかったら移動の処理を行わない。
            if (!GameManager.Instance.PathPositions.Contains(new Vector2Int((int)toPos.x, (int)toPos.y)))
            {
                IsStepEnded = true;
                yield break;
            }

            while (true)
            {
                float speed = _floor switch
                {
                    FLOOR.F1 => SO_Player.Entity.EnemySpeed1F,
                    FLOOR.BF1 => SO_Player.Entity.EnemySpeedB1F,
                    FLOOR.BF2 => SO_Player.Entity.EnemySpeedB2F,
                    _ => 0
                };
                if (isRaised) speed *= 0.5f; // レイズ時は移動速度を半分にする
                transform.position += speed * Time.deltaTime * dir;

                if ((transform.position - fromPos).sqrMagnitude >= 1)
                    break;
                yield return null;
            }

            transform.position = toPos;
            isAtStokingPosition = _stokingPos.Contains(toPos.ToVec2I());
            IsStepEnded = true;
        }

        public void SelectNewStokingPoint()
        {
            int posNum = _stokingPos.Count;
            int nextIndex = UnityEngine.Random.Range(0, posNum);

            int i = 0;
            foreach (Vector2Int pos in _stokingPos)
            {
                Vector2Int preTargetPos = targetPos;
                targetPos = pos;
                if (i == nextIndex)
                {
                    if (targetPos != transform.position.ToVec2I())
                    {
                        break;
                    }
                    else
                    {
                        targetPos = preTargetPos;
                        break;
                    }
                }
                i++;
            }
        }

        public async UniTaskVoid ChangeFloorThenDo(FLOOR floor, Action action, CancellationToken ct)
        {
            await UniTask.WaitUntil(() => IsStepEnded, cancellationToken: ct);

            _floor = floor;

            int stokingPosIndex = _floor switch
            {
                FLOOR.F1 => 0,
                FLOOR.BF1 => 1,
                FLOOR.BF2 => 2,
                _ => 0
            };
            _stokingPos = GameManager.Instance.EnemyStokingPositions[stokingPosIndex];

            action?.Invoke();
        }
    }
}
