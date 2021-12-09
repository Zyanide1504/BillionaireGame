using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;
using UnityEngine.SceneManagement;




public class TimerManager : MonoBehaviour
{
    public Text SaveTimer_Text;
    public Text CountDown_Text;
    public Button Start_Button;
    public TimerInput_panel hourInput;
    public TimerInput_panel minuteInput;
    public TimerEdit_Panel timerEdit_Panel;
    public Animator scene_TransitonAnim;
    private DateTime CurrentTimer;

    private NotificationSender notic_Sender;
    public static TimerManager Instance { get; private set; }
    bool finish_CountDown;

    public void Awake()
    {
        Instance = this;
    }
    public void Start()
    {

        notic_Sender = new NotificationSender();
        notic_Sender.SetupNotificationChanel();
      
        // เชคว่ามีการ Save เวลามาก่อนหรือไม่ หากไม่ให้ set เป็น 00:00 หากมีให้แสดงผลเวลาเริ่มต้นตามนั้น
        if (PlayerPrefs.HasKey("Timer"))
        {
            CurrentTimer = DateTime.Parse(PlayerPrefs.GetString("Timer"));
            Debug.Log(CurrentTimer);
            SetTimer(CurrentTimer);
           
        }
        else 
        {
            CurrentTimer = DateTime.Parse("00:00");
            SetTimer(CurrentTimer);

        }
        SaveTimer_Text.text = CurrentTimer.TimeOfDay.ToString();
    }

    public void Update()
    {
        CountDownTimmerUpdate();
    }


    // Function ใช้ Update ตัวนับถอยหลัง
    public void CountDownTimmerUpdate() 
    {
        if (!finish_CountDown)
        {
            var timeNow = DateTime.Now.TimeOfDay;
            var timer = CurrentTimer.TimeOfDay;
            var timeDifference = CurrentTimer - timeNow;
            string time = timeDifference.ToString("H:mm:ss");
          
            CountDown_Text.text = time;

            if (DateTime.Now > CurrentTimer) 
            {
                finish_CountDown = true;
                CountDown_Text.text = "00:00:00";
                
            }

        }
        UpdateStartButtonStatus();
    }

    // ฟังชั่นไว้อัพเดทสภานะของปุ่มว่าสามารถเริ่มเกมได้หรือยัง
    public void UpdateStartButtonStatus() 
    {  
        Start_Button.interactable = finish_CountDown;
    }

    public void SaveTimer() 
    {
        // บันทึกเวลา
        var combind_TimeInput = hourInput.current_input + ":" + minuteInput.current_input;
        Debug.Log(combind_TimeInput);
        PlayerPrefs.SetString("Timer", combind_TimeInput);
        CurrentTimer = DateTime.Parse(combind_TimeInput);
        SaveTimer_Text.text = CurrentTimer.TimeOfDay.ToString();
        finish_CountDown = false;

        var timeDifference = CurrentTimer - DateTime.Now.TimeOfDay;


        // ยิง Notification หากเวลาที่ตั้งมากกว่าเวลาปัจจุบัญ
#if UNITY_ANDROID
        if (DateTime.Now < CurrentTimer)
        {
            notic_Sender.SetSentNotification(
                "เวลาที่คุณตั้งไว้ " + CurrentTimer, 
                "ถึงเวลาตามที่ตั้งไว้แล้วมาสนุกกับเกม " + Application.productName + "กัน!!!!",
                CurrentTimer
                );
        }
#endif

    }

    // ฟังชั่นเพื่อใช้ Set ตัว inputTimer ให้แสดงผลตาม
    public void SetTimer(DateTime time) 
    {
        var temp_CurrentTimer = time;
        hourInput.SetCurrentInput(temp_CurrentTimer.Hour);
        minuteInput.SetCurrentInput(temp_CurrentTimer.Minute);
    }

    // ฟังชั่นเพื่อใช้ เริ่มเกม
    public void StartGame() 
    {
        StartCoroutine(IE_StartGame());
    }

    public IEnumerator IE_StartGame() 
    {
        scene_TransitonAnim.SetTrigger("Hide");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
