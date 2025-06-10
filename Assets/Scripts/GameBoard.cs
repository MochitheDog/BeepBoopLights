using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to represent the game board/grid of lights
/// </summary>
public class GameBoard : MonoBehaviour
{
    private const int GRID_WIDTH = 5;
    private const int GRID_HEIGHT = 5;
    public List<LightButtonRow> lightsGrid = new List<LightButtonRow>();

    private enum GameState
    {
        IDLE,
        PLAYING
    }
    private GameState gameState = GameState.IDLE;

   

    private void OnLightClicked()
    {
        Debug.Log("AAAA CLIKED");
    }

    private void Start()
    {
        InitLightButtons();
    }

    private void OnEnable()
    {
        LightButton.OnLightButtonClicked += OnLightClicked;
    }

    private void OnDisable()
    {
        LightButton.OnLightButtonClicked -= OnLightClicked;
    }

    // Check if the player WON
    public bool IsBoardSolved()
    {
        for (int i = 0; i < lightsGrid.Count; i++)
        {
            for (int j = 0; j < lightsGrid[i].lightButtons.Count; j++)
            {
                if (lightsGrid[i].lightButtons[j].IsLit)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Set up the buttons' adjacency lists programmatically because doing it in editor will take forever
    // and any mistakes would be hard to find
    private void InitLightButtons()
    {
        for (int i = 0; i < lightsGrid.Count; i++)
        {
            var row = lightsGrid[i];
            for (int j = 0; j < row.lightButtons.Count; j++)
            {
                List<LightButton> adjacents = new List<LightButton>();
                // up
                var upButton = GetLightButton(i-1, j);
                if (upButton != null) adjacents.Add(upButton);
                // down
                var downButton = GetLightButton(i+1, j);
                if (downButton != null) adjacents.Add(downButton);
                // left
                var leftButton = GetLightButton(i, j-1);
                if (leftButton != null) adjacents.Add(leftButton);
                // right
                var rightButton = GetLightButton(i, j+1);
                if (rightButton != null) adjacents.Add(rightButton);

                row.lightButtons[j].AdjacentLights = adjacents;
                row.lightButtons[j].SetState(false);
            }
        }
    }

    // Reset the board to a new random solvable state by turning everything off and then toggling lights at random
    private void ResetBoard()
    {
        for (int i = 0; i < lightsGrid.Count; i++)
        {
            for (int j = 0; j < lightsGrid[i].lightButtons.Count; j++)
            {
                lightsGrid[i].lightButtons[j].SetState(false);
            }
        }

        for (int i = 0; i < lightsGrid.Count; i++)
        {
            for (int j = 0; j < lightsGrid[i].lightButtons.Count; j++)
            {
                var rand = Random.Range(0, 2);
                if ( rand == 1 )
                {
                    lightsGrid[i].lightButtons[j].ToggleLight();
                }
            }
        }
    }

    /// <summary>
    /// Return a particular LightButton at the given coordinates
    /// </summary>
    /// <param name="row">Vertical position</param>
    /// <param name="col">Horizontal position</param>
    /// <returns></returns>
    private LightButton GetLightButton(int row, int col)
    {
        if ( ( row >= 0 && row < lightsGrid.Count ) &&
            (col >= 0 && col < lightsGrid[row].lightButtons.Count ) )
        {
            return lightsGrid[row].lightButtons[col];
        }
        return null;
    }
}
