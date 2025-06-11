using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to organize a row of LightButtons (so we can set references in the editor for simple object management)
/// </summary>
public class LightButtonRow : MonoBehaviour
{
    [SerializeField]
    private List<LightButton> lightButtons;
    public List<LightButton> LightButtons
    {
        get { return lightButtons; }
    }
}
