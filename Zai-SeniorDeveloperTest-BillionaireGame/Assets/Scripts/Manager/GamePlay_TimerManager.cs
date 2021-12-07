using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay_TimerManager : MonoBehaviour
{
    private GameManager gameManager;
    public Text timer_Text;
    public float timer;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        gameObject.SetActive(false);
    }

    public void ShowTimer() 
    {
        gameObject.SetActive(true);
    }

    public void HideTimer()
    {
        gameObject.SetActive(false);
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
