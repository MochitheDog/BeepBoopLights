using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Main controller class for BeepBoopLightsGame
/// Controls overall game logic, and game state flow
/// </summary>
public class BeepBoopLightsGame : MonoBehaviour
{
    // Keeping reference to the GameBoard to be able to control the LightButtons
    // Trying to keep things modular and decoupled as much as possible so we don't keep many other references
    // to the Timer, MoveCounter, Buttons on the Lights, etc, using subscribed Actions instead
    [SerializeField]
    private GameBoard gameBoard;
    // Keep references to the StartScreen and WinScreen objects to show/hide them
    // Don't need to make another class for them since they're simple
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject startScreen;

    private enum GameState
    {
        START,
        PLAYING,
        WINSCREEN,
    }
    private GameState gameState = GameState.START;

    public static event Action OnGameWin;

    void Start()
    {
        gameBoard.SetBoardInteractable(false);
        winScreen.SetActive(false);
        startScreen.SetActive(true);
    }

    private void OnEnable()
    {
        LightButton.OnLightButtonClicked += OnLightClicked;
        NewGameButton.OnNewGameButtonClicked += OnNewGameButtonClicked;
    }

    private void OnDisable()
    {
        LightButton.OnLightButtonClicked -= OnLightClicked;
        NewGameButton.OnNewGameButtonClicked -= OnNewGameButtonClicked;
    }

    // Show the Win state from the Playing state
    private void GoToWinScreenState()
    {
        gameState = GameState.WINSCREEN;
        winScreen.SetActive(true);
        gameBoard.SetBoardInteractable(false);
        // Alert other scripts (Timer) that the game is over
        OnGameWin?.Invoke();
    }

    // Transition from another state to game mode
    private void GoToPlayGameState()
    {
        gameState = GameState.PLAYING;
        if (winScreen.activeSelf)
        {
            winScreen.SetActive(false);
        }
        if (startScreen.activeSelf)
        {
            startScreen.SetActive(false);
        }
        gameBoard.SetBoardInteractable(true);
        gameBoard.ResetAndRandomizeBoard();
    }

    private void OnLightClicked()
    {
        // Every time a light is clicked, check the board and change states if needed
        if (gameBoard.IsBoardSolved())
        {
            GoToWinScreenState();
        }
    }

    private void OnNewGameButtonClicked()
    {
        switch (gameState)
        {
            case GameState.START:
            case GameState.WINSCREEN:
                {
                    GoToPlayGameState();
                    break;
                }
            case GameState.PLAYING:
                {
                    gameBoard.ResetAndRandomizeBoard();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
