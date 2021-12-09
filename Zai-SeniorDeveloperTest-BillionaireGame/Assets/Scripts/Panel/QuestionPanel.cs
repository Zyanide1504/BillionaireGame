using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanel : MonoBehaviour
{
    private GameManager gameManager;
    public Animator question_anim;
    public Text question_Text;
    public List<AnswerButton> ans_Buttonlist;

    public void Start()
    {
        gameManager = GameManager.Instance;
        gameObject.SetActive(false);
    }

    // ฟังชันแสดง panel คำถาม
    public IEnumerator ShowQuestionPanel() 
    {
        question_anim.SetTrigger("ShowQuestion");

        yield return new WaitForSeconds(1);

        for (int i = 0; i < ans_Buttonlist.Count; i++) 
        {
            ans_Buttonlist[i].GetComponent<AnswerButton>().ShowAnswer();
            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }

    // ฟังชัน ซ่อน panel คำถาม
    public IEnumerator HideQuestionPanel()
    {

        for (int i = ans_Buttonlist.Count-1; i > -1; i--)
        {
            
            StartCoroutine(ans_Buttonlist[i].GetComponent<AnswerButton>().HideAnswer());
            yield return new WaitForSeconds(0.25f);
        }

        question_anim.SetTrigger("HideQuestion");

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }


    //ตั่งค่าเริ่มต้นให้ปุ่มคำคอบ
    public void Ini_Button() 
    {
        ResetAllButtonColor();
        SetActiveAllButton();
    }

    //เปลี่ยนสีปุ่มคอบให้กลับมาเป็นค่าเริ่มต้น
    public void ResetAllButtonColor()
    {
        foreach (var x in ans_Buttonlist)
        {
            x.GetComponent<Image>().color = new Color32(255,255,255,255);
        }
    }

    //สั่ง SetActive ปุ่มคำตอบทั้งหมด
    public void SetActiveAllButton()
    {
        foreach (var x in ans_Buttonlist)
        {
            x.gameObject.SetActive(true);
        }
    }

    //ฟังชั่นบอกใบ้ที่จะเปลี่ยนสีปุ่มที่เลือกให้เป็นสีเขียว
    public void hintButton(string ans) 
    {
        foreach (var x in ans_Buttonlist)
        {

            if (x.answer == ans) 
            {
              x.GetComponent<Image>().color = new Color32(161, 245, 161, 255);
                return;
            }
        }
       
    }

    //ฟังชั่นตั้ง InterAct ให้ปุ่มทั้งหมด
    public void SetAll_AnswerButtonInteract(bool interact) 
    {
        foreach (var x in ans_Buttonlist)
        {
            x.button.interactable = interact;
        }
    }


    // Setup หน้าคำถาม
    public IEnumerator Setup_QuestionPanel()
    {
        StartCoroutine(gameManager.audio_Manager.PlayRandom_IN_NPC_Category("SeeQuestion"));
        Ini_Button();
        question_Text.text = gameManager.api_Manager.current_Question.question;
        question_Text.GetComponent<Text_ForThaiFont>().AdjustText();
        ans_Buttonlist[0].transform.GetComponentInChildren<Text>().text = "A. "+gameManager.api_Manager.current_Question.choiceA;
        ans_Buttonlist[1].transform.GetComponentInChildren<Text>().text = "B. "+ gameManager.api_Manager.current_Question.choiceB;
        ans_Buttonlist[2].transform.GetComponentInChildren<Text>().text = "C. "+gameManager.api_Manager.current_Question.choiceC;
        ans_Buttonlist[3].transform.GetComponentInChildren<Text>().text = "D. "+gameManager.api_Manager.current_Question.choiceD;
        foreach (var x in ans_Buttonlist)
        {
            x.transform.GetComponentInChildren<Text>().GetComponent<Text_ForThaiFont>().AdjustText();
        }

        gameObject.SetActive(true);
        yield return StartCoroutine(ShowQuestionPanel());
        SetAll_AnswerButtonInteract(true);
        yield return null;
    }

    //ส่งคำตอบไปให้ GameManager เชค

}
