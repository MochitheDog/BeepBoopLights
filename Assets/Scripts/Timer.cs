using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Keep track of how much time has elapsed since the start of the round
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : MonoBehaviour
{
    private bool isTimerOn;
    private TextMeshProUGUI text;
    private float timerValue;
    private TimeSpan timeSpan;
    void Start()
    {
        timerValue = 0;
        isTimerOn = true;
    }

    private void Awake()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
    }

    // Using FixedUpdate for timer because 
    private void Update()
    {
        if (isTimerOn)
        {
            timerValue += Time.unscaledDeltaTime;
            FormatTimerValue();
        }
    }

    private void FormatTimerValue()
    {
        var time = TimeSpan.FromSeconds(timerValue);
        text.SetText(time.ToString(@"mm\:ss"));
    }

}
