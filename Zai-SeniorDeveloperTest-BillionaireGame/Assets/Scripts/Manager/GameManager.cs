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

    // คำสั่งที่เรียกเพื่อเริ่มต้นเกม
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

    // คำสั่งไว้ให้ QuestionCard เรียกกลับมาเมื่อผู้เล่นเลือกการ์ดแล้ว
    public void OnCardSelect(int score) 
    {
        StartCoroutine(IE_OnCardSelect(score));
    }
    // ชุดคำสั่งที่รันเมื่อผู้เล่นเลือกการ์ดแล้ว มีการเรียกไปที่ ApiManager ให้ยิง API ไปเอาคำถามจาก Server เมื่อได้มาแล้วทำการ Animate เก็บไพ่ แล้วเริ่ม Setup หน้าคำถาม
    public IEnumerator IE_OnCardSelect(int score) 
    {
        questionCard_Panel.Setinteract_AllCard(false);

        yield return StartCoroutine(api_Manager.GetRandomQuestionByScore(score));

        yield return StartCoroutine(questionCard_Panel.HideQuestionCardPanel());

        currentCard.SetScore(score);
        StartCoroutine(currentCard.ShowAndFlip());

        question_Panel.gameObject.SetActive(true); 
        yield return StartCoroutine(question_Panel.Setup_QuestionPanel());
        gameOverCountDown = StartCoroutine(timer_manager.GameOverCountDown());

        yield return null;
    }

    // คำสั่งไว้ให้ QuestionCard เรียกกลับมาเมื่อผู้เล่นเลือกคำตอบแล้ว
    public void OnSelectAnswer(string answer) 
    {
        question_Panel.SetAll_AnswerButtonInteract(false);
        StopCoroutine(gameOverCountDown);
        StartCoroutine(IE_OnSelectAnswer(answer));
    }

    // ชุดคำสั่งที่รันเมื่อผู้เล่นเลือกคำตอบทำการเชคคำตอบ ถ้าถูกให้เพิ่มคะแนนแล้วเชคว่าคะแนนถึงคะแนนที่จะชนะได้รึงยังหากยังให้ ซ่อนหน้าคำถามแล้ว SetUp หน้าเลือกการ์ด ต่อ แต่ถ้าชนะหรือคำตอบผิก ก็จะเป็นการจบเกม
    public IEnumerator IE_OnSelectAnswer(string answer)
    {
        helper_Manager.HideHelperBar();
        timer_manager.HideTimer();

        if (answer == api_Manager.current_Question.answer)
        {
            score_Manager.AddScore(api_Manager.current_Question.score);
            yield return StartCoroutine(audio_Manager.PlayRandom_IN_NPC_Category("RightAns"));

            if (!score_Manager.CheckScoreWinCondition())
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


    //เรียกเมื่อตรงเงื่อนไขชนะ
    public void OnWin() 
    {
        PlayerPrefs.SetInt("WinCount",PlayerPrefs.GetInt("WinCount") +1);
        StartCoroutine(IE_OnGameEnd("Win", "ชนะแล้ว"));
    }


    //เรียกเมื่อตรงเงื่อนไขแพ้
    public void OnGameOver()
    {
        StartCoroutine(IE_OnGameEnd("GameOver", "แพ้แล้ว"));
    }


    //เรียกเมื่อตรงเงื่อนไขจบเกม
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

