using UnityEngine;
using Apparateel.Crop;

public class DirtMound : MonoBehaviour
{
    private GameManager _gameManager;
    private Clickable _clickable;

    [SerializeField]
    private Crop _cropPrefab;

    private ICrop _occupyingCrop;
    private bool _isOccupied = false;


    private void Awake() {
        _clickable = GetComponentInChildren<Clickable>();

        _clickable.OnClick += OnClickEvent;
    }

    private void Start() {
        _gameManager = GameManager.Instance;
    }

    private void OnDestroy() {
        _clickable.OnClick -= OnClickEvent;
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

        if (MoneyManager.Instance.Money < _cropPrefab.CropData.MoneyData.BuyPrice)
            return;

        MoneyManager.Instance.ItemPurchased(_cropPrefab.CropData.MoneyData.BuyPrice);
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
}
