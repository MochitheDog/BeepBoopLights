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
        get => adjacentLights;
        set => adjacentLights = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleLight);
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        //button.onClick.RemoveAllListeners();
    }

    private void SetLightColorFromState()
    {
        image.color = IsLit ? litColor : offColor;
    }

    public void SetState( bool isLit )
    {
        IsLit = isLit;
        SetLightColorFromState();
    }

    public void ToggleLight()
    {
        IsLit = !IsLit;
        SetLightColorFromState();
    }

    private void ToggleAdjacentLights()
    {
        foreach (var light in adjacentLights)
        {
            light.ToggleLight();
        }
    }
}
