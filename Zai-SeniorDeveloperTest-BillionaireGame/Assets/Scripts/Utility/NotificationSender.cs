using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;

public class NotificationSender 
{
    private Int32 notic_identifier;
    private AndroidNotificationChannel defaultNotificationChanel;



    // ฟังชั้นไว้ สร้าง NotificationChannel
    public void SetupNotificationChanel()
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



    // ฟังชั้นไว้ ยิง NotificationChannel
    public void SetSentNotification(string Title, string Text ,DateTime fireTime)
    {

#if UNITY_ANDROID
        AndroidNotification notification = new AndroidNotification()
        {
            Title = Title,
            Text = Text,
            SmallIcon = "icon_0",
            LargeIcon = "icon_1",
            FireTime = fireTime,
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

}
