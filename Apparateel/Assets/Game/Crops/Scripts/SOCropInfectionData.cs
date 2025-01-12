using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apparateel.Crop {
    [CreateAssetMenu(fileName = "CropInfectionData", menuName = "Scriptable Objects/CropInfectionData")]
    public class SOCropInfectionData : ScriptableObject {
        public float InfectionChance;
        public float MinInfectionSeverity;
        public float MaxInfectionSeverity;
        /// <summary>
        /// The time it takes for the infection to fully develop relative to the plant growth time in percentage
        /// </summary>
        public float InfectionDevelopmentTime;
        /// <summary>
        /// The applied percentage to crop value at maximum infection
        /// </summary>
        public float PriceModifier;
        public Color InfectionColor;
        public AnimationCurve InfectionVisibility;
    }
}
