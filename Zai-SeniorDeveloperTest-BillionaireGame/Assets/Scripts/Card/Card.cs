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


    public void ShowCard() 
    {
        if (!isShow) 
        {
            animator.SetTrigger("SlideIN");
            gameManager.audio_Manager.PlaySoundEffect("CardSlide", 1);
            isShow = true;
        }

    }

    public void HideCard()
    {
        if (isShow)
        {
            animator.SetTrigger("SlideOut");
            gameManager.audio_Manager.PlaySoundEffect("CardSlide", 1);
            isShow = false;
        }
    }

    public void Set_interact(bool interact) 
    {
        cardButton.interactable = interact;
    }


    public IEnumerator ShowAndFlip()
    {
        ShowCard();
        yield return new WaitForSeconds(1f);
        FlipCard();
    }

    public IEnumerator FlipAndHide()
    {
        FlipCard();
        yield return new WaitForSeconds(1f);
        HideCard();

    }
}
