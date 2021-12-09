using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Manager")]
    public API_Manager api_Manager;
    public AudioManager audio_Manager;
    public GamePlay_TimerManager timer_manager;
    public Score_Manager score_Manager;
    public Helper_Manager helper_Manager;
    [Header("Panel")]
    public QuestionCard_Panel questionCard_Panel;
    public QuestionPanel question_Panel;

    [Header("other Reference")]
    public Question_Card currentCard;
    public Notic_Script noticScript;
    public Animator scene_TransitionImage;


    [Header("Game Setting")]
    public List<Random_Chance> scoreCard_Chance;
    public List<Random_Chance> helper_ansChance;
    public int current_win_Score;
    public int win_Score_addPerWin;
    public int max_winScore;
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
        if (PlayerPrefs.HasKey("WinCount"))
        {
            current_win_Score += win_Score_addPerWin * PlayerPrefs.GetInt("WinCount");

            if (current_win_Score >= max_winScore) 
            {
                current_win_Score = max_winScore;
            }
        }
        else 
        {
            PlayerPrefs.SetInt("WinCount", 0);
        }

        score_Manager.SetWinScore(current_win_Score); 

        yield return StartCoroutine(api_Manager.Get_QuestionList());
        yield return StartCoroutine(audio_Manager.PlayRandom_IN_NPC_Category("Welcome"));
        yield return new WaitForSeconds(0.5f);
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

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(questionCard_Panel.HideAllCard());

        yield return new WaitForSeconds(1f);

        currentCard.SetScore(score);
        StartCoroutine(currentCard.ShowAndFlip());

        StartCoroutine(audio_Manager.PlayRandom_IN_NPC_Category("SeeQuestion"));
        yield return StartCoroutine(question_Panel.Setup_QuestionPanel());
        gameOverCountDown = StartCoroutine(timer_manager.GameOverCountDown());

        yield return null;
    }

    public void OnSelectAnswer(string answer) 
    {
        StopCoroutine(gameOverCountDown);
        StartCoroutine(IE_OnSelectAnswer(answer));
    }

    public IEnumerator IE_OnSelectAnswer(string answer)
    {
        helper_Manager.HideHelperBar();
        timer_manager.HideTimer();

        if (answer == api_Manager.current_Question.answer)
        {
            score_Manager.AddScore(api_Manager.current_Question.score);
            


            yield return StartCoroutine(audio_Manager.PlayRandom_IN_NPC_Category("RightAns"));

            if (score_Manager.GetScore() < current_win_Score)
            {
                yield return StartCoroutine(question_Panel.HideQuestionPanel());
                StartCoroutine(currentCard.FlipAndHide());
                StartCoroutine(questionCard_Panel.SetUpQuestionCardPanel());
            }
            else 
            {
                OnWin();
            }
        }
        else
        {
            yield return StartCoroutine(audio_Manager.PlayRandom_IN_NPC_Category("WrongAns"));
            OnGameOver();
        }

        yield return null;
    }


    public void OnWin() 
    {
        PlayerPrefs.SetInt("WinCount",PlayerPrefs.GetInt("WinCount") +1);
        StartCoroutine(IE_OnGameEnd("Win", "ชนะแล้ว"));
    }


    public void OnGameOver()
    {
        StartCoroutine(IE_OnGameEnd("GameOver", "แพ้แล้ว"));
    }

    public IEnumerator IE_OnGameEnd(string result,string noticString)
    {
        timer_manager.HideTimer();
        helper_Manager.HideHelperBar();
        Debug.Log(result);
        StartCoroutine(question_Panel.HideQuestionPanel());
        noticScript.ShowNotic(noticString);
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(audio_Manager.PlayRandom_IN_NPC_Category(result));
        yield return new WaitForSeconds(2f);
        scene_TransitionImage.SetTrigger("Hide");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);

        yield return null;

    }






    }

