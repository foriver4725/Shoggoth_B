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

            LoadAndChangeData();
        }

        private void LoadAndChangeData()
        {
            SaveData.Load(out SaveData loadedData);
            SaveData = loadedData;
        }

        public void OnClearChangeDataAndSave(DifficultyType clearedDifficultyType)
        {
            switch (clearedDifficultyType)
            {
                case DifficultyType.Easy:
                    {
                        SaveData.ClearNumEasy++;
                    }
                    break;
                case DifficultyType.Normal:
                    {
                        SaveData.ClearNumNormal++;
                    }
                    break;
                case DifficultyType.Hard:
                    {
                        SaveData.ClearNumHard++;
                    }
                    break;
                case DifficultyType.Nightmare:
                    {
                        SaveData.ClearNumNightmare++;
                    }
                    break;
                default:
                    break;
            }

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
