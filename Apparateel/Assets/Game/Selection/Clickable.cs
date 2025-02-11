using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour, IHasOutline {
    public event EventHandler OnClick;

    [SerializeField]
    private ClickableType _clickableType;
    public ClickableType ClickableType => _clickableType;

    [SerializeField]
    private GameObject _outline;

    public void InvokeOnClick() {
        OnClick?.Invoke(this, EventArgs.Empty);
    }

    public void ToggleOutline(bool active) {
        _outline.SetActive(active);
    }
}

public enum ClickableType { 
    None,
    Crop,
    DirtMound,
    Barn
}
