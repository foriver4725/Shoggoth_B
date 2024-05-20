using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace General
{
    public class DynamicTextureClear : MonoBehaviour
    {
        [SerializeField] TMP_FontAsset _regularDynamicFont;

        void Start()
        {
            _regularDynamicFont.ClearFontAssetData();
        }

        void OnDestroy()
        {
            _regularDynamicFont?.ClearFontAssetData();
        }
    }
}
