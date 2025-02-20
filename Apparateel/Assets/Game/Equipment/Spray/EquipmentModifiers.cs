using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apparateel.Equipment {
    [System.Serializable]
    public class EquipmentStatModifier {
        public ModifiableStats StatModifier;
        public float Value;
    }

    [System.Serializable]
    public class EquipmentApplianceModifier {
        public ApplianceAction Action;
        public ApplianceMethod Method;
    }

    public enum ModifiableStats {
        None,
        SprayCostPerPlant,
        SprayMaxProtection,
        SprayProtectionDecline,
        SprayCropSellReduction
    }

    public enum ApplianceAction {
        None,
        Planting,
        Fertilizing,
        Spraying,
        Inspecting
    }

    public enum ApplianceMethod {
        None,
        Singular,
        Area,
        Row,
        Field
    }
}