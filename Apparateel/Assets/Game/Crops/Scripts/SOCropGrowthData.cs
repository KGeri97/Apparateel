using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;

namespace Apparateel.Crop {
    [CreateAssetMenu(fileName = "CropGrowthData", menuName = "Scriptable Objects/CropGrowthData")]
    public class SOCropGrowthData : ScriptableObject {
        public int GrowthStages;
        public float GrowthPeriodLength;
        public Vector3 BeginScale;
        public Vector3 EndScale;
        public AnimationCurve GrowthCurve;
        public Color BeginColor;
        public Color EndColor;
    }
}
