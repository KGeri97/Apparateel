using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;
using TMPro;
using UnityEngine.UI;

public class CropInfoUI : MonoBehaviour
{
    public static CropInfoUI Instance;
    private GameManager _gameManager;

    private Crop _selectedCrop;
    private GameObject _cropInfoUI;

    [SerializeField]
    private RectTransform _progressBar;
    [SerializeField]
    private UITextSO _progressText;
    [SerializeField]
    private TMP_Text _cropName;
    [SerializeField]
    private UITextSO _cropText;
    [SerializeField]
    private TMP_Text _infectedText;
    [SerializeField]
    private UITextSO _infectedTranslatedText;
    [SerializeField]
    private UITextSO _notInfectedTranslatedText;
    [SerializeField]
    private Button _closeButton;
    [SerializeField]
    private TMP_Text _sprayedText;
    [SerializeField]
    private UITextSO _sprayedTranslatedText;
    [SerializeField]
    private UITextSO _notSprayedTranslatedText;
    [SerializeField]
    private TMP_Text _chemicalProtectionText;
    [SerializeField]
    private UITextSO _chemicalProtectionTranslatedText;
    [SerializeField]
    private TMP_Text _valueText;
    [SerializeField]
    private UITextSO _valueTranslatedText;

    private int _langIndex = 0;

    private void Awake(){
        if (Instance != null) {
            Debug.LogError("There is already a CropInfoUI active.");
            return;
        }

        Instance = this;

        _cropInfoUI = transform.GetChild(0).gameObject;

        _closeButton.onClick.AddListener(CloseUI);
    }

    private void Start() {
        _gameManager = GameManager.Instance;

        InputManager.Instance.OnClick += OnClick;
    }

    private void Update(){
        if (!_cropInfoUI.activeSelf)
            return;

        if (LanguageSelector.Instance.Language == Language.English)
            _langIndex = 0;
        else
            _langIndex = 1;

        UpdateUI();
        ForceHighlight();
    }

    private void OnDestroy() {
        InputManager.Instance.OnClick -= OnClick;
    }

    private void OnClick(object sender, InputManager.OnClickEventArgs e) {
        if (e.ClickedType != ClickableType.Crop) {
            CloseUI();
            return;
        }

        if (_gameManager.State != GameState.Inspecting)
            return;

        _cropInfoUI.SetActive(true);

        if (_selectedCrop)
            _selectedCrop.Clickable.ToggleOutline(false);

        _selectedCrop = e.ClickedObject.GetComponentInParent<Crop>();
        e.ClickedObject.ToggleOutline(true);
    }

    private void UpdateUI() {
        if (!_selectedCrop) { 
            CloseUI();
            return;
        }

        UpdateName();
        UpdateProgress();
        UpdateValue();
        UpdateInfection();
    }

    private void UpdateProgress() {
        _progressBar.localScale = new Vector3(_selectedCrop.CropGrowth.GetFullGrowthProgress(), 1, 1);
    }

    private void UpdateValue() {
        _valueText.text = $"{_valueTranslatedText.Translations[_langIndex].Text}: {_selectedCrop.GetCropValue().ToString("F2")}";
    }

    private void UpdateInfection() {
        string text = "";
        CropInfection cropInfection = _selectedCrop.CropInfection;

        if (cropInfection.IsInfected)
            text = $"{_infectedTranslatedText.Translations[_langIndex].Text}";
        else
            text = $"{_notInfectedTranslatedText.Translations[_langIndex].Text }";

        _infectedText.text = text;



        text = "";

        if (cropInfection.IsSprayed)
            text = $"{_sprayedTranslatedText.Translations[_langIndex].Text}";
        else
            text = $"{_notSprayedTranslatedText.Translations[_langIndex].Text}";

        _sprayedText.text = text;



        _chemicalProtectionText.text = $"{_chemicalProtectionTranslatedText.Translations[_langIndex].Text}: {cropInfection.ChemicalProtection}%";
    }

    private void UpdateName() {
        _cropName.text = _selectedCrop.CropData.Name;
    }

    private void ForceHighlight() {
        if (!_selectedCrop)
            return;
        _selectedCrop.Clickable.ToggleOutline(true);
    }

    private void CloseUI() {
        if (_selectedCrop)
            _selectedCrop.Clickable.ToggleOutline(false);
        _selectedCrop = null;
        _cropInfoUI.SetActive(false);
        HighlightManager.ChangeHighlightedClickables(new List<Clickable>()); //Doesnt remove highlights on close
    }
}
