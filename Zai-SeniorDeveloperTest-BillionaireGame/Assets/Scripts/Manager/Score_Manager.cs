using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour
{
    public Text Score_Text;
    private int CurrentScore;
    private int WinScore;

    public void SetWinScore(int score) 
    {
        WinScore = score;
        IniScoreText();
    }

    public void IniScoreText() 
    {
        CurrentScore = 0;
        Score_Text.text = CurrentScore.ToString()+"/" + WinScore;

    }

    public void AddScore(int score) 
    {
        CurrentScore += score;
        Score_Text.text = CurrentScore.ToString()+"/" + WinScore; 
    }

    public int GetScore()
    {
        return CurrentScore;
    }
}
