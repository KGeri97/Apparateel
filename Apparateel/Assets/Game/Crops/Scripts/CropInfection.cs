using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Apparateel.Crop;
using Apparateel.Utilities.Timer;
using Apparateel.Equipment;

public class CropInfection : MonoBehaviour
{
    private SOCropInfectionData _infectionData;
    private SOCropGrowthData _growthData;
    private CropGrowth _cropGrowth;
    private Timer _infectionTimer;
    //public Timer InfectionTimer => _infectionTimer;

    private bool _isInfected = false;
    public bool IsInfected => _isInfected;

    private bool _isSprayed = false;
    public bool IsSprayed => _isSprayed;

    private SOSpray _sprayedWith;
    public SOSpray SprayedWith => _sprayedWith;
    private int _timesSprayed = 0;
    public int TimesSprayed => _timesSprayed;

    private float _infectionProgress = 0;
    public float InfectionProgress => _infectionProgress;

    public float CurrentInfectionPriceModifier => _infectionData.PriceModifier * InfectionProgress;

    /// <summary>
    /// The time it takes for the infection to fully develop in seconds
    /// </summary>
    private float _infectionDevelopmentTime;

    private float _naturalResistance;
    public float NaturalResistance => _naturalResistance;
    private float _chemicalProtection = 0;
    public float ChemicalProtection => _chemicalProtection;

    private void Start() {
        SOCropData cropData = GetComponent<ICrop>().CropData;

        _infectionData = cropData.InfectionData;
        _growthData = cropData.GrowthData;
        _cropGrowth = GetComponent<CropGrowth>();

        _cropGrowth.OnCropGrowth += OnCropGrowth;

        _naturalResistance = _infectionData.NaturalResistance;
    }

    private void Update() {
        if (!_isInfected || _infectionProgress == 1)
            return;

        _infectionTimer.Update();
        _infectionProgress = _infectionTimer.Progress;

        //Debug.Log(_infectionProgress);
    }

    private void OnCropGrowth(object sender, CropGrowth.OnCropGrowthEventArgs e) {
        ReduceChemicalProtection(_sprayedWith);
        Infection();
    }

    private void Infection() {
        if (_isInfected)
            return;

        //Checking if crop gets infected. If not, return
        float chance = RNG.Next(0, 100) + 1;
        chance += _naturalResistance * 100;
        chance += _chemicalProtection * 100;

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

    public void Spray(SOSpray sprayedWithData) {
        _isSprayed = true;
        _timesSprayed++;
        _sprayedWith = sprayedWithData;

        //Applying MaxProtection modifier
        float maxProtectionModifier = EquipmentManager.Instance.GetActiveModifier(ModifiableStats.SprayMaxProtection);
        _chemicalProtection = maxProtectionModifier == -1 ? sprayedWithData.MaxProtection : sprayedWithData.MaxProtection * maxProtectionModifier;
    }

    private void ReduceChemicalProtection(SOSpray sprayedWithData) {
        if (!_isSprayed)
            return;


        //Applying ProtectionDecline modifier
        float protectionDeclineModifier = EquipmentManager.Instance.GetActiveModifier(ModifiableStats.SprayProtectionDecline);
        float actualProtectionDecline = protectionDeclineModifier == -1 ? sprayedWithData.ProtectionDecline : sprayedWithData.ProtectionDecline * protectionDeclineModifier;

        _chemicalProtection -= actualProtectionDecline;
        if (_chemicalProtection < 0)
            _chemicalProtection = 0;
    }
}
