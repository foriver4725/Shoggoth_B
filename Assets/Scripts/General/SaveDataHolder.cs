using System;
using System.IO;
using SO;
using UnityEngine;

namespace General
{
    public sealed class SaveDataHolder : MonoBehaviour
    {
        public static SaveDataHolder Instance { get; private set; } = null;

        public SaveData SaveData { get; private set; } = new();

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
        public uint ClearNumEasy { get; set; } = 0;
        public uint ClearNumNormal { get; set; } = 0;
        public uint ClearNumHard { get; set; } = 0;
        public uint ClearNumNightmare { get; set; } = 0;

        private const string PATH = "saves.json";

        public static void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);
            StreamWriter sw = new(Path.Combine(Application.persistentDataPath, PATH), false);
            sw.Write(json);
            sw.Flush();
            sw.Close();
        }

        public static void Load(out SaveData data)
        {
            try
            {
                StreamReader sr = new(Path.Combine(Application.persistentDataPath, PATH));
                string json = sr.ReadToEnd();
                sr.Close();
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
