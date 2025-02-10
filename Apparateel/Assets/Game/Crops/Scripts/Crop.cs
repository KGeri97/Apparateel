using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Apparateel.Crop {
    [RequireComponent(typeof(CropGrowth))]
    [RequireComponent(typeof(CropInfection))]
    [RequireComponent(typeof(CropColor))]
    public class Crop : MonoBehaviour, ICrop, IHasOutline {

        private GameManager _gameManager;

        [SerializeField]
        private SOCropData _cropData;
        public SOCropData CropData => _cropData;

        [SerializeField]
        private Clickable _clickable;
        public Clickable Clickable => _clickable;

        [SerializeField]
        private GameObject _outline;

        private CropGrowth _cropGrowth;
        public CropGrowth CropGrowth => _cropGrowth;
        private CropInfection _cropInfection;
        public CropInfection CropInfection => _cropInfection;

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
            _gameManager = GameManager.Instance;
        }

        private void OnClickEvent(object sender, EventArgs e) {
            if (!IsGrowing && _gameManager.State == GameState.Running) {
                HarvestCrop();
                return;
            }
        }

        private void HarvestCrop() {
            MoneyManager.Instance.ItemSold(GetCropValue());
            Mound.RemoveCrop();
            Destroy(gameObject);
        }

        public float GetCropValue() {
            float baseSellPrice = _cropData.MoneyData.SellPrices[0].SellPrice;
            float finalSellPrice = baseSellPrice;

            //Infection modifier
            if (_cropInfection.IsInfected && _cropInfection.CurrentInfectionPriceModifier != 0)
                finalSellPrice -= baseSellPrice * _cropInfection.CurrentInfectionPriceModifier;

            //Spray modifier
            if (_cropInfection.IsSprayed)
                finalSellPrice -= _cropInfection.SprayedWith.CropSellPriceReduction * _cropInfection.TimesSprayed;

            return finalSellPrice ;
        }

        public void SetDirtMound(DirtMound dirtMound) {
            Mound = dirtMound;
        }

        public void ToggleOutline(bool active) {
            _outline.SetActive(active);
        }
    }

    public enum CropQualityClass {
        Extra,
        I,
        II,
        III
    }
}
