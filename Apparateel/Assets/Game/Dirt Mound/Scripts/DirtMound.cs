using UnityEngine;
using Apparateel.Crop;

public class DirtMound : MonoBehaviour
{
    private Clickable _clickable;

    [SerializeField]
    private Crop _cropPrefab;

    private bool _isOccupied = false;


    private void Awake() {
        _clickable = GetComponentInChildren<Clickable>();

        _clickable.OnClick += OnClickEvent;
    }

    private void OnDestroy() {
        _clickable.OnClick -= OnClickEvent;
    }

    private void OnClickEvent(object sender, System.EventArgs e) {
        if (_isOccupied)
            return;

        if (MoneyManager.Instance.Money < _cropPrefab.CropData.MoneyData.BuyPrice)
            return;

        MoneyManager.Instance.ItemPurchased(_cropPrefab.CropData.MoneyData.BuyPrice);
        ICrop _crop = Instantiate(_cropPrefab, transform.position, Quaternion.identity, transform);
        _crop.SetDirtMound(this);
        _isOccupied = true;
    }

    public void RemoveCrop() {
        _isOccupied = false;
    }
}
