﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper_Manager : MonoBehaviour
{
    GameManager gameManager;
    public List<Helper_AnswerChance> helper_ansChance;
    string[] choice = { "A", "B", "C", "D" };
    void Start()
    {
        gameManager = GameManager.Instance;

        //uncomment เพื่อทดสอบการ random คำตอบของ NPC
        //for(int i = 0; i < 10; i++) { Debug.Log(RandomAnswer()); }

    }

    // Update is called once per frame
    public void ExtendTime() 
    {
        gameManager.timer_manager.timer = gameManager.timer_manager.timer * 2;
    }

    public void Remove_2Choice() 
    {
        
        List<GameObject> select_remove = new List<GameObject>();

        while (select_remove.Count != 2) 
        {
            int randomIndex = Random.RandomRange(0, choice.Length-1);
            var temp_GameObj = gameManager.question_Panel.ans_Buttonlist[randomIndex].gameObject;

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
            x.SetActive(false);
        }
    }

    public void AskNPC() 
    {

        switch (RandomAnswer()) 
        {
            case "IDK":

                Debug.Log("I don't know!!!");

                break;

            case "WrongANS":

                string temp_wrongAns = "";
                string real_ans = gameManager.api_Manager.current_Question.answer;
                List<GameObject> select_wrongAns = new List<GameObject>();

                foreach (var x in gameManager.question_Panel.ans_Buttonlist) 
                {
                    if (x.GetComponent<AnswerButton>().answer != real_ans && x.gameObject.active) 
                    {
                        select_wrongAns.Add(x.gameObject);
                    }
                
                }

                temp_wrongAns = select_wrongAns[Random.RandomRange(0, select_wrongAns.Count - 1)].GetComponent<AnswerButton>().answer;
                Debug.Log(temp_wrongAns);

                gameManager.question_Panel.hintButton(System.Array.IndexOf(choice, temp_wrongAns));

                break;

            case "ANS":

                var temp_ans = gameManager.api_Manager.current_Question.answer;
                gameManager.question_Panel.hintButton(System.Array.IndexOf(choice, temp_ans));

                break;

        }
    }


    public string RandomAnswer()
    {
        
        float chanceSum = 0;

        for (int i = 0; i < helper_ansChance.Count; i++)
        {
            var current = helper_ansChance[i];
            chanceSum += current.Chance;
            if (i == 0)
            {
                current.minChance = 0;
                current.maxChance = current.Chance;

            }
            else
            {

                current.minChance = helper_ansChance[i - 1].maxChance;
                current.maxChance = current.minChance + current.Chance;

            }

        }

        float rand = Random.Range(0, chanceSum);

        for (int i = 0; i < helper_ansChance.Count; i++)
        {
            var current = helper_ansChance[i];

            if (rand >= current.minChance && rand < current.maxChance)
            {
                Debug.Log(current.answer);
                return current.answer;
            }
        }

        return "NAN";

    }
}