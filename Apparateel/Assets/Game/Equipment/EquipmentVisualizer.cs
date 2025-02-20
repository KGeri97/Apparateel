using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Equipment;
using TMPro;
using UnityEngine.UI;

public class EquipmentVisualizer : MonoBehaviour
{
    [SerializeField]
    private EquipmentData _equipment;
    public EquipmentData Equipment => _equipment;

    [SerializeField]
    private TMP_Text _nameField;
    [SerializeField]
    private TMP_Text _modifiersField;
    [SerializeField]
    private TMP_Text _costField;
    [SerializeField]
    private Button _activeButton;
    [SerializeField]
    private Button _purchaseButton;

    public void SetEquipment(EquipmentData equipment) {
        _activeButton.onClick.AddListener(OnActiveButtonClick);
        _purchaseButton.onClick.AddListener(OnPurchaseButtonClick);

        _equipment = equipment;

        SetFieldValues();
    }

    private void SetFieldValues() {
        _nameField.text = _equipment.Data.EquipmentName;

        foreach (EquipmentStatModifier statModifier in _equipment.Data.StatModifiers) {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<RectTransform>();
            TextMeshProUGUI textComponent = gameObject.AddComponent<TextMeshProUGUI>();
            string str = $"{statModifier.StatModifier.ToString()}: x{statModifier.Value.ToString()}";

            textComponent.text = str;
            textComponent.fontSize = 24;

            gameObject.transform.SetParent(transform);
            gameObject.transform.SetSiblingIndex(_modifiersField.transform.GetSiblingIndex() + 1);
        }

        foreach (EquipmentApplianceModifier applianceModifier in _equipment.Data.ApplianceModifiers) {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<RectTransform>();
            TextMeshProUGUI textComponent = gameObject.AddComponent<TextMeshProUGUI>();
            string str = $"{applianceModifier.Action.ToString()}: {applianceModifier.Method.ToString()}";

            textComponent.text = str;
            textComponent.fontSize = 24;

            gameObject.transform.SetParent(transform);
            gameObject.transform.SetSiblingIndex(_modifiersField.transform.GetSiblingIndex() + 1);
        }

        _costField.text = _equipment.Data.Cost.ToString();

        if (!_equipment.IsPurchased) {
            _purchaseButton.gameObject.SetActive(true);
            _activeButton.gameObject.SetActive(false);
        }
        else {
            _purchaseButton.gameObject.SetActive(false);
            _activeButton.gameObject.SetActive(true);

            if (_equipment.IsActive) {
                _activeButton.image.color = Color.green;
            }
            else {
                _activeButton.image.color = Color.red;
            }
        }
    }

    private void OnActiveButtonClick() {
        _equipment.IsActive = !_equipment.IsActive;
        EquipmentManager.Instance.ChangeActiveEquipment(_equipment, _equipment.IsActive);

        if (_equipment.IsActive) {
            _activeButton.image.color = Color.green;
        }
        else {
            _activeButton.image.color = Color.red;
        }
    }

    private void OnPurchaseButtonClick() {
        if (!MoneyManager.Instance.ItemPurchased(_equipment.Data.Cost))
            return;

        _purchaseButton.gameObject.SetActive(false);
        _activeButton.gameObject.SetActive(true);
        _activeButton.image.color = Color.red;

        EquipmentManager.Instance.EquipmentPurchased(_equipment);
    }
}
