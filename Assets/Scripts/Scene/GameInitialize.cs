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
            else if (SO_Debug.Entity.IsAchieveAllSaveData)
            {
                SaveDataHolder.Instance.SaveData.HasEasyCleared = true;
                SaveDataHolder.Instance.SaveData.HasNormalCleared = true;
                SaveDataHolder.Instance.SaveData.HasHardCleared = true;
                SaveDataHolder.Instance.SaveData.HasNightmareCleared = true;
                SaveDataHolder.Instance.SaveData.ClearNum = 999;
                SaveDataHolder.Instance.SaveData.OverNum = 999;
                SaveDataHolder.Instance.SaveData.HasEnteredToilet = true;
                SaveDataHolder.Instance.SaveData.HasToiletExploded = true;
                SaveDataHolder.Instance.SaveData.HasFoundSecretItem = true;
                SaveDataHolder.Instance.Save();
            }
#endif

            SaveDataHolder.Instance.Load();
        }
    }
}
