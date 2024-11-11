using System;
using System.IO;
using SO;
using UnityEngine;

namespace General
{
    public sealed class SaveDataHolder : MonoBehaviour
    {
        public static SaveDataHolder Instance { get; private set; } = null;

        public SaveData SaveData { get; private set; } = null;

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
        public uint ClearNumEasy = 0;
        public uint ClearNumNormal = 0;
        public uint ClearNumHard = 0;
        public uint ClearNumNightmare = 0;
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
