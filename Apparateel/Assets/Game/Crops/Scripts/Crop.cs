using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public bool IsGrowing => _cropGrowth.IsGrowing;


        private void OnEnable() {
            _clickable.OnClick += OnClick;
        }

        private void OnDisable() {
            _clickable.OnClick -= OnClick;
        }

        private void Start() {
            _cropGrowth = GetComponent<CropGrowth>();
        }

        private void Update() {
        }

        private void OnClick() {
            Debug.Log("Potatoed");
        }

    }

    public enum CropQualityClass {
        Extra,
        I,
        II,
        III
    }
}
