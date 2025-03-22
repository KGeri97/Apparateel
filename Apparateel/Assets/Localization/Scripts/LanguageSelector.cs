using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LanguageSelector : MonoBehaviour {

    public static LanguageSelector Instance;

    [SerializeField]
    private Button _switchLaunguageButton;
    [SerializeField]
    private TMP_Text _buttonText;

    private Language _language = Language.English;
    public Language Language => _language;

    public event EventHandler<OnLanguageChangedEventArgs> OnLanguageChanged;
    public class OnLanguageChangedEventArgs : EventArgs {
        public Language Language;
    }

    private void Awake() {
        Instance = this;
    }

    private void Start(){
        _switchLaunguageButton.onClick.AddListener(ChangeLanguage);
    }

    private void ChangeLanguage() {
        if (_language == Language.English) {
            _language = Language.Dutch;
            _buttonText.text = "EN";
        }
        else {
            _language = Language.English;
            _buttonText.text = "NL";
        }

        OnLanguageChanged?.Invoke(this, new OnLanguageChangedEventArgs() {
            Language = _language
        }); ;
    }
}

public enum Language { 
    English,
    Dutch
}
