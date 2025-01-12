using UnityEngine;
using System;

//Not used

/// <summary>
/// Event on which the input handler sends the reference of a clickable gameobject.
/// </summary>

[CreateAssetMenu(menuName = "Events/Click Event Channel")]
public class InputChannelSO : ScriptableObject
{
    public Action<GameObject> OnClickEventRaised;

    public void RaiseClickEvent(GameObject gameObject) {
        OnClickEventRaised?.Invoke(gameObject);
    }
}
