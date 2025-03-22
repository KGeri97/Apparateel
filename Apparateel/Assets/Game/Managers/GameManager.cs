using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField]
    private GameState _state;
    public GameState State => _state;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs {
        public GameState OldState;
        public GameState NewState;
    }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already a GameManager active.");
            return;
        }

        Instance = this;

        _state = GameState.Running;
    }

    public void ChangeGameState(GameState newState) {
        if (_state == newState)
            return;

        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            OldState = _state,
            NewState = newState
        });

        _state = newState;
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
