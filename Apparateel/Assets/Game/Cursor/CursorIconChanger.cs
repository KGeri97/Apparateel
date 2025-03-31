using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorIconChanger : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorTexturePlant;
    [SerializeField]
    private Texture2D _cursorTextureSpray;
    [SerializeField]
    private Texture2D _cursorTextureInspect;
    //Flaticon https://www.flaticon.com/uicons
    [SerializeField][Tooltip("https://www.flaticon.com/uicons")]
    private Texture2D _cursorTextureHarvest;
    [SerializeField]
    private Vector2 _hotSpot;

    private void Start(){
        GameManager.Instance.OnStateChanged += StateChanged;
    }

    private void OnDestroy() {
        GameManager.Instance.OnStateChanged -= StateChanged;
    }

    private void StateChanged(object sender, GameManager.OnStateChangedEventArgs e) {
        switch (e.NewState) {
            case GameState.Running:
                ChangeCursorTexture(null);
                break;
            case GameState.Planting:
                ChangeCursorTexture(_cursorTexturePlant);
                break;
            case GameState.Spraying:
                ChangeCursorTexture(_cursorTextureSpray);
                break;
            case GameState.Inspecting:
                ChangeCursorTexture(_cursorTextureInspect);
                break;
            case GameState.Harvesting:
                ChangeCursorTexture(_cursorTextureHarvest);
                break;
        }
    }

    private void ChangeCursorTexture(Texture2D texture) {
        Cursor.SetCursor(texture, _hotSpot, CursorMode.Auto);
    }
}
