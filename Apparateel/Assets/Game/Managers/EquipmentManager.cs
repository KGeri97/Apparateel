using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Apparateel.Equipment;
using UnityEngine.UI;
using System.Linq;

namespace Apparateel.Equipment {
    public class EquipmentManager : MonoBehaviour {
        public static EquipmentManager Instance;

        [SerializeField]
        private Clickable _clickable;

        [SerializeField]
        private GameObject _barnUI;

        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private List<EquipmentData> _availableEquipments;
        public List<EquipmentData> AvailableEquipments => _availableEquipments;
        private List<EquipmentData> _purchasedEquipments = new();
        public List<EquipmentData> PurchasedEquipments => _purchasedEquipments;
        private List<EquipmentData> _activeEquipments = new();
        public List<EquipmentData> ActiveEquipments => _activeEquipments;

        public event EventHandler<OnEquipmentPurchasedEventArgs> OnEquipmentPurchased;
        public class OnEquipmentPurchasedEventArgs : EventArgs {
            public EquipmentData PurchasedEquipment;
            public List<EquipmentData> AvailableEquipments;
            public List<EquipmentData> PurchasedEquipments;
        }

        public event EventHandler<OnActiveEquipmentsChangedEventArgs> OnActiveEquipmentsChanged;
        public class OnActiveEquipmentsChangedEventArgs : EventArgs {
            public List<EquipmentData> ActiveEquipments;
            public EquipmentData ChangedEquipment;
            public bool Added;
        }

        private void Awake() {
            if (Instance != null) {
                Debug.LogError("There is already an EquipmentManager Instance.");
                return;
            }

            Instance = this;

            _clickable.OnClick += OnClick;
            _closeButton.onClick.AddListener(CloseUI);
        }

        private void OnDestroy() {
            _clickable.OnClick -= OnClick;
        }

        /// <summary>
        /// Changes the list of active equipments
        /// </summary>
        /// <param name="equipment">The equipment being changed</param>
        /// <param name="added">True if activeated, false if deactivated</param>
        public void ChangeActiveEquipment(EquipmentData equipment, bool added) {
            if (added) {
                _activeEquipments.Add(equipment);
                _purchasedEquipments.Remove(equipment);
                equipment.IsActive = true;
            }
            else {
                _purchasedEquipments.Add(equipment);
                _activeEquipments.Remove(equipment);
                equipment.IsActive = false;
            }
        }

        public void EquipmentPurchased(EquipmentData purchasedEquipment) {
            purchasedEquipment.IsPurchased = true;
            PurchasedEquipments.Add(purchasedEquipment);
            AvailableEquipments.Remove(purchasedEquipment);

            OnEquipmentPurchased?.Invoke(this, new() {
                PurchasedEquipment = purchasedEquipment,
                AvailableEquipments = _availableEquipments,
                PurchasedEquipments = _purchasedEquipments});
        }

        private void OnClick(object sender, EventArgs e) {
            _barnUI.SetActive(true);
        }

        private void CloseUI() {
            _barnUI.SetActive(false);
        }

        public float GetActiveModifier(ModifiableStats modifiableStat) {
            float modifier;
            foreach (EquipmentData ed in _activeEquipments) {
                modifier = ed.GetStatModifier(modifiableStat);
                if (modifier != -1f)
                    return modifier;
            }

            return -1f;
        }

        /// <summary>
        /// Returns the active equipment based on the action it modifies
        /// </summary>
        /// <param name="applianceAction"></param>
        /// <returns></returns>
        public EquipmentData GetActiveEquipment(ApplianceAction applianceAction) {
            if (_activeEquipments.Count == 0) {
                return null;
            }

            EquipmentData equipment = _activeEquipments.FirstOrDefault(x => x.Data.ActionType.Contains(applianceAction));
            return equipment;
        }
    }
}
