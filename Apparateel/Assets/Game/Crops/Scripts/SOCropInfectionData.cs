using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apparateel.Crop {
    [CreateAssetMenu(fileName = "CropInfectionData", menuName = "Scriptable Objects/CropInfectionData")]
    public class SOCropInfectionData : ScriptableObject {
        [Range(0f,1f)][Tooltip("So much less chance of getting infected")]
        public float NaturalResistance;
        [Range(0f, 1f)][Tooltip("Chance for an infection at growth stage advancement")]
        public float InfectionChance;
        //[Range(0f, 1f)][Tooltip("")]
        //public float MinInfectionSeverity;
        //[Range(0f, 1f)][Tooltip("")]
        //public float MaxInfectionSeverity;
        /// <summary>
        /// The time it takes for the infection to fully develop relative to the plant growth time in percentage
        /// </summary>
        [Range(0f, 1f)][Tooltip("The time it takes for the infection to fully develop. Given in percentage of totalt growth time")]
        public float InfectionDevelopmentTime;
        /// <summary>
        /// The applied percentage to crop value at maximum infection
        /// </summary>
        [Range(0f, 1f)][Tooltip("At full infection development the price will be reduced by this much percentage")]
        public float PriceModifier;
        public Color InfectionColor;
        public AnimationCurve InfectionVisibility;
    }
}
