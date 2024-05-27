using SO;
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
            // �𑜓x�ƃt���X�N���[���ɂ��邩�ǂ�����ݒ�
            Screen.SetResolution(SO_GameState.Entity.Resolution.x, SO_GameState.Entity.Resolution.y, SO_GameState.Entity.IsFullScreen);

            // Vsync�i�ƃ^�[�Q�b�g�t���[�����[�g�j�̐ݒ�
            if (SO_GameState.Entity.IsVsyncOn)
            {
                QualitySettings.vSyncCount = 1; // VSync��ON�ɂ���
            }
            else
            {
                QualitySettings.vSyncCount = 0; // VSync��OFF�ɂ���
                Application.targetFrameRate = SO_GameState.Entity.TargetFrameRate; // �^�[�Q�b�g�t���[�����[�g�̐ݒ�
            }
        }
    }
}
