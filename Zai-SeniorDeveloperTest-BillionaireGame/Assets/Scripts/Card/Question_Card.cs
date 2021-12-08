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

    private GameManager gameManager;
    public void Start()
    {
        gameManager = GameManager.Instance;
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
