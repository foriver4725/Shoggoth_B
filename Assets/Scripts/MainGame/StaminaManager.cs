using IA;
using SO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MainGame
{
    public class StaminaManager : MonoBehaviour
    {
        [SerializeField] Image FrontStamina;

        // ���݂̃X�^�~�i (0 ~ 1)
        private float _stamina = 1;
        private float stamina
        {
            get
            {
                return _stamina;
            }
            set
            {
                _stamina = Mathf.Clamp(value, 0, 1);
            }
        }

        void Update()
        {
            // �_�b�V��������...
            if (InputGetter.Instance.MainGame_IsDash)
            {
                // �X�^�~�i����
                stamina -= 0.2f * Time.deltaTime;
            }

            // ��ɃX�^�~�i�͉񕜂���
            stamina += 0.1f * Time.deltaTime;
        }
    }
}
