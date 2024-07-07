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

        // プレイヤーの向き
        DIR lookingDir = DIR.DOWN;

        // プレイヤーが整数座標まで移動完了しているか
        bool isStepEnded = true;

        // 生きているか
        public bool IsAlive { get; set; } = true;

        void Update()
        {
            // 死んでいるなら動けない
            if (!IsAlive) return;

            if (isStepEnded)
            {
                Vector2 inputDir = InputGetter.Instance.MainGame_ValueMove;

                // （動いているなら）向いている方向を判断する。
                if (inputDir != Vector2.zero)
                {
                    lookingDir = inputDir.ToDir();
                }

                // アニメーションの遷移を発火させる。
                anim.SetBool("IsMoving", inputDir != Vector2.zero);
                anim.SetInteger("LookingDirection", (int)lookingDir);

                // 必ず向いている方向の次の整数座標まで移動し、その間は入力を受け付けない。
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