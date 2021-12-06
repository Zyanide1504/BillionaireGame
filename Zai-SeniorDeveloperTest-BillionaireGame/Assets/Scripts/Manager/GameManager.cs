using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public API_Manager api_Manager;
    public QuestionCard_Panel questionCard_Panel;
    public List<QuestionScore> card_scorelist;

    public float revealAllCard_delay;



    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame() 
    {
        yield return StartCoroutine(api_Manager.Get_QuestionList());
        SetUpQuestionPanel();
    }

    public void SetUpQuestionPanel() 
    {
        StartCoroutine(IE_SetUpQuestionPanel());
    }

    public IEnumerator IE_SetUpQuestionPanel() 
    {
      
        var question_cardList = questionCard_Panel.question_CardList;

        for (int i = 0; i < question_cardList.Count; i++)
        {
            question_cardList[i].SetScore(RandomCardScore());
        }

        StartCoroutine(questionCard_Panel.ShowAllCard());

        yield return null;
    }

    public void CardSelect(int score) 
    {
        StartCoroutine(IE_CardSelect(score));
    }

    public IEnumerator IE_CardSelect(int score) 
    {
        QuestionID_Info_List temp_QIDL = new QuestionID_Info_List();
        temp_QIDL.question_list = new List<QuestionID_Info>();

        foreach (var x in api_Manager.QuestionID_List.question_list)
        {
            if (x.score == score) 
            {
                temp_QIDL.question_list.Add(x);
            }
        }

        int random = Random.Range(0, temp_QIDL.question_list.Count);

        StartCoroutine(api_Manager.Get_Question_By_ID(temp_QIDL.question_list[random].question_ID));



        StartCoroutine(questionCard_Panel.RevealAllCard());

        yield return new WaitForSeconds(revealAllCard_delay);

        yield return StartCoroutine(questionCard_Panel.FlipAllCard());

        StartCoroutine(questionCard_Panel.HideAllCard());

        yield return null;
    }



    public int RandomCardScore() 
    {

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

                current.minChance = card_scorelist[i-1].maxChance;
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

