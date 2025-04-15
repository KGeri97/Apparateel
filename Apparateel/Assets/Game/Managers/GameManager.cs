using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Apparateel.Crop;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField]
    private GameState _gameState;
    public GameState GameState => _gameState;
    [SerializeField]
    private CycleState _cycleState;
    public CycleState CycleState => _cycleState;

    private Parcel _parcel;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs {
        public GameState OldState;
        public GameState NewState;
    }

    public event EventHandler<OnCycleStateChangedEventArgs> OnCycleStateChanged;
    public class OnCycleStateChangedEventArgs {
        public CycleState OldState;
        public CycleState NewState;
    }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already a GameManager active.");
            return;
        }

        Instance = this;

        _gameState = GameState.Running;
        _cycleState = CycleState.Planting;
    }

    private void Start() {
        _parcel = Parcel.Instance;
    }

    private void Update() {
        //if (_cycleState == CycleState.Growing) {
        //    foreach (Crop crop in _parcel.PlantedCrops) {
        //        if (crop.CropGrowth.GetFullGrowthProgress() == 1) {
        //            ChangeCycleState(CycleState.Harvesting);
        //            return;
        //        }
        //    }
        //}

        //if (_cycleState == CycleState.Harvesting &&
        //    _parcel.PlantedCrops.)
    }

    public void ChangeGameState(GameState newState) {
        if (_gameState == newState)
            return;

        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            OldState = _gameState,
            NewState = newState
        });

        _gameState = newState;
    }
    public void ChangeCycleState(CycleState newState) {
        if (_cycleState == newState)
            return;

        OnCycleStateChanged?.Invoke(this, new OnCycleStateChangedEventArgs {
            OldState = _cycleState,
            NewState = newState
        });

        _cycleState = newState;
    }
}

public enum GameState { 
    None,
    Running,
    InMenu,
    InBarnMenu,
    Planting,
    Fertilizing,
    Spraying,
    Inspecting,
    Harvesting
}

public enum CycleState {
    Planting,
    Growing,
    Harvesting
}
