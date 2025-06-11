using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for the Start Game button on the start screen
/// Doesn't seem to work for some reason...
/// </summary>
[RequireComponent(typeof(Button))]
public class StartButton : MonoBehaviour
{
    private Button button;
    public static event Action OnStartButtonClicked;

    void Awake()
    {
        button = transform.GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(StartButtonClicked);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    public void StartButtonClicked()
    {
        Debug.Log("AAAA CLICK AAA");
        OnStartButtonClicked?.Invoke();
    }
}
