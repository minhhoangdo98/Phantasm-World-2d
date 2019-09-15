using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoiGian : MonoBehaviour
{
    public string timeNow = DateTime.Now.ToLongTimeString();
    public Text clockText;
    [HideInInspector]
    public string minuteCount = "";
    [HideInInspector]
    public string secondCount = "";
    public int dayNow = DateTime.Now.Day, dayPrev;


    public void TimeCount(Text timeCount)//dem thoi gian thuc hien cong viec
    {
        minuteCount = ((int)Time.timeSinceLevelLoad / 60).ToString();//Hien phut
        secondCount = ((int)Time.timeSinceLevelLoad % 60).ToString();//hien giay
        timeCount.text = minuteCount + ":" + secondCount;
    }
}
