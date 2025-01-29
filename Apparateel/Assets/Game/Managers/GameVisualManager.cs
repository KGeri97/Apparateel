using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVisualManager : MonoBehaviour
{
    public static GameVisualManager Instance;

    [Header("UI colors")]
    [SerializeField]
    private Color _uiButtonDeselectedColor;
    public Color UIButtonDeselectedColor => _uiButtonDeselectedColor;
    [SerializeField]
    private Color _uiButtonSelectedColor;
    public Color UIButtonSelectedColor => _uiButtonSelectedColor;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already a GameVisualManager active.");
            return;
        }

        Instance = this;
    }
}
