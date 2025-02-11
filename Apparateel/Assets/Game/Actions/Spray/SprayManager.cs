using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;
using Apparateel.Equipment;

public class SprayManager : MonoBehaviour {

    public static SprayManager Instance;
    private InputManager _inputManager;
    private GameManager _gameManager;
    private Parcel _parcel;

    [SerializeField]
    private SOSprayEquipment _activeSprayEquipment;
    public SOSprayEquipment ActiveSprayEquipment => _activeSprayEquipment;

    [SerializeField]
    private SOSpray _sprayData;

    private List<Crop> _highlightedCrops = new();
    public List<Crop> HighlightedCrops => _highlightedCrops;

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
        _inputManager = InputManager.Instance;
        _inputManager.OnClick += OnClick;
        _inputManager.OnMouseHoverObjectChange += OnMouseHoverObjectChange;
    }

    private void OnDestroy() {
        _inputManager.OnClick -= OnClick;
        _inputManager.OnMouseHoverObjectChange -= OnMouseHoverObjectChange;
    }

    private void OnClick(object sender, InputManager.OnClickEventArgs e) {
        //Incorrect state
        if (_gameManager.State != GameState.Spraying)
            return;

        SprayHandler.Spray(_sprayData);


        //CropInfection cropInfection = e.ClickedObject.GetComponentInParent<Crop>().CropInfection;
        //cropInfection.Spray(_sprayData);
    }

    private void OnMouseHoverObjectChange(object sender, InputManager.OnMouseHoverObjectChangeEventArgs e) {
        if (_highlightedCrops.Count > 0) {
            foreach (Crop crop in _highlightedCrops) {
                crop.ToggleOutline(false);
            }
            _highlightedCrops = new();
        }

        if (_gameManager.State != GameState.Spraying || e.Clickable == null)
            return;

        Crop hoveredCrop = e.Clickable.GetComponentInParent<Crop>();
        if (!hoveredCrop)
            return;

        switch (_activeSprayEquipment.SprayType) {
            case SprayType.Singular:
                _highlightedCrops.Add(hoveredCrop);
                break;
            case SprayType.Row:
                MarkRowForHighlight(hoveredCrop);
                break;
            case SprayType.Area:
                break;
            case SprayType.None:
                Debug.LogError($"SprayType of {_activeSprayEquipment.name} is not set", this);
                break;
            default:
                throw new System.NotImplementedException();
        }

        if (_highlightedCrops.Count == 0)
            return;

        foreach (Crop crop in _highlightedCrops) {
            crop.ToggleOutline(true);
        }
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
                _highlightedCrops.Add(_parcel.PlantedCrops[rowNumber, i]);
        }
    }
}

public enum SprayEquipment {
    None,
    Default,
}
