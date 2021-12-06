using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanel : MonoBehaviour
{
    public GameManager gameManager;
    public Text question_Text;
    public List<Button> ans_Buttonlist;

    public void Start()
    {
        Debug.Log("IN");
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
        question_Text.text = gameManager.api_Manager.current_Question.question;
        ans_Buttonlist[0].transform.GetComponentInChildren<Text>().text = gameManager.api_Manager.current_Question.choiceA;
        ans_Buttonlist[1].transform.GetComponentInChildren<Text>().text = gameManager.api_Manager.current_Question.choiceB;
        ans_Buttonlist[2].transform.GetComponentInChildren<Text>().text = gameManager.api_Manager.current_Question.choiceC;
        ans_Buttonlist[3].transform.GetComponentInChildren<Text>().text = gameManager.api_Manager.current_Question.choiceD;

        Set_AnswerButtonInteract(true);
        yield return null;
    }

    public void SentAnswer(string answer) 
    {
        Set_AnswerButtonInteract(false);
        gameManager.OnSelectAnswer(answer);
    }
}
