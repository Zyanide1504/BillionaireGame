using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public string answer;
    public Button button;
    public Animator anim;
    public bool isShow;
    private GameManager gameManager;

    public void Start()
    {
        gameManager = GameManager.Instance;
    }

    // ฟังชั่นแสดงตัวเลือก
    public void ShowAnswer() 
    {
        if (!isShow) 
        {
            anim.SetTrigger("ShowAnswer");
            isShow = true;
        }
 
    }

    // ฟังชั่นซ่อนตัวเลือก
    public IEnumerator HideAnswer()
    {
        if (isShow)
        {
            anim.SetTrigger("HideAnswer");
            isShow = false;
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
           
        }

        yield return null;

    }

    // ฟังชั่นส่งคำตอบไป Game Manager;
    public void SendAnswer()
    {
        gameManager.OnSelectAnswer(answer);
    }

   

}
