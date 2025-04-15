using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CycleButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _buttonText;
    [SerializeField]
    private TMP_Text _cycleText;
    private Button _button;

    private int _stateNumber = 0;


    private void Awake(){
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void Start() {
        GameManager.Instance.OnCycleStateChanged += OnCycleStateChanged;
    }

    private void Update(){
        
    }

    private void OnClick() {
        _stateNumber++;
        if (_stateNumber > 2)
            _stateNumber = 0;

        GameManager.Instance.ChangeCycleState((CycleState)_stateNumber);

    }

    private void OnCycleStateChanged(object sender, GameManager.OnCycleStateChangedEventArgs e) {
        _cycleText.text = e.NewState.ToString();
    }
}
