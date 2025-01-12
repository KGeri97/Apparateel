using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;

public class CropColor : MonoBehaviour
{
    private SOCropInfectionData _infectionData;
    private SOCropGrowthData _growthData;
    private CropGrowth _cropGrowth;
    private CropInfection _cropInfection;

    [SerializeField]
    private MeshRenderer _meshRenderer;


    private void Start(){
        SOCropData cropData = GetComponent<ICrop>().CropData;
        _infectionData = cropData.InfectionData;
        _growthData = cropData.GrowthData;

        _cropGrowth = GetComponent<CropGrowth>();
        _cropInfection = GetComponent<CropInfection>();
    }

    private void Update(){
        if (!_cropGrowth.IsGrowing && _cropInfection.InfectionProgress >= 1)
            return;

        _meshRenderer.material.color = Color.Lerp(_growthData.BeginColor, _growthData.EndColor, _cropGrowth.GetFullGrowthProgress());
        _meshRenderer.material.color = Color.Lerp(_meshRenderer.material.color, _infectionData.InfectionColor, _infectionData.InfectionVisibility.Evaluate(_cropInfection.InfectionProgress));
    }
}
