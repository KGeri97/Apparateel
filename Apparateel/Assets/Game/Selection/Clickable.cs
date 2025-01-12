using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour, IClickable {
    public Action OnClick { get; set; }
}
