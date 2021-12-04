using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public TimerInput_panel hourInput;
    public TimerInput_panel minuteInput;
    private DateTime CurrentTimer;

    public void SaveTimer() 
    {
        CurrentTimer = DateTime.Parse(hourInput.current_input + ":" + minuteInput.current_input);
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
