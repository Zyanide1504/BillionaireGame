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
        // ใช้คำสั่ง GameManagerInstance จาก Card
        GameManagerInstance();
    }

    //function ไว้ตั่งงค่าคะแนนของตัวการ์ด
    public void SetScore(int _score) 
    {
        score_Text.text = _score.ToString();
        score = _score;
    }

    // ฟังชั่นเชื่อมกับปุ่มเมื่อกดจะหงายตัวเองแล้วทำการเรียกต่อไปที่ GameManager เพื่อบอกว่าผู้เล่นเลือกการ์ดแล้ว
    public void CardClick() 
    {
        FlipCard();
        gameManager.OnCardSelect(score);
    }
}
