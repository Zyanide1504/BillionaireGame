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

    public IEnumerator FlipCard()
    {
        card_Back.SetActive(!card_Back.active);

        if (!card_Back.active)
        {
            isReveal = true;
        }
        else 
        {
            isReveal = false;
        }

        yield return null;
    }


    public IEnumerator ShowCard() 
    {
      
        this.gameObject.SetActive(true);
        yield return null;
    }

    public IEnumerator HideCard()
    {
        this.gameObject.SetActive(false);
        yield return null;
    }

    public void Set_interact(bool interact) 
    {
        cardButton.interactable = interact;
    }
}
