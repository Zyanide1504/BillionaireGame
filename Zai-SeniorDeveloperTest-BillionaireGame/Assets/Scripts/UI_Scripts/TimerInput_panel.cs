﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerInput_panel : MonoBehaviour
{
    public float lerpSnapSpeed;

    public GameObject TimeNumber_Prefab;
    public int max_number;

    public RectTransform panel;
    public List<GameObject> contentObj;
    public RectTransform center;

    private float[] distance;
    private bool dragging = false;
    private int objDistance;
    private int minObjnum;

    public string current_input;


    void Start()
    {
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

    // Update is called once per frame
    void Update()
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

       
    }

    void LerpToObj(int position)
    {
        float newY = Mathf.Lerp(panel.anchoredPosition.y, position, Time.deltaTime / lerpSnapSpeed);
        Vector2 newPosition = new Vector2(panel.anchoredPosition.x, newY);
       
        panel.anchoredPosition = newPosition;

    }

    public void StartDrag() 
    {

        dragging = true;

    }

    public void StopDrag() 
    {
        dragging = false;
        current_input = contentObj[minObjnum].GetComponent<Text>().text;
    }

}