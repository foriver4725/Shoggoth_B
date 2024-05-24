using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Test_InputCheck : MonoBehaviour
    {
        void Update()
        {
            if (IA.InputGetter.Instance.IsSubmit)
            {
                Debug.Log("<color=red>Submitが押された瞬間のフレームにのみ、この処理は呼ばれる。</color>");
            }

            if (IA.InputGetter.Instance.IsCancel)
            {
                Debug.Log("<color=blue>Cancelが押された瞬間のフレームにのみ、この処理は呼ばれる。</color>");
            }

            Debug.Log($"<color=green>常に、正規化された2軸入力を検知している。今の値は：　{IA.InputGetter.Instance.ValueMove}</color>");

            if (IA.InputGetter.Instance.IsHold)
            {
                Debug.Log("<color=yellow>Holdが長押しされて3秒経った瞬間のフレームにのみ、この処理は呼ばれる。</color>");
            }
        }
    }
}
