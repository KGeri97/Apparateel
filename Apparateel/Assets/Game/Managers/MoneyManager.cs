using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    private float _money = 0;
    public float Money => _money;

    public event EventHandler<float> OnMoneyChanged;

    private void Awake() {
        if (Instance != null) {
            return;
        }

        Instance = this;
    }

    public void ItemSold(float itemValue) {
        Debug.Log($"{itemValue} {_money}");
        _money += itemValue;
        OnMoneyChanged?.Invoke(this, _money);
    }

    private void Start(){
        
    }

    private void Update() {

    }
}
