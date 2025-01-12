using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;

namespace Apparateel.Crop {

    [CreateAssetMenu(fileName = "CropMoneyData", menuName = "Scriptable Objects/CropMoneyData")]
    public class SOCropMoneyData : ScriptableObject {

        public float BuyPrice;
        public ValuePairCropQualitySellPrice[] SellPrices;
        public CropQualityClass CropQuality;

        private void OnEnable() {
            // Initialize array if not already set
            if (SellPrices == null || SellPrices.Length == 0) {
                int enumLength = System.Enum.GetValues(typeof(CropQualityClass)).Length;
                SellPrices = new ValuePairCropQualitySellPrice[enumLength];

                //Initialize each element in the array
                for (int i = 0; i < enumLength; i++) {
                    SellPrices[i] = new ValuePairCropQualitySellPrice();
                    SellPrices[i].Quality = (CropQualityClass)i;
                }
            }
        }
    }
}

