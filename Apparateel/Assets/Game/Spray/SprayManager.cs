using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;

public class SprayManager : MonoBehaviour {

    public static SprayManager Instance;
    private InputManager _inputManager;
    private GameManager _gameManager;

    [SerializeField]
    private SOSpray _sprayData;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already an EquipmentManager Instance.");
            return;
        }

        Instance = this;
    }

    private void Start() {
        _gameManager = GameManager.Instance;
        _inputManager = InputManager.Instance;
        _inputManager.OnClick += OnClick;
    }

    private void OnDestroy() {
        _inputManager.OnClick -= OnClick;
    }

    private void OnClick(object sender, InputManager.OnClickEventArgs e) {
        if (_gameManager.State != GameState.Spraying || e.ClickedType != ClickableType.Crop)
            return;

        CropInfection cropInfection = e.ClickedObject.GetComponentInParent<Crop>().CropInfection;

        cropInfection.Spray(_sprayData);
    }
}
