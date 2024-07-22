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

        // プレイヤーの向き
        public DIR LookingDir { get; set; } = DIR.DOWN;

        // プレイヤーの移動方向のベクトル(大きさ1を想定)
        public Vector2 InputDir { get; set; } = Vector2.zero;

        // プレイヤーが整数座標まで移動完了しているか
        bool isStepEnded = true;

        enum MoveState { STOP, WALK, DASH };
        MoveState moveState = MoveState.STOP; // 現在のフレームの移動状態
        MoveState moveStatePre = MoveState.STOP; // 1つ前のフレームの移動状態
        bool isOnDash = true; // ダッシュ状態になったフレームであるか
        bool isOnWalk = true; // 歩き状態になったフレームであるか
        bool isOnStop = true; // 停止状態になったフレームであるか

        void Update()
        {
            // クリアまたはゲームオーバーなら動かない
            if (GameManager.Instance.IsClear || GameManager.Instance.IsOver) return;

            InputDir = Time.timeScale == 1f ? InputGetter.Instance.MainGame_ValueMove : Vector2.zero;

            // ダッシュの入力を検知して、フラグを更新する

            if (InputGetter.Instance.MainGame_IsDash && StaminaManager.Stamina != 0) moveState = MoveState.DASH;
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
                // （動いているなら）向いている方向を判断する。
                if (InputDir != Vector2.zero)
                {
                    LookingDir = InputDir.ToDir();
                }

                // アニメーションの遷移を発火させる。
                anim.SetBool("IsMoving", InputDir != Vector2.zero);
                anim.SetInteger("LookingDirection", (int)LookingDir);

                // 必ず向いている方向の次の整数座標まで移動し、その間は入力を受け付けない。
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

            // pathの所しか動けないので、目的地がpathでなかったら移動の処理を行わない。
            if (!GameManager.Instance.PathPositions.Contains(new Vector2Int((int)toPos.x, (int)toPos.y)))
            {
                isStepEnded = true;
                yield break;
            }

            while (true)
            {
                //true,true:A  true,false:B  false,false:C  false,true:C
                transform.position +=
                    (InputGetter.Instance.MainGame_IsDash && StaminaManager.Stamina != 0 ?
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