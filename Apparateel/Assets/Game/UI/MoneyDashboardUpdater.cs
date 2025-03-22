using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDashboardUpdater : MonoBehaviour
{
    private TMP_Text _textComponent;
    [SerializeField]
    private UITextSO _translatedText;

    private void Awake() {
        _textComponent = GetComponent<TMP_Text>();
    }

    private void Start() {
        MoneyManager.Instance.OnMoneyChanged += UpdateDashboard;
        LanguageSelector.Instance.OnLanguageChanged += (object sender, LanguageSelector.OnLanguageChangedEventArgs e) => { UpdateDashboard(this, MoneyManager.Instance.Money); };
    }

    private void OnDestroy() {
        MoneyManager.Instance.OnMoneyChanged -= UpdateDashboard;
    }

    private void UpdateDashboard(object sender, float value) {
        string str = LanguageSelector.Instance.Language == Language.English ? _translatedText.Translations[0].Text : _translatedText.Translations[1].Text;
        _textComponent.text = $"{str}: {value}";
    }

}
