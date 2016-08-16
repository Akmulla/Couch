using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Score : MonoBehaviour
{
    public static Score scoreComponent;
    int score;
    int bestScore;
    Text scoreText;
    public GameObject finishWindow;
    public Text finalScoreText;
    public Text bestScoreText;
    
	// Use this for initialization
	void Awake ()
    {
        score = 0;
        bestScore = 0;
        scoreComponent = this;
        scoreText = GetComponent<Text>();
        if (File.Exists((Application.persistentDataPath + "/playerInfo.dat")))
        {
            SaveLoad.Load();
            
        }
        finishWindow.SetActive(false);
	}
	
	public void IncreaseScore(int add)
    {
        score+=add;
        scoreText.text = score.ToString(); 
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void Finish()
    {
        finalScoreText.text = "Final Score: " + score;
        GetSetBestScore = score;
        SaveLoad.Save(GetSetBestScore);
        finishWindow.SetActive(true);

    }

    public void Continue()
    {
        finishWindow.SetActive(false);
    }

    public int GetSetScore
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.text = score.ToString();
        }
    }

    public int GetSetBestScore
    {
        get
        {
            return bestScore;
        }
        set
        {
            if (value > bestScore)
            {
                bestScore = value;
                bestScoreText.text = "Best Score: " + bestScore.ToString();
            }
        }
    }
}
