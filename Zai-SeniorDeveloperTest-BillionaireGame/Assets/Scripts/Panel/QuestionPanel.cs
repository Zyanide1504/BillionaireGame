using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanel : MonoBehaviour
{
    private GameManager gameManager;
    public Text question_Text;
    public List<Button> ans_Buttonlist;

    public void Start()
    {
        gameManager = GameManager.Instance;
        HideQuestionPanel();
    }

    public void ShowQuestionPanel() 
    {
        this.gameObject.SetActive(true);

    }

    public void HideQuestionPanel()
    {
        this.gameObject.SetActive(false);

    }

    public void Ini_Button() 
    {
        ResetAllButtonColor();
        ShowAllButton();
    }

    public void ResetAllButtonColor()
    {
        foreach (var x in ans_Buttonlist)
        {
            x.GetComponent<Image>().color = new Color32(255,255,255,255);
        }
    }

    public void ShowAllButton()
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
            x.interactable = interact;
        }
    }


    public IEnumerator Setup_QuestionPanel()
    {
        ShowQuestionPanel();
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


        Set_AnswerButtonInteract(true);
        yield return null;
    }

    public void SentAnswer(string answer) 
    {
        Set_AnswerButtonInteract(false);
        gameManager.OnSelectAnswer(answer);
    }
}
