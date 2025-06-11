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

    // Don't want to populate this programatically
    // to avoid using GameObject.FindGameObjects... etc/GameObject.Find
    // since those are costly and slow (loops through every GameObject)
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
            for (int j = 0; j < row.LightButtons.Count; j++)
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

                row.LightButtons[j].SetAdjacentLights(adjacents);
                row.LightButtons[j].SetLitState(false);
            }
        }
    }

    /// <summary>
    /// Reset the board to a new random solvable state by turning everything off and then toggling lights at random
    /// </summary>
    public void ResetAndRandomizeBoard()
    {
        bool isGoodBoard = false;
        foreach (var row in LightsGrid)
        {
            foreach (var light in row.LightButtons)
            {
                light.SetLitState(false);
            }
        }
        foreach (var row in LightsGrid)
        {
            foreach (var light in row.LightButtons)
            {
                var rand = Random.Range(0, 2);
                if (rand == 1)
                {
                    isGoodBoard = true;
                    light.ToggleLightAndAdjacents();
                }
            }
        }
        // I suppose it's possible to make this even more random by randomizing the order that
        // the lights get processed

        // Very slight chance that every random roll is 0
        // so if that happens let's just roll again i guess
        if (!isGoodBoard)
        {
            ResetAndRandomizeBoard();
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
            (col >= 0 && col < LightsGrid[row].LightButtons.Count ) )
        {
            return LightsGrid[row].LightButtons[col];
        }
        return null;
    }

    /// <summary>
    /// Turn all Buttons' interactables on or off
    /// </summary>
    /// <param name="interactable"></param>
    public void SetBoardInteractable(bool interactable)
    {
        foreach (var row in LightsGrid)
        {
            foreach (var light in row.LightButtons)
            {
                light.SetInteractable(interactable);
            }
        }
    }

    /// <summary>
    /// Check if the player WON
    /// </summary>
    /// <returns>True if player solved the board</returns>
    public bool IsBoardSolved()
    {
        foreach (var row in LightsGrid)
        {
            foreach (var light in row.LightButtons)
            {
                if (light.IsLit)
                {
                    return false;
                }
            }
        }
        return true;
        
    }
}
