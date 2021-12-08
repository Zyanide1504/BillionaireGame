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


    public void FlipCard()
    {
        if (!isReveal)
        {
            animator.SetTrigger("FlipUp");

            isReveal = true;
        }
        else 
        {
            animator.SetTrigger("FlipDown");
            isReveal = false;
        }
    }


    public void ShowCard() 
    {
        if (!isShow) 
        {
            animator.SetTrigger("SlideIN");
            isShow = true;
        }

    }

    public void HideCard()
    {
        if (isShow)
        {
            animator.SetTrigger("SlideOut");
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
