using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Notifications.Android;


public class TimerManager : MonoBehaviour
{
    public TimerInput_panel hourInput;
    public TimerInput_panel minuteInput;
    private DateTime CurrentTimer;
    private AndroidNotificationChannel defaultNotificationChanel;

    public void Start()
    {

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
        CurrentTimer = DateTime.Parse(hourInput.current_input + ":" + minuteInput.current_input);

#if UNITY_ANDROID
        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Test Notification!!!",
            Text = "This is a test notification!!!",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = CurrentTimer,
        };

        AndroidNotificationCenter.SendNotification(notification, "default_channel");
#endif

    }

    public void StartGame() 
    {
        if (DateTime.Now > CurrentTimer)
        {


        }
        else 
        {
        
        }
    
    }
}
