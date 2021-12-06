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
                Debug.Log(result);
                yield return current_Question = JsonUtility.FromJson<Full_QuestionData>(result.Trim('[',']'));
            }
        }
    }

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
                Debug.Log("{"+"\"question_list\":" + result+"}" );
                yield return QuestionID_List = JsonUtility.FromJson<QuestionID_Info_List>("{" + "\"question_list\":" + result + "}");
            }
        }
    }

}
