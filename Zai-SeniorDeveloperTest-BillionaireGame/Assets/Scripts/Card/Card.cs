using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    [Header("Card Back")]
    public GameObject card_Back;
    [Header("Card Button")]
    public Button cardButton;

    public bool isReveal;
    private bool isShow;
    public Animator animator;
    [HideInInspector]
    public GameManager gameManager;

    public void GameManagerInstance() 
    {
        gameManager = GameManager.Instance;
    }

    //Function สำหรับกลับไพ่
    public void FlipCard()
    {
        if (!isReveal)
        {
            animator.SetTrigger("FlipUp");
            gameManager.audio_Manager.PlaySoundEffect("FlipCard", 0.5f);
            isReveal = true;
        }
        else 
        {
            animator.SetTrigger("FlipDown");
            gameManager.audio_Manager.PlaySoundEffect("FlipCard", 0.5f);
            isReveal = false;
        }
    }

    //Function สำหรับแสดงไพ่
    public void ShowCard() 
    {
        if (!isShow) 
        {
            animator.SetTrigger("SlideIN");
            gameManager.audio_Manager.PlaySoundEffect("CardSlide", 1);
            isShow = true;
        }

    }
    //Function สำหรับซ่อนไพ่
    public void HideCard()
    {
        if (isShow)
        {
            animator.SetTrigger("SlideOut");
            gameManager.audio_Manager.PlaySoundEffect("CardSlide", 1);
            isShow = false;
        }
    }

    //Function สำหรับตั้งค่า interact ของปุ่ม
    public void Set_interact(bool interact) 
    {
        cardButton.interactable = interact;
    }


    //Function สำหรับ แสดงไพ่แล้วหงายไพ่
    public IEnumerator ShowAndFlip()
    {
        ShowCard();
        yield return new WaitForSeconds(1f);
        FlipCard();
    }

    //Function สำหรับ คว่ำไพ่แล้วซ่อนไพ่
    public IEnumerator FlipAndHide()
    {
        FlipCard();
        yield return new WaitForSeconds(1f);
        HideCard();

    }
}
