using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Full_QuestionData : QuestionID_Info
{

    public string question;
    public string answer;
    public string choiceA;
    public string choiceB;
    public string choiceC;
    public string choiceD;

}

[System.Serializable]
public class QuestionID_Info 
{
    public string question_ID;
    public int score;
}


[System.Serializable]
public class QuestionID_Info_List
{
    public List<QuestionID_Info> question_list;

}

[System.Serializable]
public class QuestionScore : Random_Chance
{
    public int score;
   
}


[System.Serializable]
public class Helper_AnswerChance : Random_Chance
{
    public string answer;
}


[System.Serializable]
public class Random_Chance 
{
    public float Chance;
    [HideInInspector]
    public float minChance;
    [HideInInspector]
    public float maxChance;
}