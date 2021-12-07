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

    public Animator animator;


    public IEnumerator FlipCard()
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
        yield return null;
    }


    public IEnumerator ShowCard() 
    {
      
        animator.SetTrigger("SlideIN");
        yield return null;
    }

    public IEnumerator HideCard()
    {
       
        animator.SetTrigger("SlideOut");
        yield return null;
    }

    public void Set_interact(bool interact) 
    {
        cardButton.interactable = interact;
    }
}
