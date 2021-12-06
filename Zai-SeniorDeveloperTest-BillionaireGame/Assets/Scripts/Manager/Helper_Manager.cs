using System.Collections;
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
        Debug.Log(System.Array.IndexOf(choice,"C"));
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

            if (choice[randomIndex] != gameManager.api_Manager.current_Question.answer) 
            {
                var temp_GameObj = gameManager.question_Panel.ans_Buttonlist[randomIndex].gameObject;
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

                var temp_wrongAns = "";
                int randomIndex = 0;

                while (temp_wrongAns == "") 
                {
                    randomIndex = Random.RandomRange(0, choice.Length - 1);

                    if (choice[randomIndex] != gameManager.api_Manager.current_Question.answer) 
                    {
                        temp_wrongAns = choice[randomIndex];
                    }

                }

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
