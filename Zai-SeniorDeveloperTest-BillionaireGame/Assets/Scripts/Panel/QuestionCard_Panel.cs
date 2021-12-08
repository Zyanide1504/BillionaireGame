using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionCard_Panel : MonoBehaviour
{
    private GameManager gameManager;
    public List<Question_Card> question_CardList;

    public void Start()
    {
        gameManager = GameManager.Instance;
    }

    public IEnumerator ShowAllCard() 
    {
        foreach(var x in question_CardList) 
        {
            yield return new WaitForSeconds(0.5f);
            x.ShowCard();
        }

        yield return null;
    }

    public void Setinteract_AllCard(bool interact)
    {
        foreach (var x in question_CardList)
        {
            x.Set_interact(interact);
        }

    }


    public IEnumerator HideAllCard()
    {
        foreach (var x in question_CardList)
        {
            x.HideCard();
        }

        yield return null;
    }

    public IEnumerator RevealAllCard() 
    {

        foreach (var x in question_CardList)
        {
            if (!x.isReveal)
            {
                x.FlipCard();
            }
        }
        yield return null;
    
    }

    public IEnumerator FlipAllCard()
    {

        foreach (var x in question_CardList)
        {
             x.FlipCard();
        }


        yield return null;
    }

    public IEnumerator SetUpQuestionCardPanel() 
    {

        for (int i = 0; i < question_CardList.Count; i++)
        {
            question_CardList[i].SetScore(RandomCardScore());
        }

        StartCoroutine(ShowAllCard());
        yield return new WaitForSeconds(3f);
        Setinteract_AllCard(true);

        yield return null;
    }


    public int RandomCardScore()
    {
        var card_scorelist = gameManager.card_scorelist;
        float chanceSum = 0;

        for (int i = 0; i < card_scorelist.Count; i++)
        {
            var current = card_scorelist[i];
            chanceSum += current.Chance;
            if (i == 0)
            {
                current.minChance = 0;
                current.maxChance = current.Chance;

            }
            else
            {

                current.minChance = card_scorelist[i - 1].maxChance;
                current.maxChance = current.minChance + current.Chance;

            }

        }

        float rand = Random.Range(0, chanceSum);

        for (int i = 0; i < card_scorelist.Count; i++)
        {
            var current = card_scorelist[i];

            if (rand >= current.minChance && rand < current.maxChance)
            {
                return current.score;
            }
        }

        return 0;

    }
}
