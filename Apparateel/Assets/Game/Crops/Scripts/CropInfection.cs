using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;
using Apparateel.Utilities.Timer;

public class CropInfection : MonoBehaviour
{
    private SOCropInfectionData _infectionData;
    private SOCropGrowthData _growthData;
    private CropGrowth _cropGrowth;
    private Timer _infectionTimer;
    //public Timer InfectionTimer => _infectionTimer;

    private bool _isInfected = false;
    public bool IsInfected => _isInfected;

    private float _infectionProgress = 0;
    public float InfectionProgress => _infectionProgress;

    public float CurrentInfectionPriceModifier => _infectionData.PriceModifier * InfectionProgress;

    /// <summary>
    /// The time it takes for the infection to fully develop in seconds
    /// </summary>
    private float _infectionDevelopmentTime;

    private void Start() {
        SOCropData cropData = GetComponent<ICrop>().CropData;

        _infectionData = cropData.InfectionData;
        _growthData = cropData.GrowthData;
        _cropGrowth = GetComponent<CropGrowth>();

        _cropGrowth.OnCropGrowth += OnCropGrowthInfection;
    }

    private void Update() {
        if (!_isInfected || _infectionProgress == 1)
            return;

        _infectionTimer.Update();
        _infectionProgress = _infectionTimer.Progress;

        //Debug.Log(_infectionProgress);
    }

    private void OnCropGrowthInfection(object sender, CropGrowth.OnCropGrowthEventArgs e) {
        if (_isInfected)
            return;

        //Checking if crop gets infected. If not, return
        int chance = RNG.Instance.Next(0,100) + 1;
        //Debug.Log($"Chance: {chance}");
        if (chance > _infectionData.InfectionChance * 100) {
            return;
        }

        //Debug.Log("Infected");
        _isInfected = true;
        _infectionProgress = 0;

        //Starting a timer. Even if the crop is fully developed the crop can get more infected
        float totalGrowthTime = _growthData.GrowthPeriodLength * _growthData.GrowthStages;
        _infectionDevelopmentTime = _infectionData.InfectionDevelopmentTime * totalGrowthTime;

        _infectionTimer = new Timer(_infectionDevelopmentTime, () => { });
        _infectionTimer.Start();
    }


}
