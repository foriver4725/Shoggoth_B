using IA;
using SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] Animator anim;

        // プレイヤーの向きを判定する際に使う2つのベクトル。
        // 入力された移動方向の単位ベクトルと、これらとの内積の正負の組み合わせによって、向いている方向を判断する。
        static readonly Vector2 baseVec1 = new(-1, -1);
        static readonly Vector2 baseVec2 = new(1, -1);

        // プレイヤーの向き
        enum DIR { DOWN, LEFT, UP, RIGHT }
        DIR lookingDir = DIR.DOWN;

        // プレイヤーが整数座標まで移動完了しているか
        bool IsStepEnded = true;

        void Update()
        {
            if (IsStepEnded)
            {
                Vector2 inputDir = InputGetter.Instance.MainGame_ValueMove;

                // （動いているなら）向いている方向を判断する。
                if (inputDir != Vector2.zero)
                {
                    float dot1 = Vector2.Dot(baseVec1, inputDir);
                    float dot2 = Vector2.Dot(baseVec2, inputDir);

                    lookingDir = (dot1 >= 0, dot2 >= 0) switch
                    {
                        (true, true) => DIR.DOWN,
                        (true, false) => DIR.LEFT,
                        (false, true) => DIR.RIGHT,
                        (false, false) => DIR.UP,
                    };
                }

                // アニメーションの遷移を発火させる。
                anim.SetBool("IsMoving", inputDir != Vector2.zero);
                anim.SetInteger("LookingDirection", (int)lookingDir);

                // 必ず向いている方向の次の整数座標まで移動し、その間は入力を受け付けない。
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
                transform.position += SO_Player.Entity.Speed * Time.deltaTime * dir;
                if ((transform.position - fromPos).sqrMagnitude >= 1)
                    break;
                yield return null;
            }

            transform.position = toPos;
            IsStepEnded = true;
        }

        // プレイヤーの向きから、その方向の単位ベクトルを返す。
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