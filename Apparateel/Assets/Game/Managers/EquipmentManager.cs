using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Apparateel.Equipment {
    public class EquipmentManager : MonoBehaviour {
        public static EquipmentManager Instance;


        private void Awake() {
            if (Instance != null) {
                Debug.LogError("There is already an EquipmentManager Instance.");
                return;
            }

            Instance = this;
        }
    }
}
