using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Notifications.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TimerManager : MonoBehaviour
{
    public Text debugText;
    public TimerInput_panel hourInput;
    public TimerInput_panel minuteInput;
    private DateTime CurrentTimer;
    private AndroidNotificationChannel defaultNotificationChanel;
    private Int32 notic_identifier;

    public void Start()
    {
      
        if (PlayerPrefs.HasKey("Timer"))
        {
            CurrentTimer = DateTime.Parse(PlayerPrefs.GetString("Timer"));
            hourInput.SetCurrentInput(CurrentTimer.Hour);
            minuteInput.SetCurrentInput(CurrentTimer.Minute);

        }
        else 
        {
            hourInput.SetCurrentInput(0);
            minuteInput.SetCurrentInput(0);
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

    public void SaveTimer() 
    {
        var combind_TimeInput = hourInput.current_input + ":" + minuteInput.current_input;
        PlayerPrefs.SetString("Timer", combind_TimeInput);
        CurrentTimer = DateTime.Parse(combind_TimeInput);

#if UNITY_ANDROID


        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Test Notification!!!",
            Text = "This is a test notification!!!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = CurrentTimer,
        };

        debugText.text = AndroidNotificationCenter.CheckScheduledNotificationStatus(notic_identifier).ToString();

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

    public void StartGame() 
    {
        if (DateTime.Now > CurrentTimer)
        {
            SceneManager.LoadScene(1);
        }
        else 
        {
        
        }
    
    }
}
