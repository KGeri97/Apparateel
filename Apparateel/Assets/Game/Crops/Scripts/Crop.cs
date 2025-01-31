using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        [SerializeField]
        private GameObject _outline;
        private CropInfoUI _cropInfoUI;

        private CropGrowth _cropGrowth;
        private CropInfection _cropInfection;

        public DirtMound Mound { get; private set; }

        public bool IsGrowing => _cropGrowth.IsGrowing;

        private void OnEnable() {
            _clickable.OnClick += OnClickEvent;
            InputManager.Instance.OnClick += RemoveHighlight;
        }

        private void OnDisable() {
            _clickable.OnClick -= OnClickEvent;
            InputManager.Instance.OnClick += RemoveHighlight;
        }

        private void Start() {
            _cropGrowth = GetComponent<CropGrowth>();
            _cropInfection = GetComponent<CropInfection>();
            _gameManager = GameManager.Instance;
            _cropInfoUI = CropInfoUI.Instance;
        }

        private void OnClickEvent(object sender, EventArgs e) {
            if (!IsGrowing && _gameManager.State == GameState.Running) {
                HarvestCrop();
                return;
            }

            switch (_gameManager.State) {
                case GameState.Inspecting:
                    Inspect();
                    break;
            }
        }

        private void HarvestCrop() {
            MoneyManager.Instance.ItemSold(GetCropValue());
            Mound.RemoveCrop();
            Destroy(gameObject);
        }

        private void Inspect() {
            if (_outline.activeSelf) {
                RemoveHighlight(null, new InputManager.OnClickEventArgs { ClickedObject = null });
                return;
            }

            _outline.SetActive(true);
            _cropInfoUI.AddCropToInvestigate(this);
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

        private void RemoveHighlight(System.Object sender, InputManager.OnClickEventArgs e) {
            if (e.ClickedObject == _clickable)
                return;

            _outline.SetActive(false);
            _cropInfoUI.RemoveCropFromInvestigation(this);

        }

    }

    public enum CropQualityClass {
        Extra,
        I,
        II,
        III
    }
}
