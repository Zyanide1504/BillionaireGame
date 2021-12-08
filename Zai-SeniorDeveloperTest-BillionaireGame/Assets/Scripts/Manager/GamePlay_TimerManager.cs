using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay_TimerManager : MonoBehaviour
{
    private GameManager gameManager;
    public Text timer_Text;
    public Animator animator;
    public float timer;

    private bool isShow;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        
    }

    public void ShowTimer() 
    {
        if (!isShow)
        {
            animator.SetTrigger("ShowTimer");
            isShow = true;
        }
    }

    public void HideTimer()
    {
        if (isShow)
        {
            animator.SetTrigger("HideTimer");
            isShow = false;
        }
    }

    public IEnumerator GameOverCountDown() 
    {
        yield return new WaitForSeconds(gameManager.delay_countDown);
        StartCoroutine(gameManager.audio_Manager.PlayRandom_IN_NPC_Category("StartCountDown"));
        ShowTimer();
        gameManager.helper_Manager.ShowHelperBar();
        timer = gameManager.gameOverTime;

        while (timer > 0)
        {
            yield return new WaitForSeconds(0.005f);
            timer -= Time.deltaTime;
            int showtime = (int)timer;
            timer_Text.text = showtime.ToString();
        }

        gameManager.question_Panel.Set_AnswerButtonInteract(false);
        yield return gameManager.audio_Manager.PlayRandom_IN_NPC_Category("TimeOut");
        gameManager.OnGameOver();
    }


}
