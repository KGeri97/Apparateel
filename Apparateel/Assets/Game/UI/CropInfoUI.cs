using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;

public class CropInfoUI : MonoBehaviour
{
    public static CropInfoUI Instance;
    private List<Crop> _selectedCrops = new();
    private GameObject _cropInfoUI;

    private void Awake(){
        if (Instance != null) {
            Debug.LogError("There is already a CropInfoUI active.");
            return;
        }

        Instance = this;

        _cropInfoUI = transform.GetChild(0).gameObject;
    }

    private void Update(){
        if (_selectedCrops.Count == 0 && _cropInfoUI.activeSelf)
            _cropInfoUI.SetActive(false);
        else if (_selectedCrops.Count > 0 && !_cropInfoUI.activeSelf)
            _cropInfoUI.SetActive(true);
    }

    public void AddCropToInvestigate(Crop crop) {
        _selectedCrops.Add(crop);
    }

    public void RemoveCropFromInvestigation(Crop crop) {
        _selectedCrops.Remove(crop);
    }
}
