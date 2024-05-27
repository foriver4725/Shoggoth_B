using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] Animator anim;

        void Update()
        {
            Debug.Log(IA.InputGetter.Instance.MainGame_ValueScrollItem);
        }
    }
}