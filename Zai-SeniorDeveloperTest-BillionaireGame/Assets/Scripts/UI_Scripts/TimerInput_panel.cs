using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerInput_panel : MonoBehaviour
{
    public float lerpSnapSpeed;

    public GameObject TimeNumber_Prefab;
    public int max_number;
    [Space]
    public float alpha_fadeSpeed;
    public float max_alpha;
    public float min_alpha;
    [Space]
    public float scaling_Speed;
    public float max_TextScale;
    public float min_TextScale;

    public RectTransform panel;
    public List<GameObject> contentObj;
    public RectTransform center;

    private float[] distance;
    private bool dragging = false;
    private int objDistance;

    
    public int minObjnum = 0;

    public string current_input;


    void Awake()
    {
        // สร้าง obj ตัวเลขเวลาขึ้นมาตามที่ตั้งเช่นชั่วโมงมี 24 ชั่วโมง max_number = 24 ก็จะสร้างตัวเลขเวลาตั้งแต่ 00 - 23
        for (int i = 0; i < max_number; i++) 
        {
            var temp_TimeNumber = Instantiate(TimeNumber_Prefab, panel.transform).GetComponent<Text>();
           
            temp_TimeNumber.text = i.ToString();

            if (i < 10) 
            {
                temp_TimeNumber.text = "0" + temp_TimeNumber.text;
            }
            contentObj.Add(temp_TimeNumber.gameObject);

        }

        int objLength = contentObj.Count;
        distance = new float[objLength];
    }

    void Update()
    {
        UpdateInputAnimation(); 
    }


    // ฟังชั่นอัพเดทตำแหน่งของตัวเลข
    public void UpdateInputAnimation() 
    {
        if (objDistance == 0)
        {
            objDistance = (int)Mathf.Abs(contentObj[1].GetComponent<RectTransform>().anchoredPosition.y
           - contentObj[0].GetComponent<RectTransform>().anchoredPosition.y);
        }

        for (int i = 0; i < contentObj.Count; i++)
        {

            distance[i] = Mathf.Abs(center.transform.position.y - contentObj[i].transform.position.y);
        }

        float minDistance = Mathf.Min(distance);

        //เชคว่ามีการลากหรือไม่ถ้าไม่ให้ทำการไหลตัววเลขที่อยู่ไกล้ที่สุดมาเป็นตัวที่เลือก
        if (!dragging)
        {

            LerpToObj(minObjnum * objDistance);
        }
        else
        {

            for (int j = 0; j < contentObj.Count; j++)
            {
                if (minDistance == distance[j])
                {
                    minObjnum = j;
                }
            }
        }

        // ทำ Animate ให้ Alpha และ ขนาด น้อยลงเมื่อไม่ถูกเลือก
        for (int i = 0; i < contentObj.Count; i++)
        {
            var tempobj_text = contentObj[i].GetComponent<Text>();
            float destination_TextScale = 0;
            float destination_alpha = 0;

            if (minObjnum == i)
            {
                destination_TextScale = max_TextScale;
                destination_alpha = max_alpha;
            }
            else
            {
                destination_TextScale = min_TextScale;
                destination_alpha = min_alpha;
            }

            int currentFrontSize = (int)Mathf.Lerp(tempobj_text.fontSize, destination_TextScale, Time.deltaTime / alpha_fadeSpeed);
            tempobj_text.fontSize = currentFrontSize;

            var lerp_Alph = Mathf.Lerp(tempobj_text.color.a, destination_alpha, Time.deltaTime / alpha_fadeSpeed);
            tempobj_text.color = new Color(tempobj_text.color.r, tempobj_text.color.g, tempobj_text.color.b, lerp_Alph);
        }
    }




    //ฟังชันไว้ Lerp เลขให้ไปอยู่ในตำแหนน่งที่ต้องการ
    void LerpToObj(int position)
    {
        float newY = Mathf.Lerp(panel.anchoredPosition.y, position, Time.deltaTime / lerpSnapSpeed);
        Vector2 newPosition = new Vector2(panel.anchoredPosition.x, newY);
       
        panel.anchoredPosition = newPosition;

    }

    //ทำงานตอนลาก Set dragging ให้เป็น false เพื่อให้ทำการ Update ตำแหน่งเลข
    public void StartDrag() 
    {

        dragging = true;

    }

    //ทำงานตอนหยุดลาก Set dragging เป็น true เพื่อให้ หยุดอัพเดทตำแหน่ง
    public void StopDrag() 
    {
        dragging = false;
        UpdateCurrentInput();
    }

    // function อัพเดทว่าตอนนี้ Input เป็นเลขอะไร
    public void UpdateCurrentInput() 
    {
        current_input = contentObj[minObjnum].GetComponent<Text>().text;
    }



    //Set ค่า Input ของ TimerInput ให้เป็น เลขที่ต้องการ
    public void SetCurrentInput(int index) 
    {
        minObjnum = index;
        UpdateCurrentInput();
    }

}
