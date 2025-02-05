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
    private InputManager _inputManager;

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

        _inputManager = InputManager.Instance;
        _inputManager.OnClick += OnClick;
    }

    private void Update(){
        if (!_cropInfoUI.activeSelf)
            return;

        UpdateUI();
    }

    private void OnDestroy() {
        _inputManager.OnClick -= OnClick;
    }

    private void OnClick(object sender, InputManager.OnClickEventArgs e) {
        if (_gameManager.State != GameState.Inspecting || e.ClickedType != ClickableType.Crop) {
            CloseUI();
            return;
        }

        _selectedCrop = e.ClickedObject.GetComponentInParent<Crop>();
        _cropInfoUI.SetActive(true);
    }

    private void UpdateUI() {
        UpdateProgress();
        UpdateInfection();
        UpdateName();
    }

    private void UpdateProgress() {
        _progressBar.localScale = new Vector3(_selectedCrop.CropGrowth.GetFullGrowthProgress(), 1, 1);
    }

    private void UpdateInfection() {
        string text = "";

        if (_selectedCrop.CropInfection.IsInfected)
            text = "Infected";
        else
            text = "Not infected";

        _infectedText.text = text;
    }

    private void UpdateName() {
        _cropName.text = _selectedCrop.CropData.Name;
    }

    private void CloseUI() {
        _selectedCrop = null;
        _cropInfoUI.SetActive(false);
    }
}
