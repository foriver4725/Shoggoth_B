using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;

namespace MainGame
{
    public class EnemyMove : MonoBehaviour
    {
        private enum FLOOR { F1, BF1, BF2 };
        [SerializeField, Header("どの階層の敵か")] private FLOOR _floor;

        [SerializeField, Header("敵の移動速度[m/s]")] private float _enemySpeed;

        private HashSet<Vector2Int> _stokingPos;

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

        // チェイスBGM
        private AudioSource _chaseBGMObj = null;
        //private Coroutine _bgmChange = null;

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

        void Update()
        {
            // プレイヤーに近づいたら追跡モードになる。
            if ((GameManager.Instance.Player.transform.position - transform.position).sqrMagnitude <= SO_Player.Entity.EnemyChaseRange * SO_Player.Entity.EnemyChaseRange)
            {
                isChasing = true;

                //if (_bgmChange != null) StopCoroutine(_bgmChange);
                //_bgmChange = null;
                if (_chaseBGMObj == null) _chaseBGMObj = Soundtest.bgmOn(SO_Sound.Entity.ChaseBGM);
                _chaseBGMObj.Play();
                //_bgmChange = StartCoroutine(SoundVolumeChange(_chaseBGMObj, 0, 1, 1, false));
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

                    //if (_bgmChange != null) StopCoroutine(_bgmChange);
                    //_bgmChange = null;
                    _chaseBGMObj.Stop();
                    Destroy(_chaseBGMObj.gameObject);
                    //_bgmChange = StartCoroutine(SoundVolumeChange(_chaseBGMObj, _chaseBGMObj.volume, 0, 1, true));

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
                transform.position += _enemySpeed * Time.deltaTime * dir;
                if ((transform.position - fromPos).sqrMagnitude >= 1)
                    break;
                yield return null;
            }

            transform.position = toPos;
            isAtStokingPosition = _stokingPos.Contains(toPos.ToVec2I());
            IsStepEnded = true;
        }

        //IEnumerator SoundVolumeChange(AudioSource source, float start, float end, float duration, bool isDestroyoOnEnd = false)
        //{
        //    float t = 0;
        //    while (t < duration)
        //    {
        //        t += Time.deltaTime;

        //        float volume = t * (end - start) / duration + start;
        //        source.volume = volume;

        //        yield return null;
        //    }

        //    if (isDestroyoOnEnd) Destroy(source.gameObject);
        //}

        void SelectNewStokingPoint()
        {
            int posNum = _stokingPos.Count;
            int nextIndex = Random.Range(0, posNum);

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
    }
}
