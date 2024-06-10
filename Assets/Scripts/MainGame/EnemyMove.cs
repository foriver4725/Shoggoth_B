using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;

namespace MainGame
{
    public class EnemyMove : MonoBehaviour
    {
        // �G�̌���
        DIR lookingDir = DIR.DOWN;

        // �v���C���[���������W�܂ňړ��������Ă��邩
        bool IsStepEnded = true;

        // �G���p�j�|�C���g��ɂ��邩�itrue�ɂȂ�����A1�񂾂��ړI���W���X�V����B�j
        bool isAtStokingPosition = true;
        Vector2Int targetPos = new();

        // �v���C���[��ǂ������郂�[�h�ɂȂ��Ă��邩
        bool isChasing = false;
        float stopChaseTime = 0;

        void Update()
        {
            // �v���C���[�ɋ߂Â�����ǐՃ��[�h�ɂȂ�B
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
