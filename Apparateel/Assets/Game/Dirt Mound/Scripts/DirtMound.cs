using UnityEngine;
using System;
using Apparateel.Crop;

public class DirtMound : MonoBehaviour, IHasOutline
{
    private GameManager _gameManager;
    private InputManager _inputManager;
    private Clickable _clickable;

    [SerializeField]
    private GameObject _outline;

    [SerializeField]
    private Crop _cropPrefab;

    private Crop _occupyingCrop;
    public Crop OccupyingCrop => _occupyingCrop;
    private bool _isOccupied = false;

    private int _rowPosition;
    public int RowPosition => _rowPosition;
    private int _position;
    public int Position => _position;

    public static event EventHandler<OnCropPlantedEventArgs> OnCropPlanted;
    public class OnCropPlantedEventArgs : EventArgs {
        public Crop Crop;
        public int RowPosition;
        public int Position;
    }


    private void Awake() {
        _clickable = GetComponentInChildren<Clickable>();

        _clickable.OnClick += OnClickEvent;

        Crop.OnCropHarvested += OnCropHarvested;
    }

    private void Start() {
        _gameManager = GameManager.Instance;
        _inputManager = InputManager.Instance;
        _inputManager.OnMouseHoverObjectChange += OnMouseHoverObjectChange;
    }

    private void OnDestroy() {
        _clickable.OnClick -= OnClickEvent;
        _inputManager.OnMouseHoverObjectChange -= OnMouseHoverObjectChange;
        Crop.OnCropHarvested -= OnCropHarvested;
    }

    private void OnClickEvent(object sender, System.EventArgs e) {
        switch (_gameManager.State) {
            case GameState.Planting:
                PlantCrop();
                break;
            case GameState.Fertilizing:
                FertilizeCrop();
                break;
        }
    }

    private void PlantCrop() {
        if (_isOccupied)
            return;

        if (!MoneyManager.Instance.ItemPurchased(_cropPrefab.CropData.MoneyData.BuyPrice))
            return;

        _occupyingCrop = Instantiate(_cropPrefab, transform.position, Quaternion.identity, transform);
        _occupyingCrop.SetDirtMound(this);
        _isOccupied = true;

        OnCropPlanted?.Invoke(this, new OnCropPlantedEventArgs() {
            Crop = _occupyingCrop,
            RowPosition = _rowPosition,
            Position = _position
        });
    }

    private void FertilizeCrop() {

    }

    public void RemoveCrop() {
        _occupyingCrop = null;
        _isOccupied = false;
    }

    public void ToggleOutline(bool active) {
        _outline.SetActive(active);
    }

    private void OnMouseHoverObjectChange(object sender, InputManager.OnMouseHoverObjectChangeEventArgs e) {
        if ((_gameManager.State == GameState.Planting || _gameManager.State == GameState.Fertilizing)
            && e.Clickable == _clickable)
            ToggleOutline(true);
        else
            ToggleOutline(false);
    }

    public void SetPosition(int row, int position) {
        _rowPosition = row;
        _position = position;
    }

    private void OnCropHarvested(object sender, Crop.OnCropHarvestedEventArgs e) {
        RemoveCrop();
    }
}
