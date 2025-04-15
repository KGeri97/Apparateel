using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;
using Apparateel.Equipment;

public class SprayManager : MonoBehaviour, ICanAskYesNoQuestion {

    public static SprayManager Instance;
    private GameManager _gameManager;
    private Parcel _parcel;

    //[SerializeField]
    //private SOSprayEquipment _activeSprayEquipment;
    //public SOSprayEquipment ActiveSprayEquipment => _activeSprayEquipment;

    [SerializeField]
    private SOSpray _sprayData;

    private List<Clickable> _highlightedClickables = new();
    public List<Clickable> HighlightedClickables => _highlightedClickables;

    [SerializeField]
    private AreYouSure _areYouSurePopUpWindow;

    //private SprayType

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already an EquipmentManager Instance.");
            return;
        }

        Instance = this;
    }

    private void Start() {
        _parcel = Parcel.Instance;
        _gameManager = GameManager.Instance;
        InputManager.Instance.OnClick += OnClick;
        InputManager.Instance.OnMouseHoverObjectChange += OnMouseHoverObjectChange;
        _gameManager.OnStateChanged += OnStateChanged;
    }

    private void OnDestroy() {
        InputManager.Instance.OnClick -= OnClick;
        InputManager.Instance.OnMouseHoverObjectChange -= OnMouseHoverObjectChange;
        _gameManager.OnStateChanged += OnStateChanged;
    }

    private void OnClick(object sender, InputManager.OnClickEventArgs e) {
        //Incorrect state
        if (_gameManager.GameState != GameState.Spraying)
            return;

        Spray(_sprayData);
    }

    private void OnMouseHoverObjectChange(object sender, InputManager.OnMouseHoverObjectChangeEventArgs e) {
        if (_highlightedClickables.Count > 0) {
            foreach (Clickable clickable in _highlightedClickables) {
                clickable.ToggleOutline(false);
            }
            _highlightedClickables = new();
        }

        if (_gameManager.GameState != GameState.Spraying || e.Clickable == null)
            return;

        Crop hoveredCrop = e.Clickable.GetComponentInParent<Crop>();
        if (!hoveredCrop)
            return;

        ApplianceMethod sprayMethod = ApplianceMethod.Singular;
        EquipmentData equipmentData = EquipmentManager.Instance.GetActiveEquipment(ApplianceAction.Spraying);
        if (equipmentData == null)
            sprayMethod = ApplianceMethod.Singular;
        else {
            ApplianceMethod sprayMethodModifier = equipmentData.GetApplianceMethod(ApplianceAction.Spraying);
            if (sprayMethodModifier != ApplianceMethod.None)
                sprayMethod = sprayMethodModifier;
        }

        switch (sprayMethod) {
            case ApplianceMethod.Singular:
                _highlightedClickables.Add(e.Clickable);
                break;
            case ApplianceMethod.Row:
                MarkRowForHighlight(hoveredCrop);
                break;
            case ApplianceMethod.Area:
                throw new System.NotImplementedException();
            case ApplianceMethod.None:
                Debug.LogError($"SprayType of the current spray equipment is not set");
                break;
            case ApplianceMethod.Field:
                break;
            default:
                throw new System.NotImplementedException();
        }

        if (_highlightedClickables.Count == 0)
            return;

        HighlightManager.ChangeHighlightedClickables(_highlightedClickables);
    }

    private void MarkRowForHighlight(Crop crop) {
        int rowNumber = -1;
        for (int row = 0; row < _parcel.PlantedCrops.GetLength(0); row++) {
            for (int column = 0; column < _parcel.PlantedCrops.GetLength(1); column++) {
                if (_parcel.PlantedCrops[row, column] == crop)
                    rowNumber = row;
            }
        }

        if (rowNumber < 0) {
            Debug.LogError("The highlighted plant does not exist.");
            return;
        }

        for (int i = 0; i < _parcel.PlantedCrops.GetLength(1); i++) {
            if (_parcel.PlantedCrops[rowNumber, i])
                _highlightedClickables.Add(_parcel.PlantedCrops[rowNumber, i].Clickable);
        }
    }

    public void Spray(SOSpray sprayData) {
        foreach (Clickable clickable in _highlightedClickables) {
            if (!MoneyManager.Instance.ItemPurchased(GetModifiedSprayCost(_sprayData)))
                return;

            clickable.GetComponentInParent<CropInfection>().Spray(sprayData);
            Vector3 spawnLocation = Vector3.Lerp(clickable.transform.position, Camera.main.transform.position, 0.2f);
            MoneyPopUpManager.Instance.CreatePopUp(-GetModifiedSprayCost(_sprayData), spawnLocation);
            AudioManager.Instance.PlaySound(2);
        }
    }

    private void OnStateChanged(object sender, GameManager.OnStateChangedEventArgs e) {
        EquipmentData activeEquipment = EquipmentManager.Instance.GetActiveEquipment(ApplianceAction.Spraying);
        if (e.NewState != GameState.Spraying || activeEquipment == null || activeEquipment.GetApplianceMethod(ApplianceAction.Spraying) != ApplianceMethod.Field)
            return;

        _areYouSurePopUpWindow.gameObject.SetActive(true);
        _areYouSurePopUpWindow.SetAskingObject(this);
    }

    public void ResponseReceived(bool response) {
        _gameManager.ChangeGameState(GameState.Running);
        if (!response)
            return;

        //_parcel.PlantedCrops[0, 0].CropInfection.Spray(_sprayData);

        foreach (Crop crop in _parcel.PlantedCrops) {
            if (!MoneyManager.Instance.ItemPurchased(GetModifiedSprayCost(_sprayData)))
                return;

            if (crop == null)
                continue;

            crop.CropInfection.Spray(_sprayData);
        }
    }

    private float GetModifiedSprayCost(SOSpray sprayData) {
        float modifier = EquipmentManager.Instance.GetActiveModifier(ModifiableStats.SprayCostPerPlant);

        if (modifier == -1)
            return sprayData.CostPerPlant;
        else
            return sprayData.CostPerPlant * modifier;
    }
}
