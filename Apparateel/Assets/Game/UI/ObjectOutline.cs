using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOutline : MonoBehaviour
{
    [SerializeField]
    private Transform _originalObject;
    [SerializeField][Tooltip("If this is a crop then assign the Visuals transform otherwise this can be the same as _originalObject")]
    private Transform _scalerObject;

    private float _outlineThickness;

    private void Start() {
        _outlineThickness = GameVisualManager.Instance.OutlineThickness;
    }

    private void Update() {
        transform.position = _originalObject.position;
        transform.localScale = _scalerObject.localScale + new Vector3(_outlineThickness, _outlineThickness, _outlineThickness);
    }
}
