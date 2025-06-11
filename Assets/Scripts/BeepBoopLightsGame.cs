using System;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Main class for BeepBoopLightsGame
/// Controls overall game functions, with components of other functionality
/// </summary>
public class BeepBoopLightsGame : MonoBehaviour
{
    [SerializeField]
    private GameBoard gameBoard;
    [SerializeField]
    private GameObject winScreen;
    [SerializeField]
    private GameObject startScreen;

    // Unsure if actually need this or not yet
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

    // Check if the player WON
    public bool IsBoardSolved()
    {
        for (int i = 0; i < gameBoard.LightsGrid.Count; i++)
        {
            for (int j = 0; j < gameBoard.LightsGrid[i].lightButtons.Count; j++)
            {
                if (gameBoard.LightsGrid[i].lightButtons[j].IsLit)
                {
                    return false;
                }
            }
        }
        return true;
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
        if (IsBoardSolved())
        {
            GoToWinScreenState();
        }
    }

    private void OnNewGameButtonClicked()
    {
        switch (gameState)
        {
            case GameState.START:
                {
                    GoToPlayGameState();
                    break;
                }
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
