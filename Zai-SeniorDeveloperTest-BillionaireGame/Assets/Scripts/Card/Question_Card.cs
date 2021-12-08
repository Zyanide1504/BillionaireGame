using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question_Card : Card
{
    [Header("Question Card info")]
    [SerializeField]
    private Text score_Text;
    [SerializeField]
    private int score;

  
    public void Start()
    {
        GameManagerInstance();
    }


    public void SetScore(int _score) 
    {
        score_Text.text = _score.ToString();
        score = _score;
    }

    public void CardClick() 
    {
        FlipCard();
        gameManager.OnCardSelect(score);
    }
}
