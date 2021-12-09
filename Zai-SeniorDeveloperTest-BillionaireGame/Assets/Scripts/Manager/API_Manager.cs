using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class API_Manager : MonoBehaviour
{
    private string get_Question_By_ID_API = "https://zai-test-billionaire-game.herokuapp.com/get_question_by_id?question_id=";
    private string get_QuestionID_List_API = "https://zai-test-billionaire-game.herokuapp.com/get_all_questionID";
    public Full_QuestionData current_Question;
    public QuestionID_Info_List QuestionID_List;


    // ฟังชั้น Get เอา ID และ ตะแนนของคำถามทั้งหมดใน Server มาเพื่อใช้ในการ Random คำถาม
    public IEnumerator Get_QuestionList()
    {
        var uri = get_QuestionID_List_API;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                var result = webRequest.downloadHandler.text;
                //Debug.Log("{"+"\"question_list\":" + result+"}" );
                yield return QuestionID_List = JsonUtility.FromJson<QuestionID_Info_List>("{" + "\"question_list\":" + result + "}");
            }
        }
    }



    // ฟังชั้น Get โดยใส่ ID ของคำถามเข้าไปจะได้เป็นข้อมูลทั้งหมดของคำถามนั้น
    public IEnumerator Get_Question_By_ID(string question_ID)
    {
        var uri = get_Question_By_ID_API + question_ID;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {

                var result = webRequest.downloadHandler.text;
                //Debug.Log(result);
                yield return current_Question = JsonUtility.FromJson<Full_QuestionData>(result.Trim('[', ']'));
            }
        }
    }


    // ฟังชั้น Get คำถามโดยแรนด้อมตามคะแนนที่ใส่ลงไปใน Function
    public IEnumerator GetRandomQuestionByScore( float score) 
    {
        QuestionID_Info_List temp_QIDL = new QuestionID_Info_List();
        temp_QIDL.question_list = new List<QuestionID_Info>();

        foreach (var x in QuestionID_List.question_list)
        {
            if (x.score == score)
            {
                temp_QIDL.question_list.Add(x);
            }
        }

        int random = Random.Range(0, temp_QIDL.question_list.Count);

        yield return StartCoroutine(Get_Question_By_ID(temp_QIDL.question_list[random].question_ID));
    }

}
