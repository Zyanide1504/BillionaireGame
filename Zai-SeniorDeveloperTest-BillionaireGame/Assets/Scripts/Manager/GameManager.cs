using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Manager")]
    public API_Manager api_Manager;
    [Header("Panel")]
    public QuestionCard_Panel questionCard_Panel;
    public QuestionPanel question_Panel;
    
    [Header("Game Setting")]
    public List<QuestionScore> card_scorelist;
    public int win_Score;
    public float delay_countDown;
    public float gameOverTime;
    public float revealAllCard_delay;

    private Coroutine gameOverCountDown;


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
        StartCoroutine(questionCard_Panel.SetUpQuestionCardPanel());
    }


    public void OnCardSelect(int score) 
    {
        StartCoroutine(IE_OnCardSelect(score));
    }

    public IEnumerator IE_OnCardSelect(int score) 
    {
        questionCard_Panel.Setinteract_AllCard(false);

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

        yield return StartCoroutine(api_Manager.Get_Question_By_ID(temp_QIDL.question_list[random].question_ID));



        StartCoroutine(questionCard_Panel.RevealAllCard());

        yield return new WaitForSeconds(revealAllCard_delay);

        yield return StartCoroutine(questionCard_Panel.FlipAllCard());

        StartCoroutine(questionCard_Panel.HideAllCard());

        yield return StartCoroutine(question_Panel.Setup_QuestionPanel());
        gameOverCountDown = StartCoroutine(GameOverCountDown());
         
        yield return null;
    }

    public void OnSelectAnswer(string answer) 
    {
        StopCoroutine(gameOverCountDown);
        StartCoroutine(IE_OnSelectAnswer(answer));
    }

    public IEnumerator IE_OnSelectAnswer(string answer)
    {
        if (answer == api_Manager.current_Question.answer)
        {
            question_Panel.HideQuestionPanel();
            StartCoroutine(questionCard_Panel.SetUpQuestionCardPanel());
        }
        else
        {
            OnGameOver();
        }

        yield return null;
    }

    public IEnumerator GameOverCountDown() 
    {
        yield return new WaitForSeconds(delay_countDown);

        var timer = gameOverTime;

        while (timer > 0) 
        {
        
        }

    
    }


    public void OnGameOver()
    {
        StartCoroutine(IE_OnGameOver());
    }

    public IEnumerator IE_OnGameOver()
    {
        Debug.Log("GameOver");
        yield return null;
    }





}

