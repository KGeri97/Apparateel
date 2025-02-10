using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apparateel.Equipment {
    [CreateAssetMenu(fileName = "SprayEquipmentData", menuName = "Scriptable Objects/SprayEquipmentData")]
    public class SOSprayEquipment : ScriptableObject {
        public string Name;
        public SprayType SprayType;
    }

    public enum SprayType {
        None,
        Singular,
        Area,
        Row,
        Field
    }
}
