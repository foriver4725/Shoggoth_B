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
                Debug.Log("<color=red>Submit�������ꂽ�u�Ԃ̃t���[���ɂ̂݁A���̏����͌Ă΂��B</color>");
            }

            if (IA.InputGetter.Instance.IsCancel)
            {
                Debug.Log("<color=blue>Cancel�������ꂽ�u�Ԃ̃t���[���ɂ̂݁A���̏����͌Ă΂��B</color>");
            }

            Debug.Log($"<color=green>��ɁA���K�����ꂽ2�����͂����m���Ă���B���̒l�́F�@{IA.InputGetter.Instance.ValueMove}</color>");

            if (IA.InputGetter.Instance.IsHold)
            {
                Debug.Log("<color=yellow>Hold�������������3�b�o�����u�Ԃ̃t���[���ɂ̂݁A���̏����͌Ă΂��B</color>");
            }
        }
    }
}
