using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerTEXT;

    [Header("Timer Settings")]
    public float currentTime;
    public bool countUp;

    [Header("Timer Format Settings")]
    public bool hasFormat;
    public TimerFormats format;

    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    void Start()
    {
        timeFormats.Add(TimerFormats.Whole, "0");
        timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormats.HundrenthDecimal, "0.00");
    }

    void Update()
    {
        currentTime = countUp ? currentTime + Time.deltaTime : currentTime - Time.deltaTime;
        SetTimerText();
    }

    private void SetTimerText()
    {
        timerTEXT.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }

    // Public property to expose currentTime
    public float CurrentTime
    {
        get { return currentTime; }
    }
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrenthDecimal
}