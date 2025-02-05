using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public event EventHandler<OnClickEventArgs> OnClick;
    public class OnClickEventArgs {
        public Clickable ClickedObject;
        public ClickableType ClickedType;
    }


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already an InputManager Instance.");
            return;
        }

        Instance = this;
    }

    private void Update() {
        //if (Input.touchCount != 1)
        //    return;

        //Touch touch = Input.touches[0];

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Transform hitTransform = hit.transform;
                if (hitTransform == null)
                    return;
                Clickable clickable = hitTransform.gameObject.GetComponent<Clickable>();
                if (clickable == null)
                    return;

                OnClick?.Invoke(this, new OnClickEventArgs{ 
                    ClickedObject = clickable,
                    ClickedType = clickable.ClickableType
                });

                clickable.InvokeOnClick();
            }
        }
    }
}
