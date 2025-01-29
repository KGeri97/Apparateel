using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDashboardUpdater : MonoBehaviour
{
    private TMP_Text _textComponent;

    private void Awake() {
        _textComponent = GetComponent<TMP_Text>();
    }

    private void Start() {
        MoneyManager.Instance.OnMoneyChanged += UpdateDashboard;
    }

    private void OnDestroy() {
        MoneyManager.Instance.OnMoneyChanged -= UpdateDashboard;
    }

    private void UpdateDashboard(object sender, float value) {
        _textComponent.text = $"Money: {value}";
    }

}
