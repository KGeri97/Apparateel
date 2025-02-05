using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour {
    public event EventHandler OnClick;

    [SerializeField]
    private ClickableType _clickableType;
    public ClickableType ClickableType => _clickableType;

    public void InvokeOnClick() {
        OnClick?.Invoke(this, EventArgs.Empty);
    }
}

public enum ClickableType { 
    None,
    Crop,
    DirtMound
}
