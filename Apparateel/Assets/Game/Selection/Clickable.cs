using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour {
    public event EventHandler OnClick;

    public void InvokeOnClick() {
        OnClick?.Invoke(this, EventArgs.Empty);
    }
}
