using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Apparateel.Utilities.Timer;
using Apparateel.Crop;

public class CropGrowth : MonoBehaviour {

    [SerializeField][Tooltip("The gameobject that contains all the visuals")]
    private Transform _visuals;

    private ICrop _crop;
    private SOCropGrowthData _growthData;

    private Timer _growthPeriodTimer;
    public Timer GrowthPeriodTimer => _growthPeriodTimer;

    private int _currentGrowthStage = 1;

    private Vector3 _currentBeginScale;
    private Vector3 _currentEndScale;

    private bool _isGrowing;
    public bool IsGrowing => _isGrowing;

    public event EventHandler<OnCropGrowthEventArgs> OnCropGrowth;
    //For now I dont need args but Im doing this for learning purposes
    public class OnCropGrowthEventArgs : EventArgs {
        public int GrowthStage;
    }

    private void Start() {
        _crop = GetComponent<ICrop>();
        _growthData = _crop.CropData.GrowthData;

        SetGrowthParams();
        _isGrowing = true;

        _growthPeriodTimer = new Timer(_growthData.GrowthPeriodLength, AdvanceGrowthPeriod);
        _growthPeriodTimer.Repeat(false);
        _growthPeriodTimer.Start();
    }

    private void Update(){
        if (_isGrowing) {
            _growthPeriodTimer.Update();
            _visuals.localScale = Vector3.Lerp(_currentBeginScale, _currentEndScale, _growthPeriodTimer.Progress);
        }
    }

    private void SetGrowthParams() {
        Vector3 growthPerStage = (_growthData.EndScale - _growthData.BeginScale) / _growthData.GrowthStages;

        _currentBeginScale = _growthData.BeginScale + growthPerStage * (_currentGrowthStage - 1);
        _currentEndScale = _growthData.BeginScale + growthPerStage * _currentGrowthStage;
    }

    /// <summary>
    /// Advances the plant into the new growth stage
    /// </summary>
    private void AdvanceGrowthPeriod() {
        if (_currentGrowthStage == _growthData.GrowthStages) {
            _isGrowing = false;
            return;
        }

        _currentGrowthStage++;
        SetGrowthParams();

        OnCropGrowth?.Invoke(this, new OnCropGrowthEventArgs {
            GrowthStage = _currentGrowthStage
        });

        _growthPeriodTimer.Reset();
        _growthPeriodTimer.Start();
    }

    public float GetFullGrowthProgress() {
        float totalGrowthTime = _growthData.FullGrowthTime;
        float progress = (_currentGrowthStage - 1) * _growthData.GrowthPeriodLength + _growthPeriodTimer.Progress * _growthData.GrowthPeriodLength;

        //Debug.Log(progress / totalGrowthTime);
        return progress / totalGrowthTime;
    }
}
