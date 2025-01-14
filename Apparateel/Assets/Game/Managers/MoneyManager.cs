using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [SerializeField]
    private float _money;
    public float Money => _money;

    public event EventHandler<float> OnMoneyChanged;

    private void Awake() {
        if (Instance != null) {
            return;
        }

        Instance = this;
    }

    public void ItemSold(float itemValue) {
        _money += itemValue;
        OnMoneyChanged?.Invoke(this, _money);
    }

    public void ItemPurchased(float itemValue) {
        _money -= itemValue;
        OnMoneyChanged?.Invoke(this, _money);
    }

    private void Start(){
        //Make sure the starting money is displayed correctly
        OnMoneyChanged?.Invoke(this, _money);
    }
}
