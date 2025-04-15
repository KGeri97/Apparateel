using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Equipment;
using System.Linq;

namespace Apparateel.Equipment {
    [System.Serializable]
    public class EquipmentData {
        public SOEquipment Data;
        public bool IsActive;
        public bool IsPurchased;
        public bool IsRented;

        public float GetStatModifier(ModifiableStats statModifier) {
            float modifier = Data.StatModifiers
                .Where(x => x.StatModifier == statModifier)
                .Select(x => x.Value)
                .DefaultIfEmpty(-1f)
                .First();
            return modifier;
        }

        public ApplianceMethod GetApplianceMethod(ApplianceAction applianceAction) {
            ApplianceMethod method = Data.ApplianceModifiers
                .Where(x => x.Action == applianceAction)
                .Select(x => x.Method)
                .DefaultIfEmpty(ApplianceMethod.None)
                .First();
            return method;
        }
    }
}
