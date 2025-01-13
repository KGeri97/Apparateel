using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNG : MonoBehaviour
{
    public static RNG Instance;

    private System.Random _rng;

    private void Awake() {
        _rng = new System.Random();
    }

    private void Start(){
        if (Instance != null) {
            Debug.LogError("There is already a MoneyManager Instance.");
            return;
        }

        Instance = this;
    }

    public int Next(int minValue, int maxValue) {
        return _rng.Next(minValue, maxValue);
    }
}
