using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropOutline : MonoBehaviour
{
    [SerializeField]
    private Transform _originalCropObject;
    [SerializeField]
    private Transform _scalerObject;

    private float _outlineThickness;

    private void Start() {
        _outlineThickness = GameVisualManager.Instance.OutlineThickness;
    }

    private void Update() {
        transform.position = _originalCropObject.position;
        transform.localScale = _scalerObject.localScale + new Vector3(_outlineThickness, _outlineThickness, _outlineThickness);
    }
}
