using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parcel : MonoBehaviour
{
    public static Parcel Instance;

    [SerializeField]
    private List<List<DirtMound>> _dirtMounds;
    /// <summary>
    /// First number is which row, second number is the DirtMound
    /// </summary>
    public List<List<DirtMound>> DirtMounds { get { return _dirtMounds; } }

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already a Parcel Instance.");
            return;
        }

        Instance = this;
    }

    private void Start(){
        _dirtMounds = new();
        foreach (Transform rowTransform in transform) {
            List<DirtMound> row = new();

            foreach (Transform mound in rowTransform) {
                DirtMound dirtMound = mound.gameObject.GetComponent<DirtMound>();

                if (dirtMound != null)
                    row.Add(dirtMound.gameObject.GetComponent<DirtMound>());
            }

            _dirtMounds.Add(row);
        }
    }
}
