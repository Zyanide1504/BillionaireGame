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
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        
    }

    public void ShowTimer() 
    {
        animator.SetTrigger("ShowTimer");
    }

    public void HideTimer()
    {
        animator.SetTrigger("HideTimer");
    }

    public IEnumerator GameOverCountDown() 
    {
        yield return new WaitForSeconds(gameManager.delay_countDown);
        ShowTimer();
        timer = gameManager.gameOverTime;

        while (timer > 0)
        {
            yield return new WaitForSeconds(0.005f);
            timer -= Time.deltaTime;
            int showtime = (int)timer;
            timer_Text.text = showtime.ToString();
        }

       gameManager.OnGameOver();
    }


}
