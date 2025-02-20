using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Equipment;

namespace Apparateel.Equipment {
    [CreateAssetMenu(fileName = "SOEquipment", menuName = "Scriptable Objects/EquipmentData")]
    public class SOEquipment : ScriptableObject {
        public string EquipmentName;
        public int Cost;
        public List<EquipmentStatModifier> StatModifiers;
        public List<EquipmentApplianceModifier> ApplianceModifiers;
        public List<ApplianceAction> ActionType;
    }
}
