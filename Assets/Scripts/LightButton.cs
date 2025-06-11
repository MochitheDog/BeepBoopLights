using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class LightButton : MonoBehaviour
{
    private Image image;
    private Button button;

    private Color litColor = Color.yellow;
    private Color offColor = Color.grey;

    public bool IsLit { get; private set; }

    private List<LightButton> adjacentLights = new List<LightButton>();
    public List<LightButton> AdjacentLights
    {
        get { return adjacentLights; }
        private set { adjacentLights = value; }
    }

    // Use Action instead of UnityEvent because we don't need Inspector functionality
    // and Action is faster and has less overhead (no reflection needed)
    // Invoke this Action whenever any LightButton is clicked (static)
    // Also, if we didn't have this then every LightButton would have to couple with
    // BeepBoopLightsGame and the MoveCounter.
    public static event Action OnLightButtonClicked;

    void Awake()
    {
        // Using GetComponent? Should be fine for now
        // Alternatively, could link the reference using SerializeField and the Inspector
        // but this is easy and fast, don't have to worry about forgotten/broken refs,
        // and not thinking about probably-micro optimizations right now
        image = transform.GetComponent<Image>();
        button = transform.GetComponent<Button>();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ToggleLightButtonCommand);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

    private void SetLightColorFromState()
    {
        image.color = IsLit ? litColor : offColor;
    }

    public void SetLitState( bool isLit )
    {
        IsLit = isLit;
        SetLightColorFromState();
    }

    /// <summary>
    /// Function to be called when the LightButton is clicked
    /// </summary>
    public void ToggleLightButtonCommand()
    {
        ToggleLight();
        ToggleAdjacentLights();
        OnLightButtonClicked?.Invoke();
    }

    public void ToggleLightAndAdjacents()
    {
        ToggleLight();
        ToggleAdjacentLights();
    }

    private void ToggleLight()
    {
        IsLit = !IsLit;
        SetLightColorFromState();
    }

    private void ToggleAdjacentLights()
    {
        foreach (var light in AdjacentLights)
        {
            light.ToggleLight();
        }
    }

    public void SetAdjacentLights(List<LightButton> adjacents)
    {
        AdjacentLights = adjacents;
    }

    public void SetInteractable(bool interactable)
    {
        button.interactable = interactable;
    }
}
