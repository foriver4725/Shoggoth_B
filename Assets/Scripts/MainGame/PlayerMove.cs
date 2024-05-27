using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] Animator anim;

        // プレイヤーの向きを判定する際に使う2つの単位ベクトル。
        // 入力された移動方向の単位ベクトルと、これらとの内積の正負の組み合わせによって、向いている方向を判断する。
        readonly Vector2 baseVec1 = new Vector2(-1, -1).normalized;
        readonly Vector2 baseVec2 = new Vector2(1, -1).normalized;

        // プレイヤーの向き
        enum DIR { DOWN, LEFT, UP, RIGHT }
        DIR lookingDir = DIR.DOWN;

        float speed = 1f;

        void Update()
        {
            Vector2 inputDir = IA.InputGetter.Instance.MainGame_ValueMove;

            // （動いているなら）向いている方向を判断する。
            if (inputDir != Vector2.zero)
            {
                float dot1 = Vector2.Dot(baseVec1, inputDir);
                float dot2 = Vector2.Dot(baseVec2, inputDir);
                if (dot1 >= 0 && dot2 >= 0) lookingDir = DIR.DOWN;
                else if (dot1 >= 0 && dot2 < 0) lookingDir = DIR.LEFT;
                else if (dot1 < 0 & dot2 >= 0) lookingDir = DIR.RIGHT;
                else lookingDir = DIR.UP;
            }

            // アニメーションの遷移を発火させる。
            anim.SetBool("IsMoving", inputDir != Vector2.zero);
            anim.SetInteger("LookingDirection", (int)lookingDir);

            // （動いているなら）向いている方向に移動する。
            if (inputDir != Vector2.zero) transform.position += speed * Time.deltaTime * GetMoveDir(lookingDir);
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