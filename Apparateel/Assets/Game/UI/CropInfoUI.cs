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
    private TMP_Text _cropName;
    [SerializeField]
    private TMP_Text _infectedText;
    [SerializeField]
    private Button _closeButton;
    [SerializeField]
    private TMP_Text _sprayedText;
    [SerializeField]
    private TMP_Text _chemicalProtectionText;
    [SerializeField]
    private TMP_Text _valueText;

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
        _valueText.text = $"Value: {_selectedCrop.GetCropValue().ToString("F2")}";
    }

    private void UpdateInfection() {
        string text = "";
        CropInfection cropInfection = _selectedCrop.CropInfection;

        if (cropInfection.IsInfected)
            text = "Infected";
        else
            text = "Not infected";

        _infectedText.text = text;



        text = "";

        if (cropInfection.IsSprayed)
            text = "Sprayed";
        else
            text = "Not sprayed";

        _sprayedText.text = text;



        _chemicalProtectionText.text = $"Chemical Protection: {cropInfection.ChemicalProtection}%";
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
        _selectedCrop = null;
        _cropInfoUI.SetActive(false);
        HighlightManager.ChangeHighlightedClickables(new List<Clickable>());
    }
}
