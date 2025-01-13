using UnityEngine;
using System;

//Not used

/// <summary>
/// Event on which the input handler sends the reference of a clickable gameobject.
/// </summary>

[CreateAssetMenu(menuName = "Events/Click Event Channel")]
public class InputChannelSO : ScriptableObject
{
    public event EventHandler<OnClickEventArgs> OnClick;

    public void RaiseClickEvent(object sender, Clickable clickedObject) {
        OnClick?.Invoke(sender, new OnClickEventArgs { 
            ClickedObject = clickedObject
        });
    }
    public class OnClickEventArgs : EventArgs {
        public Clickable ClickedObject;
    }
}
