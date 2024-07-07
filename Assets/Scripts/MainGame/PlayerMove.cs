using IA;
using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;

namespace MainGame
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] Animator anim;

        // �v���C���[�̌���
        DIR lookingDir = DIR.DOWN;

        // �v���C���[���������W�܂ňړ��������Ă��邩
        bool isStepEnded = true;

        // �����Ă��邩
        public bool IsAlive { get; set; } = true;

        void Update()
        {
            // ����ł���Ȃ瓮���Ȃ�
            if (!IsAlive) return;

            if (isStepEnded)
            {
                Vector2 inputDir = InputGetter.Instance.MainGame_ValueMove;

                // �i�����Ă���Ȃ�j�����Ă�������𔻒f����B
                if (inputDir != Vector2.zero)
                {
                    lookingDir = inputDir.ToDir();
                }

                // �A�j���[�V�����̑J�ڂ𔭉΂�����B
                anim.SetBool("IsMoving", inputDir != Vector2.zero);
                anim.SetInteger("LookingDirection", (int)lookingDir);

                // �K�������Ă�������̎��̐������W�܂ňړ����A���̊Ԃ͓��͂��󂯕t���Ȃ��B
                if (inputDir != Vector2.zero)
                {
                    isStepEnded = false;
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
                isStepEnded = true;
                yield break;
            }


            while (true)
            {
                //true,true:A  true,false:B  false,false:C  false,true:C
                transform.position +=
                    (InputGetter.Instance.MainGame_IsDash ? StaminaManager.StaminaDetection?
                    SO_Player.Entity.PlayerDashSpeed : SO_Player.Entity.PlayerSpeed: SO_Player.Entity.PlayerSpeed)
                    * Time.deltaTime * dir;
                if ((transform.position - fromPos).sqrMagnitude >= 1)
                    break;
                yield return null;
            }

            transform.position = toPos;
            isStepEnded = true;
        }
    }
}