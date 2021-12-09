using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score_Manager : MonoBehaviour
{
    public Text Score_Text;
    private int CurrentScore;
    private int WinScore;
    private GameManager gameManager;

    //ฟังชั่นในการตั้งคะแนนที่จะชนะบท UI
    public void SetWinScore(int score) 
    {
        WinScore = score;
        IniScoreText();
    }


    //ฟังชั่น Update คะแนนที่โชว์
    public void IniScoreText() 
    {
        CurrentScore = 0;
        Score_Text.text = CurrentScore.ToString()+"/" + WinScore;

    }

    //ฟังชั่นเพิ่มคะแนนที่โชว์
    public void AddScore(int score) 
    {
        CurrentScore += score;
        Score_Text.text = CurrentScore.ToString()+"/" + WinScore; 
    }

    //ฟังชั่นเรียก คะแนนปัจจุบัญเพื่อไปเชคใน G
    public bool CheckScoreWinCondition()
    {

        return (CurrentScore>=WinScore);
    }

}
