using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] Animator anim;

        // �v���C���[�̌����𔻒肷��ۂɎg��2�̒P�ʃx�N�g���B
        // ���͂��ꂽ�ړ������̒P�ʃx�N�g���ƁA�����Ƃ̓��ς̐����̑g�ݍ��킹�ɂ���āA�����Ă�������𔻒f����B
        readonly Vector2 baseVec1 = new Vector2(-1, -1).normalized;
        readonly Vector2 baseVec2 = new Vector2(1, -1).normalized;

        // �v���C���[�̌���
        enum DIR { DOWN, LEFT, UP, RIGHT }
        DIR lookingDir = DIR.DOWN;

        // �v���C���[���������W�܂ňړ��������Ă��邩
        bool IsStepEnded = true;

        float speed = 5f;

        void Update()
        {
            if (IsStepEnded)
            {
                Vector2 inputDir = IA.InputGetter.Instance.MainGame_ValueMove;

                // �i�����Ă���Ȃ�j�����Ă�������𔻒f����B
                if (inputDir != Vector2.zero)
                {
                    float dot1 = Vector2.Dot(baseVec1, inputDir);
                    float dot2 = Vector2.Dot(baseVec2, inputDir);
                    if (dot1 >= 0 && dot2 >= 0) lookingDir = DIR.DOWN;
                    else if (dot1 >= 0 && dot2 < 0) lookingDir = DIR.LEFT;
                    else if (dot1 < 0 & dot2 >= 0) lookingDir = DIR.RIGHT;
                    else lookingDir = DIR.UP;
                }

                // �A�j���[�V�����̑J�ڂ𔭉΂�����B
                anim.SetBool("IsMoving", inputDir != Vector2.zero);
                anim.SetInteger("LookingDirection", (int)lookingDir);

                // �K�������Ă�������̎��̐������W�܂ňړ����A���̊Ԃ͓��͂��󂯕t���Ȃ��B
                if (inputDir != Vector2.zero)
                {
                    IsStepEnded = false;
                    StartCoroutine(MoveTo(GetMoveDir(lookingDir)));
                }
            }
        }

        IEnumerator MoveTo(Vector3 dir)
        {
            Vector3 fromPos = transform.position;
            Vector3 toPos = fromPos + dir;

            while (true)
            {
                transform.position += speed * Time.deltaTime * dir;
                if ((transform.position - fromPos).sqrMagnitude >= 1) break;
                yield return null;
            }

            transform.position = toPos;
            IsStepEnded = true;
        }

        // �v���C���[�̌�������A���̕����̒P�ʃx�N�g����Ԃ��B
        Vector3 GetMoveDir(DIR dir)
        {
            Vector3 moveDir = dir switch
            {
                DIR.DOWN => Vector3.down,
                DIR.LEFT => Vector3.left,
                DIR.UP => Vector3.up,
                DIR.RIGHT => Vector3.right,
                _ => Vector3.zero
            };

            return moveDir;
        }
    }
}