using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to represent the game board/grid of lights
/// Does not handle game logic! Do not put game logic in here
/// </summary>
public class GameBoard : MonoBehaviour
{
    private const int GRID_WIDTH = 5;
    private const int GRID_HEIGHT = 5;

    [SerializeField]
    private List<LightButtonRow> lightsGrid = new List<LightButtonRow>();
    public List<LightButtonRow> LightsGrid
    {
        get { return lightsGrid; }
        private set { lightsGrid = value; }
    }

    private void Start()
    {
        InitLightButtons();
    }

    // Set up the buttons' adjacency lists programmatically because doing it in editor will take forever
    // and any mistakes would be hard to find
    private void InitLightButtons()
    {
        for (int i = 0; i < LightsGrid.Count; i++)
        {
            var row = LightsGrid[i];
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

                row.lightButtons[j].SetAdjacentLights(adjacents);
                row.lightButtons[j].SetLitState(false);
            }
        }
    }

    // Reset the board to a new random solvable state by turning everything off and then toggling lights at random
    public void ResetAndRandomizeBoard()
    {
        foreach (var row in LightsGrid)
        {
            foreach (var light in row.lightButtons)
            {
                light.SetLitState(false);
            }
        }
        foreach (var row in LightsGrid)
        {
            foreach (var light in row.lightButtons)
            {
                var rand = Random.Range(0, 2);
                if (rand == 1)
                {
                    light.ToggleLight();
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
        if ( ( row >= 0 && row < LightsGrid.Count ) &&
            (col >= 0 && col < LightsGrid[row].lightButtons.Count ) )
        {
            return LightsGrid[row].lightButtons[col];
        }
        return null;
    }

    // Making buttons non-interactable is functionally done by the WinScreen
    // which blocks click raycasts but just in case we need this
    public void SetBoardInteractable(bool interactable)
    {
        foreach (var row in LightsGrid)
        {
            foreach (var light in row.lightButtons)
            {
                light.SetInteractable(interactable);
            }
        }
    }
}
