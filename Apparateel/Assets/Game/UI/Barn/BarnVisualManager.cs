using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Equipment;
using System;

public class BarnVisualManager : MonoBehaviour
{
    [SerializeField]
    private Transform _availableEquipmentsTransform;
    [SerializeField]
    private Transform _purchasedEquipmentsTransform;
    [SerializeField]
    private EquipmentVisualizer _equipmentPrefab;

    private void Start() {
        foreach (EquipmentData equipment in EquipmentManager.Instance.AvailableEquipments) {
            EquipmentVisualizer ev = Instantiate(_equipmentPrefab, _availableEquipmentsTransform);
            ev.SetEquipment(equipment);
        }

        EquipmentManager.Instance.OnEquipmentPurchased += OnEquipmentPurchased;
    }

    private void DrawEquipmentCards(List<EquipmentData> equipments, Transform parent) {
        if (parent.childCount > 0) {
            int i = 0;

            GameObject[] allChildren = new GameObject[parent.childCount];

            foreach (Transform child in parent) {
                allChildren[i] = child.gameObject;
                i += 1;
            }

            foreach (GameObject child in allChildren) {
                DestroyImmediate(child.gameObject);
            }

        }

        foreach (EquipmentData equipment in equipments) {
            EquipmentVisualizer ev = Instantiate(_equipmentPrefab, parent);
            ev.SetEquipment(equipment);
        }
    }

    private void OnEquipmentPurchased(object sender, EquipmentManager.OnEquipmentPurchasedEventArgs e) {
        DrawEquipmentCards(e.AvailableEquipments, _availableEquipmentsTransform);
        DrawEquipmentCards(e.PurchasedEquipments, _purchasedEquipmentsTransform);
    }
}
