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

    public IEnumerator HideQuestionPanel()
    {

        for (int i = ans_Buttonlist.Count-1; i > -1; i--)
        {
            Debug.Log(i);
            StartCoroutine(ans_Buttonlist[i].GetComponent<AnswerButton>().HideAnswer());
            yield return new WaitForSeconds(0.25f);
        }

        question_anim.SetTrigger("HideQuestion");

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }

    public void Ini_Button() 
    {
        ans_Buttonlist[0].GetComponent<AnswerButton>().answer = "A";
        ans_Buttonlist[1].GetComponent<AnswerButton>().answer = "B";
        ans_Buttonlist[2].GetComponent<AnswerButton>().answer = "C";
        ans_Buttonlist[3].GetComponent<AnswerButton>().answer = "D";

        ResetAllButtonColor();
        SetActiveAllButton();
    }

    public void ResetAllButtonColor()
    {
        foreach (var x in ans_Buttonlist)
        {
            x.GetComponent<Image>().color = new Color32(255,255,255,255);
        }
    }

    public void SetActiveAllButton()
    {
        foreach (var x in ans_Buttonlist)
        {
            x.gameObject.SetActive(true);
        }
    }

    public void hintButton(int buttonIndex) 
    {
        ans_Buttonlist[buttonIndex].GetComponent<Image>().color = new Color32(161,245,161,255);
    }


    public void Set_AnswerButtonInteract(bool interact) 
    {
        foreach (var x in ans_Buttonlist)
        {
            x.button.interactable = interact;
        }
    }


    public IEnumerator Setup_QuestionPanel()
    {
        
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
        Set_AnswerButtonInteract(true);
        yield return null;
    }

    public void SentAnswer(string answer) 
    {
        Set_AnswerButtonInteract(false);
        gameManager.OnSelectAnswer(answer);
    }
}
