using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;



public class NameSaver : MonoBehaviour
{
    public static NameSaver Instance;
    public string PlayerName;

    public string BestPlayerName = "";

    public int CurrentScore;
    public int HighScore = 0;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    [System.Serializable]
    class SaveData
    {
        public string BestPlayerName;
        public int HighScore;
    }
    public void SavePlayerScore()
    {
        SaveData data = new SaveData();
        if (CurrentScore>HighScore)
        {

            BestPlayerName = PlayerName;
            HighScore = CurrentScore;
            data.BestPlayerName = BestPlayerName;
            data.HighScore = HighScore;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    public void LoadPlayerScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            BestPlayerName = data.BestPlayerName;
            HighScore = data.HighScore;
        }
    }
}

