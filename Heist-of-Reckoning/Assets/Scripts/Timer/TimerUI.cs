using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TextMeshProUGUI TimerUIText;
    float TimerUICount;
    

    void Start()
    {
        //Checks if it can find the Text box
        if (TimerUIText == null)
        {
            Debug.LogError("TimerUIText is not assigned!");
        }

        TimerUICount = 0;
        
    }

    void Update()
    {
        TimerUICount += Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(TimerUICount);

       
        string formattedTime = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        TimerUIText.text = formattedTime;
    }
}
