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
            // Screen.SetResolution(1920, 1080, true); // 解像度とフルスクリーンにするかどうかを設定

            QualitySettings.vSyncCount = 0; // VSyncをOFFにする
            Application.targetFrameRate = 60; // ターゲットフレームレートの設定
        }
    }
}
