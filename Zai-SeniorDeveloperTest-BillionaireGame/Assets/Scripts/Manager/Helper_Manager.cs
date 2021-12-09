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

    // ฟังชั่น Toggle การแสดงตัววช่วย
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

    // ฟังชั่น แสดงตัววช่วย
    public void ShowHelperBar() 
    {
       
        if (!isShow)
        {
            animator.SetTrigger("ShowHelper");
            isShow = true;
        }
    }

    // ฟังชั่น ซ่อนตัวช่วย
    public void HideHelperBar()
    {
        if (isShow)
        {
            animator.SetTrigger("HideHelper");
            isShow = false;
        }
    }

    // ฟังชั่นขยายเวลา เวลาใช้สามารถเอา obj ที่มี Scrip นี้ไป reference แล้วเรียกได้เลิย หรือจะเรียกผ่าน instance ขอ GameManager ก็ได้เช่นกัน เช่น gameManager.helper_Manager.ExtendTime(3); เวลาก็จะเพิ่มเป็น 3 เท่า
    public void ExtendTime(float multiple) 
    {

        gameManager.timer_manager.timer = gameManager.timer_manager.timer * multiple;
    }

    // ตัดคำตอบออกไปสองข้อ วีธีใช้เหมือน ExtendTime()
    public void Remove_2Choice() 
    {
        
        List<AnswerButton> select_remove = new List<AnswerButton>();

        while (select_remove.Count != 2) 
        {
            int randomIndex = Random.RandomRange(0, gameManager.question_Panel.ans_Buttonlist.Count);
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


    // ถาม NPC  วีธีใช้เหมือน ExtendTime() และ Remove_2Choice()
    public void AskNPC() 
    {
    
        var result = Utility.RandomWithChance(gameManager.helper_ansChance);

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
                gameManager.question_Panel.hintButton(temp_ans);
                

                break;

        }
    }

    //บอกคำตอบที่ผิด
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
        gameManager.question_Panel.hintButton(temp_wrongAns);
    }

}
