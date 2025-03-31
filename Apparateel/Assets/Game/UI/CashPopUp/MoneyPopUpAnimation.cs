using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Utilities.Timer;
using TMPro;
using System;

public class MoneyPopUpAnimation : MonoBehaviour {
    [SerializeField]
    private float _duration;

    [SerializeField]
    private Vector3 _floatMovement;

    [SerializeField]
    private AnimationCurve _curve;

    private Timer _timer;

    private Vector3 _startingPosition;
    private Vector3 _endPosition;

    [SerializeField]
    private TMP_Text _textComponent;

    private void Awake() {
        _timer = new Timer(_duration, EndAnimation);
        _timer.Start();
        _startingPosition = transform.position;

        _endPosition = _startingPosition + _floatMovement;
        //Debug.Log($"Start {_startingPosition}");
        //Debug.Log($"End {_endPosition}");
    }

    private void Update() {
        _timer.Update();

        transform.position = Vector3.Lerp(_startingPosition, _endPosition, _curve.Evaluate(_timer.Progress));
        //Debug.Log($"Timer {_timer.Progress}");

    }

    private void EndAnimation() {
        //Debug.Log("Destroy");
        Destroy(gameObject);
    }

    public void SetText(float value) {
        _textComponent.text = $"{decimal.Round((decimal)value, 2, MidpointRounding.AwayFromZero)}€";
        _textComponent.color = value > 0 ? Color.green : Color.red;
    }
}
