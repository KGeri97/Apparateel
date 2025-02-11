using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    public static HighlightManager Instance;

    private static List<Clickable> _highlightedClickables = new();

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already a HighlightManager Instance.");
            return;
        }

        Instance = this;
    }

    private void Start() {
        InputManager.Instance.OnMouseHoverObjectChange += OnMouseHoverObjectChange;
    }

    private void OnDestroy() {
        InputManager.Instance.OnMouseHoverObjectChange -= OnMouseHoverObjectChange;
    }

    private void OnMouseHoverObjectChange(object sender, InputManager.OnMouseHoverObjectChangeEventArgs e) {
        ToggleAllClickableOutlines(false);

        if (e.Clickable == null)
            return;

        _highlightedClickables.Add(e.Clickable);
        e.Clickable.ToggleOutline(true);
    }

    public static void ChangeHighlightedClickables(List<Clickable> clickables) {
        ToggleAllClickableOutlines(false);

        _highlightedClickables = clickables;

        ToggleAllClickableOutlines(true);
    }

    private static void ToggleAllClickableOutlines(bool active) {
        foreach (Clickable clickable in _highlightedClickables) {
            clickable.ToggleOutline(active);
        }
    }
}
