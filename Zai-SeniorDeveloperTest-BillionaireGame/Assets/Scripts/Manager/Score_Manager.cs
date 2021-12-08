using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour
{
    public Text Score_Text;
    private int CurrentScore;

    public void AddScore(int score) 
    {
        CurrentScore += score;
        Score_Text.text = CurrentScore.ToString();
    }

    public int GetScore()
    {
        return CurrentScore;
    }
}
