using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.Notifications.Android;
using UnityEngine.SceneManagement;




public class TimerManager : MonoBehaviour
{

    public Text CountDown_Text;
    public Button Start_Button;
    public TimerInput_panel hourInput;
    public TimerInput_panel minuteInput;
    public TimerEdit_Panel timerEdit_Panel;
    private DateTime CurrentTimer;
    private AndroidNotificationChannel defaultNotificationChanel;
    private Int32 notic_identifier;

    bool finish_CountDown;

    public void Start()
    {
      
        if (PlayerPrefs.HasKey("Timer"))
        {
            CurrentTimer = DateTime.Parse(PlayerPrefs.GetString("Timer"));
            SetTimer(CurrentTimer);

        }
        else 
        {
            SetTimer(DateTime.Parse("00:00"));
            Debug.Log("null"); 

        }


#if UNITY_ANDROID
        defaultNotificationChanel = new AndroidNotificationChannel() 
        {
            Id = "default_channel",
            Name = "Default Channel",
            Description = "For Generic notification",
            Importance = Importance.Default,
        };

         AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChanel);
#endif

    }

    public void Update()
    {
        CountDownTimmerUpdate();
        UpdateStartButtonStatus();
    }

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
    }


    public void UpdateStartButtonStatus() 
    {  
        Start_Button.interactable = finish_CountDown;
    }

    public void SaveTimer() 
    {
        var combind_TimeInput = hourInput.current_input + ":" + minuteInput.current_input;
        PlayerPrefs.SetString("Timer", combind_TimeInput);
        CurrentTimer = DateTime.Parse(combind_TimeInput);
        finish_CountDown = false;
#if UNITY_ANDROID


        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Test Notification!!!",
            Text = "This is a test notification!!!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = CurrentTimer,
        };


        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notic_identifier) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelNotification(notic_identifier);
            notic_identifier = AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }
        else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notic_identifier) == NotificationStatus.Delivered)
        {
            AndroidNotificationCenter.CancelNotification(notic_identifier);
            notic_identifier = AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }
        else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(notic_identifier) == NotificationStatus.Unknown)
        {
            notic_identifier = AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }       
#endif

    }


    public void SetTimer(DateTime time) 
    {
        var temp_CurrentTimer = time;
        hourInput.SetCurrentInput(temp_CurrentTimer.Hour);
        minuteInput.SetCurrentInput(temp_CurrentTimer.Minute);
    }


    public void TurnOnTimerEditPanel() 
    {
        timerEdit_Panel.gameObject.SetActive(true);
        timerEdit_Panel.hour_InputField.text = hourInput.current_input;
        timerEdit_Panel.minute_InputField.text = minuteInput.current_input;
    }

    public void ConfirmTimerEdit() 
    {
        try 
        {
            SetTimer(DateTime.Parse(timerEdit_Panel.hour_InputField.text + ":" + timerEdit_Panel.minute_InputField.text));
            timerEdit_Panel.gameObject.SetActive(false);
        } 
        catch 
        { 
        
        
        }
    
    }


    public void StartGame() 
    {
            SceneManager.LoadScene(1);
    }
}
