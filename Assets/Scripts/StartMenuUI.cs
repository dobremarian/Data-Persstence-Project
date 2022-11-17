using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMenuUI : MonoBehaviour
{
    public Text bestScoreText;
    public InputField nameInputBox;

    private string nameHolder;
    private string playerName;

    // Start is called before the first frame update
    void Start()
    {
        UpdateBestScoreText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateBestScoreText()
    {
        if (GameManager.instance.ReturnSavedScoresList().Count != 0)
        {
            SaveData bestScore = GameManager.instance.ReturnBestScore();

            bestScoreText.text = "Best Score : " + bestScore.playerName + " : " + bestScore.score;
        }
        else
        {
            bestScoreText.text = "Best Score : : 0";
        }
    }

    public void ReadNameInput(string text)
    {
        nameHolder = text;
        Debug.Log(nameHolder);
    }

    public void StartButton()
    {
        if(!string.IsNullOrEmpty(nameHolder) && !string.IsNullOrWhiteSpace(nameHolder) &&  nameHolder != "" && nameHolder.Trim() != string.Empty)
        {
            playerName = nameHolder;
            GameManager.instance.playerName = playerName;
            Debug.Log(playerName + " was saved!");
            SceneManager.LoadScene(1);
        }
    }

    public void QuitButton()
    {
        GameManager.instance.SaveScoresFile();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }
}
