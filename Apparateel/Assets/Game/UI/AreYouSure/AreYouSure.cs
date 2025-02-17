using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AreYouSure : MonoBehaviour
{
    [SerializeField]
    private Button _yesButton;
    [SerializeField]
    private Button _noButton;

    private ICanAskYesNoQuestion _askingObject;

    private void Awake() {
        _yesButton.onClick.AddListener(() => OnPlayerInput(true));
        _noButton.onClick.AddListener(() => OnPlayerInput(false));
    }

    private void OnPlayerInput(bool response) {
        _askingObject.ResponseReceived(response);
        _askingObject = null;
        gameObject.SetActive(false);
    }

    public void SetAskingObject(ICanAskYesNoQuestion askingObject) {
        _askingObject = askingObject;
    }
}
