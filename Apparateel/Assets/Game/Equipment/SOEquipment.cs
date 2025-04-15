using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Equipment;

namespace Apparateel.Equipment {
    [CreateAssetMenu(fileName = "SOEquipment", menuName = "Scriptable Objects/EquipmentData")]
    public class SOEquipment : ScriptableObject {
        public string EquipmentName;
        public List<ApplianceAction> ActionType;
        public List<EquipmentStatModifier> StatModifiers;
        public List<EquipmentApplianceModifier> ApplianceModifiers;
        public int Cost;
        public int RentCost;
    }
}
