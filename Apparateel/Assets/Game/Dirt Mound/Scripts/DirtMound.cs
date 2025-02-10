using UnityEngine;
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


    private void Awake() {
        _clickable = GetComponentInChildren<Clickable>();

        _clickable.OnClick += OnClickEvent;
    }

    private void Start() {
        _gameManager = GameManager.Instance;
        _inputManager = InputManager.Instance;
        _inputManager.OnMouseHoverObjectChange += OnMouseHoverObjectChange;
    }

    private void OnDestroy() {
        _clickable.OnClick -= OnClickEvent;
        _inputManager.OnMouseHoverObjectChange -= OnMouseHoverObjectChange;
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
}
