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

    // ฟังชันแสดงการ์ดทั้งหมด
    public IEnumerator ShowAllCard() 
    {
        foreach(var x in question_CardList) 
        {
            x.ShowCard();
            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }

    // ฟังชัน Set interact ของ Button component ในการ์ดทุกใบ
    public void Setinteract_AllCard(bool interact)
    {
        foreach (var x in question_CardList)
        {
            x.Set_interact(interact);
        }

    }


    // ฟังชันซ่อนการ์ดทั้งหมด
    public IEnumerator HideAllCard()
    {
        foreach (var x in question_CardList)
        {
            x.HideCard();
        }

        yield return null;
    }


    // ฟังชันหงายไพ่ทุกใบ
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


    // ฟังชันกลับไพ่ทุกใบ
    public IEnumerator FlipAllCard()
    {

        foreach (var x in question_CardList)
        {
             x.FlipCard();
        }


        yield return null;
    }

    // ฟังชัน Setup panel ของการ์ด
    public IEnumerator SetUpQuestionCardPanel() 
    {
        StartCoroutine(gameManager.audio_Manager.PlayRandom_IN_NPC_Category("SelectQuestionCard"));
        var utility = new Utility();

        for (int i = 0; i < question_CardList.Count; i++)
        {
            var random_result = utility.RandomWithChance(gameManager.scoreCard_Chance);
            question_CardList[i].SetScore(int.Parse(random_result));
        }

        StartCoroutine(ShowAllCard());
        yield return new WaitForSeconds(3f);
        Setinteract_AllCard(true);

        yield return null;
    }

    // ฟังชัน ซ่อน panel ของการ์ด
    public IEnumerator HideQuestionCardPanel()
    {

        StartCoroutine(RevealAllCard());

        yield return new WaitForSeconds(gameManager.revealAllCard_delay);

        yield return StartCoroutine(FlipAllCard());

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(HideAllCard());

        yield return new WaitForSeconds(1f);

        yield return null;
    }
}
