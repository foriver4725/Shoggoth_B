using General;
using SO;
using UnityEngine;

namespace Scene
{
    public static class GameInitialize
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Initialize()
        {
#if UNITY_EDITOR
            if (SO_Debug.Entity.IsDeleteAllSaveData)
            {
                SaveDataHolder.Instance.SaveData = new();
                SaveDataHolder.Instance.Save();
            }
#endif

            SaveDataHolder.Instance.Load();
        }
    }
}
