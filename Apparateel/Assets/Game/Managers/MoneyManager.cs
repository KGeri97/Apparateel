using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Apparateel.Crop;

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
        Crop.OnCropHarvested += OnCropHarvested;
    }

    public void ItemSold(float itemValue) {
        _money += itemValue;
        OnMoneyChanged?.Invoke(this, _money);
    }

    public bool ItemPurchased(float itemValue) {
        if (_money < itemValue)
            return false;

        _money -= itemValue;
        OnMoneyChanged?.Invoke(this, _money);
        return true;
    }

    private void Start(){
        //Make sure the starting money is displayed correctly
        OnMoneyChanged?.Invoke(this, _money);
    }

    private void OnDestroy() {
        Crop.OnCropHarvested -= OnCropHarvested;
    }

    private void OnCropHarvested(object sender, Crop.OnCropHarvestedEventArgs e) {
        ItemSold(e.Value);
    }
}
