using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class StaticTextChanger : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _textField;
    [SerializeField]
    private UITextSO _textSO;

    private void Start() {
        LanguageSelector.Instance.OnLanguageChanged += SetLanguage;
    }

    private void SetLanguage(object sender, LanguageSelector.OnLanguageChangedEventArgs e) {
        _textField.text = e.Language == Language.English ? _textSO.Translations[0].Text : _textSO.Translations[1].Text;
    }
}
