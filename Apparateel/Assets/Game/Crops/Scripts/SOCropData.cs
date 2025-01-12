using UnityEngine;
using Apparateel.Crop;

namespace Apparateel.Crop {
    [CreateAssetMenu(fileName = "CropData", menuName = "Scriptable Objects/CropData")]
    public class SOCropData : ScriptableObject {
        [Header("Name")]
        public string Name;
        [Header("DataModules")]
        public SOCropGrowthData GrowthData;
        public SOCropInfectionData InfectionData;
        public SOCropMoneyData MoneyData;
    }
}
