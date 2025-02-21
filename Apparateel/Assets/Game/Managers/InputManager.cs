using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already an InputManager Instance.");
            return;
        }

        Instance = this;
    }

    private void Update() {
        if (Input.touchCount != 1)
            return;

        Touch touch = Input.touches[0];

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Transform hitTransform = hit.transform;
            if (hitTransform == null)
                return;
            Clickable clickable = hitTransform.gameObject.GetComponent<Clickable>();
            if (clickable == null)
                return;

            clickable.InvokeOnClick();
        }
    }
}
