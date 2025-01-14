using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Apparateel.Crop {
    [RequireComponent(typeof(CropGrowth))]
    [RequireComponent(typeof(CropInfection))]
    [RequireComponent(typeof(CropColor))]
    public class Crop : MonoBehaviour, ICrop {

        [SerializeField]
        private SOCropData _cropData;
        public SOCropData CropData => _cropData;

        [SerializeField]
        private Clickable _clickable;

        private CropGrowth _cropGrowth;
        private CropInfection _cropInfection;

        public DirtMound Mound { get; private set; }

        public bool IsGrowing => _cropGrowth.IsGrowing;

        private void OnEnable() {
            _clickable.OnClick += OnClickEvent;
        }

        private void OnDisable() {
            _clickable.OnClick -= OnClickEvent;
        }

        private void Start() {
            _cropGrowth = GetComponent<CropGrowth>();
            _cropInfection = GetComponent<CropInfection>();
        }

        private void OnClickEvent(object sender, EventArgs e) {
            if (IsGrowing)
                return;

            MoneyManager.Instance.ItemSold(GetCropValue());
            Mound.RemoveCrop();
            Destroy(gameObject);
        }

        private float GetCropValue() {
            float baseSellPrice = _cropData.MoneyData.SellPrices[0].SellPrice;
            float finalSellPrice = baseSellPrice;

            if (_cropInfection.IsInfected)
                finalSellPrice *= _cropInfection.CurrentInfectionPriceModifier;
            return finalSellPrice ;
        }

        public void SetDirtMound(DirtMound dirtMound) {
            Mound = dirtMound;
        }

    }

    public enum CropQualityClass {
        Extra,
        I,
        II,
        III
    }
}
