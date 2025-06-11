using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for the "reset" game button during Play mode
/// Also is used for the Start Game button on the start screen since they do basically the same thing
/// </summary>
[RequireComponent(typeof(Button))]
public class NewGameButton : MonoBehaviour
{
    // I could separate StartGameButton out but that would mean making a new class that is
    // basically the exact same as this one and does a very similar thing just depending
    // if the game has already started or not

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
        OnNewGameButtonClicked?.Invoke();
    }
}
