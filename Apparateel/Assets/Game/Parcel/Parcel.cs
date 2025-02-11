using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;

public class Parcel : MonoBehaviour
{
    public static Parcel Instance;

    //[SerializeField]
    private DirtMound[,] _dirtMounds;
    /// <summary>
    /// First number is which row, second number is the DirtMound
    /// </summary>
    public DirtMound[,] DirtMounds { get { return _dirtMounds; } }

    private Crop[,] _plantedCrops;
    /// <summary>
    /// First number is which row, second number is the DirtMound
    /// </summary>
    public Crop[,] PlantedCrops => _plantedCrops;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already a Parcel Instance.");
            return;
        }

        Instance = this;

        DirtMound.OnCropPlanted += AddCropToParcel;
        Crop.OnCropHarvested += OnCropHarvested;
    }

    private void Start() {
        //Getting the references for every DirtMound
        List<List<DirtMound>> dirtMounds = new();
        foreach (Transform rowTransform in transform) {
            List<DirtMound> row = new();

            foreach (Transform mound in rowTransform) {
                DirtMound dirtMound = mound.gameObject.GetComponent<DirtMound>();

                if (dirtMound != null)
                    row.Add(dirtMound.gameObject.GetComponent<DirtMound>());
            }
            dirtMounds.Add(row);
        }
        //Debug.Log(dirtMounds.Count);

        _dirtMounds = new DirtMound[dirtMounds.Count, dirtMounds[0].Count];
        _plantedCrops = new Crop[dirtMounds.Count, dirtMounds[0].Count];

        for (int i = 0; i < dirtMounds.Count; i++) {
            for (int j = 0; j < dirtMounds[i].Count; j++) {
                _dirtMounds[i, j] = dirtMounds[i][j];
                _dirtMounds[i, j].SetPosition(i, j);
            }
        }
    }

    private void OnDestroy() {
        DirtMound.OnCropPlanted -= AddCropToParcel;
        Crop.OnCropHarvested -= OnCropHarvested;
    }

    private void AddCropToParcel(object sender, DirtMound.OnCropPlantedEventArgs e) {
        _plantedCrops[e.RowPosition, e.Position] = e.Crop;
    }

    private void OnCropHarvested(object sender, Crop.OnCropHarvestedEventArgs e) {
        _plantedCrops[e.RowPosition, e.Position] = null;
    }
}
