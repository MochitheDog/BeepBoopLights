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

    void Start()
    {
        timerValue = 0;
        isTimerOn = false;
    }

    private void Awake()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isTimerOn)
        {
            // frame rate-independent time
            timerValue += Time.unscaledDeltaTime;
            UpdateTimerText();
        }
    }

    private void OnEnable()
    {
        NewGameButton.OnNewGameButtonClicked += ResetTimer;
        BeepBoopLightsGame.OnGameWin += StopTimer;
    }

    private void OnDisable()
    {
        NewGameButton.OnNewGameButtonClicked -= ResetTimer;
        BeepBoopLightsGame.OnGameWin -= StopTimer;
    }

    private void UpdateTimerText()
    {
        var time = TimeSpan.FromSeconds(timerValue);
        text.SetText(time.ToString(@"mm\:ss"));
    }

    private void ResetTimer()
    {
        timerValue = 0;
        UpdateTimerText();
        isTimerOn = true;
    }

    private void StopTimer()
    {
        isTimerOn = false;
    }
}
