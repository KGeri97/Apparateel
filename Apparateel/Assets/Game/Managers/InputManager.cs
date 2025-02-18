using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private GameObject _lastSelectedObject;

    public event EventHandler<OnClickEventArgs> OnClick;
    public class OnClickEventArgs {
        /// <summary>
        /// The Clickable that was clicked
        /// </summary>
        public Clickable ClickedObject;
        /// <summary>
        /// The ClickableType of the Clickable
        /// </summary>
        public ClickableType ClickedType;
        /// <summary>
        /// The mouse position at the time of the click
        /// </summary>
        public Vector2 MousePosition;
        /// <summary>
        /// The world position the mouse is pointed at, at the time of the click
        /// </summary>
        public Vector3 WorldPosition;
    }

    public event EventHandler<OnMouseHoverObjectChangeEventArgs> OnMouseHoverObjectChange;
    public class OnMouseHoverObjectChangeEventArgs : EventArgs {
        public GameObject HoveredObject;
        public Clickable Clickable;
    }


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is already an InputManager Instance.");
            return;
        }

        Instance = this;
    }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool didHit = Physics.Raycast(ray, out hit);
        GameObject gameObjectHit = didHit ? hit.transform.gameObject : null;
        Clickable clickable = null;

        if (didHit)
            gameObjectHit.TryGetComponent(out clickable);


        #region MouseHover
        MouseHover(gameObjectHit, clickable);
        #endregion

        #region MouseClick
        MouseClick(clickable, hit);
        #endregion
    }

    private void MouseHover(GameObject gameObjectHit, Clickable clickable) {
        if (_lastSelectedObject == gameObjectHit)
            return;

        _lastSelectedObject = gameObjectHit;
        OnMouseHoverObjectChange?.Invoke(this, new OnMouseHoverObjectChangeEventArgs() {
            HoveredObject = gameObjectHit,
            Clickable = clickable
        });
    }

    private void MouseClick(Clickable clickable, RaycastHit hit) {
        if (Input.GetMouseButtonDown(0)) {
            if (clickable == null)
                return;

            OnClick?.Invoke(this, new OnClickEventArgs {
                ClickedObject = clickable,
                ClickedType = clickable.ClickableType,
                MousePosition = Input.mousePosition,
                WorldPosition = hit.point
            });

            clickable.InvokeOnClick();
        }
    }
}
