using System;
using System.IO;
using UnityEngine;

namespace General
{
    public sealed class SaveDataHolder : MonoBehaviour
    {
        public static SaveDataHolder Instance { get; private set; } = null;

#if UNITY_EDITOR
        public SaveData SaveData { get; set; } = null;
#else
        public SaveData SaveData { get; private set; } = null;
#endif

        private void Awake()
        {
            if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
            else { Destroy(gameObject); return; }

            Load();
        }

        // ロードしてメンバを更新
        public void Load()
        {
            SaveData.Load(out SaveData loadedData);
            SaveData = loadedData;
        }

        //メンバをセーブ
        public void Save()
        {
            SaveData.Save(SaveData);
        }
    }

    [Serializable]
    public sealed class SaveData
    {
        public bool HasEasyCleared = false;
        public bool HasNormalCleared = false;
        public bool HasHardCleared = false;
        public bool HasNightmareCleared = false;
        public ulong ClearNum = 0;
        public ulong OverNum = 0;
        public bool HasEnteredToilet = false;
        public bool HasToiletExploded = false;

        private const string PATH = "saves.json";

        public static void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);
            using StreamWriter sw = new(Path.Combine(Application.persistentDataPath, PATH), false);
            sw.WriteLine(json);
        }

        public static void Load(out SaveData data)
        {
            try
            {
                using StreamReader sr = new(Path.Combine(Application.persistentDataPath, PATH));
                string json = sr.ReadToEnd();
                data = JsonUtility.FromJson<SaveData>(json);
            }
            catch (Exception)
            {
                SaveData newData = new();
                Save(newData);
                data = newData;
            }
        }
    }
}
