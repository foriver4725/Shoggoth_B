using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public class GameStateSetter
    {
        [RuntimeInitializeOnLoadMethod]
        static void RuntimeInitializeOnLoadMethods()
        {
            // Screen.SetResolution(1920, 1080, true); // �𑜓x�ƃt���X�N���[���ɂ��邩�ǂ�����ݒ�

            QualitySettings.vSyncCount = 0; // VSync��OFF�ɂ���
            Application.targetFrameRate = 60; // �^�[�Q�b�g�t���[�����[�g�̐ݒ�
        }
    }
}
