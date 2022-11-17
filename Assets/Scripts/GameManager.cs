using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string playerName;
    public int score = 0;

    [SerializeField] List<SaveData> savedScoresList = new List<SaveData>();

    // Start is called before the first frame update
    void Start()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScoresFile();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddNewSave()
    {
        SaveData data = new SaveData();
        data.playerName = this.playerName;
        data.score = this.score;
        bool foundSamePlayer = false;

        for (int i = 0; i < savedScoresList.Count; i++)
        {
            if (savedScoresList[i].playerName == this.playerName && savedScoresList[i].score < this.score)
            {
                savedScoresList.RemoveAt(i);
                savedScoresList.Add(data);
                foundSamePlayer = true;
            }
            else if (savedScoresList[i].playerName == this.playerName && savedScoresList[i].score >= this.score)
            {
                foundSamePlayer = true;
            }
        }
        if (!foundSamePlayer)
        {
            savedScoresList.Add(data);
        }
    }

    public List<SaveData> ReturnSavedScoresList()
    {
        return savedScoresList;
    }

    public SaveData ReturnBestScore()
    {
        SaveData bestScore = new SaveData();

        if (savedScoresList.Count != 0)
        {
            bestScore = savedScoresList[0];

            for (int i = 1; i < savedScoresList.Count; i++)
            {
                if (savedScoresList[i].score > bestScore.score)
                {
                    bestScore = savedScoresList[i];
                }
            }


        }
        return bestScore;

    }

    public void SaveScoresFile()
    {
        List<SaveData> scoreList = savedScoresList;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SavedScores.gd");
        bf.Serialize(file, scoreList);
        file.Close();
        
    }

    public void LoadScoresFile()
    {
        string path = Application.dataPath + "/SavedScores.gd";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            savedScoresList = (List<SaveData>)bf.Deserialize(file);
            file.Close();
        }
    }
}
