using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static DataManager;

public class DataManager : MonoBehaviour, IComparer<ScoreData>
{
    private const int scoreCount = 5;

    public static DataManager Instance { get; private set; }

    [HideInInspector]
    public string userName;

    public SaveData saveData;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        LoadScoreData();
    }

    public ScoreData GetTopScore()
    {
        if (saveData.ScoreData.Count > 0)
        {
            return saveData.ScoreData[0];
        }

        return null;
    }

    public void SaveScore(int score)
    {
        saveData.ScoreData.Add(new ScoreData { Name = userName, Score = score });
        saveData.ScoreData.Sort(this);
        if(saveData.ScoreData.Count > scoreCount)
        {
            saveData.ScoreData.RemoveAt(saveData.ScoreData.Count - 1);
        }

        SaveScoreData();
    }

    private void LoadScoreData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText($"{Application.persistentDataPath}/savefile.json");

            if (!string.IsNullOrEmpty(json))
            {
                var data = JsonUtility.FromJson<SaveData>(json);

                saveData = data;
            }
        }

        if (saveData == null)
        {
            saveData = new SaveData {  ScoreData  = new List<ScoreData>() };
        }
    }

    private void SaveScoreData()
    {
        var json = JsonUtility.ToJson(saveData);

        File.WriteAllText($"{Application.persistentDataPath}/savefile.json", json);
    }

    public int Compare(ScoreData x, ScoreData y)
    {
        return y.Score.CompareTo(x.Score);
    }

    [Serializable]
    public class ScoreData 
    {
        public string Name;
        public int Score;
    }

    [Serializable]
    public class SaveData
    {
        public List<ScoreData> ScoreData;
    }

}
