using UnityEngine;
using Apparateel.Crop;

public class DirtMound : MonoBehaviour
{
    private Clickable _clickable;

    [SerializeField]
    private GameObject _cropPrefab;
    private ICrop _crop;

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

        _crop = Instantiate(_cropPrefab, transform.position, Quaternion.identity, transform).GetComponent<ICrop>();
        _crop.SetDirtMound(this);
        _isOccupied = true;
    }

    public void RemoveCrop() {
        _isOccupied = false;
    }
}
