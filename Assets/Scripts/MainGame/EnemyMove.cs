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
        [SerializeField, Header("�ǂ̊K�w�̓G��")] private FLOOR _floor;

        [SerializeField, Header("�G�̈ړ����x[m/s]")] private float _enemySpeed;

        private HashSet<Vector2Int> _stokingPos;

        // �G�̌���
        DIR lookingDir = DIR.DOWN;

        // �v���C���[���������W�܂ňړ��������Ă��邩
        bool IsStepEnded = true;

        // �G���p�j�|�C���g��ɂ��邩�itrue�ɂȂ�����A1�񂾂��ړI���W���X�V����B�j
        bool isAtStokingPosition = true;
        Vector2Int targetPos = new();

        // �v���C���[��ǂ������郂�[�h�ɂȂ��Ă��邩
        public bool IsChasing { get; set; } = false;
        public float StopChaseTime { get; set; } = 0;

        // �`�F�C�XBGM
        public AudioSource ChaseAS;

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
            // �v���C���[�ɋ߂Â�����ǐՃ��[�h�ɂȂ�B
            if (!IsChasing && (GameManager.Instance.Player.transform.position - transform.position).sqrMagnitude <= SO_Player.Entity.EnemyChaseRange * SO_Player.Entity.EnemyChaseRange)
            {
                IsChasing = true;

                GameManager.Instance.ShoggothLook();
                ChaseAS.Raise(SO_Sound.Entity.ChaseBGM, SType.BGM);
            }

            if (IsChasing)
            {
                if ((GameManager.Instance.Player.transform.position - transform.position).sqrMagnitude > SO_Player.Entity.EnemyStopChaseRange * SO_Player.Entity.EnemyStopChaseRange)
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

                    ChaseAS.Stop();

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

                // �i�����Ă���Ȃ�j�����Ă�������𔻒f����B
                if (moveDir != Vector2.zero)
                {
                    lookingDir = moveDir.ToDir();
                }

                // �K�������Ă�������̎��̐������W�܂ňړ�����B
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

            // path�̏����������Ȃ��̂ŁA�ړI�n��path�łȂ�������ړ��̏������s��Ȃ��B
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
