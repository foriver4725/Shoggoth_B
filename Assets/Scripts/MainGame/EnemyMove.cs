using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;

namespace MainGame
{
    public class EnemyMove : MonoBehaviour
    {
        // 敵の向き
        DIR lookingDir = DIR.DOWN;

        // プレイヤーが整数座標まで移動完了しているか
        bool IsStepEnded = true;

        // 敵が徘徊ポイント上にいるか（trueになったら、1回だけ目的座標を更新する。）
        bool isAtStokingPosition = true;
        Vector2Int targetPos = new();

        // プレイヤーを追いかけるモードになっているか
        bool isChasing = false;
        float stopChaseTime = 0;

        void Update()
        {
            // プレイヤーに近づいたら追跡モードになる。
            if ((GameManager.Instance.Player.transform.position - transform.position).sqrMagnitude <= SO_Player.Entity.EnemyChaseRange * SO_Player.Entity.EnemyChaseRange)
            {
                isChasing = true;
            }

            if (isChasing)
            {
                if ((GameManager.Instance.Player.transform.position - transform.position).sqrMagnitude > SO_Player.Entity.EnemyStopChaseRange * SO_Player.Entity.EnemyStopChaseRange)
                {
                    stopChaseTime += Time.deltaTime;
                }
                else
                {
                    stopChaseTime = 0;
                }

                if (stopChaseTime >= SO_Player.Entity.EnemyStopChaseDuration)
                {
                    stopChaseTime = 0;
                    isChasing = false;
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

                // 必ず向いている方向の次の整数座標まで移動する。
                if (moveDir != Vector2.zero)
                {
                    IsStepEnded = false;
                    StartCoroutine(MoveTo(lookingDir.ToVector3()));
                }
            }
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
                transform.position += SO_Player.Entity.EnemySpeed * Time.deltaTime * dir;
                if ((transform.position - fromPos).sqrMagnitude >= 1)
                    break;
                yield return null;
            }

            transform.position = toPos;
            isAtStokingPosition = GameManager.Instance.EnemyStokingPositions.Contains(toPos.ToVec2I());
            IsStepEnded = true;
        }

        void SelectNewStokingPoint()
        {
            int posNum = GameManager.Instance.EnemyStokingPositions.Count;
            int nextIndex = Random.Range(0, posNum);

            int i = 0;
            foreach (Vector2Int pos in GameManager.Instance.EnemyStokingPositions)
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
    }
}
