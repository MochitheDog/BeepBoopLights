using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script for the "reset" game button during Play mode
/// Can also use this for the Start Game button on the start screen since they do basically the same thing
/// Is that a good idea? sus
/// </summary>
[RequireComponent(typeof(Button))]
public class NewGameButton : MonoBehaviour
{
    private Button button;
    public static event Action OnNewGameButtonClicked;

    void Awake()
    {
        button = transform.GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(NewGameButtonClicked);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    public void NewGameButtonClicked()
    {
        Debug.Log("AAAA CLICK");
        OnNewGameButtonClicked?.Invoke();
    }
}
