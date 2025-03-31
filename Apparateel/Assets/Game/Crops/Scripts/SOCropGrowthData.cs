using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;

namespace Apparateel.Crop {
    [CreateAssetMenu(fileName = "CropGrowthData", menuName = "Scriptable Objects/CropGrowthData")]
    public class SOCropGrowthData : ScriptableObject {
        [Range(1,15)]
        public int GrowthStages;
        [Range(0, 600)]
        public float FullGrowthTime;
        public float GrowthPeriodLength => FullGrowthTime / GrowthStages;
        public Vector3 BeginScale;
        public Vector3 EndScale;
        public AnimationCurve GrowthCurve;
        public Color BeginColor;
        public Color EndColor;
    }
}
