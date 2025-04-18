using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Apparateel.Equipment;

namespace Apparateel.Crop {
    [RequireComponent(typeof(CropGrowth))]
    [RequireComponent(typeof(CropInfection))]
    [RequireComponent(typeof(CropColor))]
    public class Crop : MonoBehaviour, ICrop {

        private GameManager _gameManager;

        [SerializeField]
        private SOCropData _cropData;
        public SOCropData CropData => _cropData;

        [SerializeField]
        private Clickable _clickable;
        public Clickable Clickable => _clickable;

        private CropGrowth _cropGrowth;
        public CropGrowth CropGrowth => _cropGrowth;
        private CropInfection _cropInfection;
        public CropInfection CropInfection => _cropInfection;

        public DirtMound Mound { get; private set; }

        public static event EventHandler<OnCropHarvestedEventArgs> OnCropHarvested;
        public class OnCropHarvestedEventArgs : EventArgs {
            public Crop Crop;
            public int RowPosition;
            public int Position;
            public float Value;
        }

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
            if (!IsGrowing && _gameManager.GameState == GameState.Harvesting) {
                HarvestCrop();
                return;
            }
        }

        private void HarvestCrop() {
            OnCropHarvested?.Invoke(this, new OnCropHarvestedEventArgs() {
                Crop = this,
                RowPosition = Mound.RowPosition,
                Position = Mound.Position,
                Value = GetCropValue()
            });
            MoneyPopUpManager.Instance.CreatePopUp(GetCropValue());
            AudioManager.Instance.PlaySound(4);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

        public float GetCropValue() {
            float baseSellPrice = _cropData.MoneyData.SellPrices[0].SellPrice;
            float finalSellPrice = baseSellPrice;

            //Infection modifier
            if (_cropInfection.IsInfected && _cropInfection.CurrentInfectionPriceModifier != 0)
                finalSellPrice -= baseSellPrice * _cropInfection.CurrentInfectionPriceModifier;

            //Spray modifier
            float cropSellPriceModifier = EquipmentManager.Instance.GetActiveModifier(ModifiableStats.SprayCropSellReduction);
            cropSellPriceModifier = cropSellPriceModifier == -1 ? 1 : cropSellPriceModifier;
            if (_cropInfection.IsSprayed)
                finalSellPrice -= _cropInfection.SprayedWith.CropSellPriceReduction * cropSellPriceModifier * _cropInfection.TimesSprayed;

            return Mathf.Max(0, finalSellPrice);
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
