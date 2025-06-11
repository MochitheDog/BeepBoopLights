using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class MoveCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int moveCount;

    private void Awake()
    {
        text = transform.GetComponent<TextMeshProUGUI>();
        text.SetText(moveCount.ToString());
    }

    private void OnEnable()
    {
        LightButton.OnLightButtonClicked += IncrementMoveCounter;
        NewGameButton.OnNewGameButtonClicked += ResetMoveCounter;
    }

    private void OnDisable()
    {
        LightButton.OnLightButtonClicked -= IncrementMoveCounter;
        NewGameButton.OnNewGameButtonClicked -= ResetMoveCounter;
    }

    private void IncrementMoveCounter()
    {
        moveCount++;
        text.SetText(moveCount.ToString());
    }

    private void ResetMoveCounter()
    {
        moveCount = 0;
        text.SetText(moveCount.ToString());
    }
}
