using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerEdit_Panel : MonoBehaviour
{
    public InputField hour_InputField;
    public InputField minute_InputField;
    public TimerManager timeMN;


    public void Start()
    {
        timeMN = TimerManager.Instance;
        this.gameObject.SetActive(false);
    }

    // ฟังชั่นเพื่อใช้ เปิดหน้าแก้ไขเวลา
    public void Show()
    {
        Debug.Log(timeMN);
        this.gameObject.SetActive(true);
        hour_InputField.text = timeMN.hourInput.current_input;
        minute_InputField.text = timeMN.minuteInput.current_input;
    }


    // ฟังชั่นยืนยันแก้ไขเวลาของ Time edit panel
    public void confirmEdit() 
    {
        try
        {
            timeMN.SetTimer(DateTime.Parse(hour_InputField.text + ":" + minute_InputField.text));
            this.gameObject.SetActive(false);
        }
        catch
        {

            Debug.Log("Wrong Time format");

        }
    }
}
