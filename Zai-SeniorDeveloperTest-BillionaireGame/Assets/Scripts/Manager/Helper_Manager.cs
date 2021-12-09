using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_Manager : MonoBehaviour
{
    GameManager gameManager;
    public Animator animator;
    string[] choice = { "A", "B", "C", "D" };

    private bool isShow;
    void Start()
    {
        gameManager = GameManager.Instance;

        //uncomment เพื่อทดสอบการ random คำตอบของ NPC
        //for(int i = 0; i < 10; i++) { Debug.Log(RandomAnswer()); }

    }


    public void ToggleShow() 
    {

        if (!isShow)
        {
            ShowHelperBar();

        }
        else 
        {
            HideHelperBar();
        }
    }

    public void ShowHelperBar() 
    {
       
        if (!isShow)
        {
            animator.SetTrigger("ShowHelper");
            isShow = true;
        }
    }

    public void HideHelperBar()
    {
        if (isShow)
        {
            animator.SetTrigger("HideHelper");
            isShow = false;
        }
    }


    public void ExtendTime() 
    {
        gameManager.timer_manager.timer = gameManager.timer_manager.timer * 2;
    }

    public void Remove_2Choice() 
    {
        
        List<AnswerButton> select_remove = new List<AnswerButton>();

        while (select_remove.Count != 2) 
        {
            int randomIndex = Random.RandomRange(0, choice.Length-1);
            var temp_GameObj = gameManager.question_Panel.ans_Buttonlist[randomIndex];

            if (temp_GameObj.GetComponent<AnswerButton>().answer != gameManager.api_Manager.current_Question.answer) 
            {
              
                if (!select_remove.Contains(temp_GameObj))
                {
                    select_remove.Add(temp_GameObj);
                }
            }
        }

        foreach (var x in select_remove) 
        {
            StartCoroutine(x.HideAnswer());
        }
    }

    public void AskNPC() 
    {
        var utility = new Utility();
        var result = utility.RandomWithChance(gameManager.helper_ansChance);

        switch (result) 
        {
            case "IDK":

                StartCoroutine(gameManager.audio_Manager.PlayRandom_IN_NPC_Category("IDK"));

                break;

            case "WrongANS":


                StartCoroutine(gameManager.audio_Manager.PlayRandom_IN_NPC_Category("GiveWrongAns"));
                HintWorngAnswer();

                break;

            case "ANS":


                StartCoroutine(gameManager.audio_Manager.PlayRandom_IN_NPC_Category("GiveRightAns"));
                var temp_ans = gameManager.api_Manager.current_Question.answer;
                gameManager.question_Panel.hintButton(System.Array.IndexOf(choice, temp_ans));

                break;

        }
    }


    public void HintWorngAnswer() 
    {
        string temp_wrongAns = "";
        string real_ans = gameManager.api_Manager.current_Question.answer;
        List<GameObject> select_wrongAns = new List<GameObject>();

        foreach (var x in gameManager.question_Panel.ans_Buttonlist)
        {
            if (x.GetComponent<AnswerButton>().answer != real_ans && x.isShow)
            {
                select_wrongAns.Add(x.gameObject);
            }

        }
        temp_wrongAns = select_wrongAns[Random.RandomRange(0, select_wrongAns.Count)].GetComponent<AnswerButton>().answer;
        gameManager.question_Panel.hintButton(System.Array.IndexOf(choice, temp_wrongAns));
    }

}
