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
        [SerializeField] AudioSource walkAS;

        // �v���C���[�̌���
        public DIR LookingDir { get; set; } = DIR.DOWN;

        // �v���C���[�̈ړ������̃x�N�g��(�傫��1��z��)
        public Vector2 InputDir { get; set; } = Vector2.zero;

        // �v���C���[���������W�܂ňړ��������Ă��邩
        bool isStepEnded = true;

        enum MoveState { STOP, WALK, DASH };
        MoveState moveState = MoveState.STOP; // ���݂̃t���[���̈ړ����
        MoveState moveStatePre = MoveState.STOP; // 1�O�̃t���[���̈ړ����
        bool isOnDash = true; // �_�b�V����ԂɂȂ����t���[���ł��邩
        bool isOnWalk = true; // ������ԂɂȂ����t���[���ł��邩
        bool isOnStop = true; // ��~��ԂɂȂ����t���[���ł��邩

        void Update()
        {
            // �N���A�܂��̓Q�[���I�[�o�[�Ȃ瓮���Ȃ�
            if (GameManager.Instance.EventState == EventState.End) return;

            InputDir = Time.timeScale == 1f ? InputGetter.Instance.MainGame_ValueMove : Vector2.zero;

            // �_�b�V���̓��͂����m���āA�t���O���X�V����

            if (InputGetter.Instance.MainGame_IsDash && GameManager.Instance.Stamina != 0) moveState = MoveState.DASH;
            else if (InputDir != Vector2.zero) moveState = MoveState.WALK;
            else moveState = MoveState.STOP;

            isOnStop = (moveState == MoveState.STOP) && (moveStatePre != MoveState.STOP);
            isOnWalk = (moveState == MoveState.WALK) && (moveStatePre != MoveState.WALK);
            isOnDash = (moveState == MoveState.DASH) && (moveStatePre != MoveState.DASH);

            moveStatePre = moveState;

            if (isOnDash) walkAS.Raise(SO_Sound.Entity.DashFootstepBGM, SType.BGM);
            if (isOnWalk) walkAS.Raise(SO_Sound.Entity.FootstepBGM, SType.BGM);
            else if (isOnStop) walkAS.Stop();

            if (isStepEnded)
            {
                // �i�����Ă���Ȃ�j�����Ă�������𔻒f����B
                if (InputDir != Vector2.zero)
                {
                    LookingDir = InputDir.ToDir();
                }

                // �A�j���[�V�����̑J�ڂ𔭉΂�����B
                anim.SetBool("IsMoving", InputDir != Vector2.zero);
                anim.SetInteger("LookingDirection", (int)LookingDir);

                // �K�������Ă�������̎��̐������W�܂ňړ����A���̊Ԃ͓��͂��󂯕t���Ȃ��B
                if (InputDir != Vector2.zero)
                {
                    isStepEnded = false;
                    StartCoroutine(MoveTo(LookingDir.ToVector3()));
                }
            }
        }

        IEnumerator MoveTo(Vector3 dir)
        {
            Vector3 fromPos = transform.position;
            Vector3 toPos = fromPos + dir;

            // path�̏����������Ȃ��̂ŁA�ړI�n��path�łȂ�������ړ��̏������s��Ȃ��B
            Vector2Int toPosInt = new((int)toPos.x, (int)toPos.y);
            if (!GameManager.Instance.PathPositions.Contains(toPosInt) || !GameManager.Instance.FencePoints.IsPath(toPosInt))
            {
                isStepEnded = true;
                yield break;
            }

            while (true)
            {
                //true,true:A  true,false:B  false,false:C  false,true:C
                transform.position +=
                    (InputGetter.Instance.MainGame_IsDash && GameManager.Instance.Stamina != 0 ?
                    SO_Player.Entity.PlayerDashSpeed : SO_Player.Entity.PlayerSpeed)
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