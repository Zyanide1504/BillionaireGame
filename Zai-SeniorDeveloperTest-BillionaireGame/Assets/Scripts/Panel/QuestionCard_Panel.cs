using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCard_Panel : MonoBehaviour
{
    public List<Question_Card> question_CardList;

    public IEnumerator ShowAllCard() 
    {
        foreach(var x in question_CardList) 
        {
            StartCoroutine(x.ShowCard());
        }

        yield return null;
    }

    public IEnumerator HideAllCard()
    {
        foreach (var x in question_CardList)
        {
            StartCoroutine(x.HideCard());
        }

        yield return null;
    }

    public IEnumerator RevealAllCard() 
    {

        foreach (var x in question_CardList)
        {
            if (!x.isReveal)
            {
                StartCoroutine(x.FlipCard());
            }
        }
        yield return null;
    
    }

    public IEnumerator FlipAllCard()
    {

        foreach (var x in question_CardList)
        {

                StartCoroutine(x.FlipCard());
        }
        yield return null;

    }

}
