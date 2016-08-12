using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Score scoreComponent;
    int score;
    Text text;
	// Use this for initialization
	void Awake ()
    {
        score = 0;
        scoreComponent = this;
        text = GetComponent<Text>();
	}
	
	public void IncreaseScore(int add)
    {
        score+=add;
        text.text = score.ToString(); 
    }
    public void ResetScore()
    {
        score =0;
        text.text = score.ToString();
    }
}
