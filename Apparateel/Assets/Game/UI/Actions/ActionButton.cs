using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Apparateel.UI {
    public class ActionButton : MonoBehaviour {
        private Button _plantButton;
        private Image _plantButtonImage;

        private GameManager _gameManager;
        private GameVisualManager _gameVisualManager;

        private Color _selectedColor;
        private Color _deselectedColor;

        [SerializeField][Tooltip("Select the corresponding state of the button")]
        private GameState _state;

        private void Awake() {
            _plantButton = GetComponent<Button>();
            _plantButtonImage = GetComponent<Image>();

            _plantButton.onClick.AddListener(OnClick);
        }

        private void Start() {
            _gameManager = GameManager.Instance;
            _gameVisualManager = GameVisualManager.Instance;

            _gameManager.OnStateChanged += ChangeButtonColor;
            _gameManager.OnCycleStateChanged += EnableButton;
            _deselectedColor = _gameVisualManager.UIButtonDeselectedColor;
            _selectedColor = _gameVisualManager.UIButtonSelectedColor;
        }

        private void EnableButton(object sender, GameManager.OnCycleStateChangedEventArgs e) {
            if (e.NewState == CycleState.Planting)
                _plantButton.interactable = _state == GameState.Planting ? true : false;
            else if (e.NewState == CycleState.Growing)
                _plantButton.interactable = _state == GameState.Spraying || _state == GameState.Inspecting ? true : false;
            else if (e.NewState == CycleState.Harvesting)
                _plantButton.interactable = _state == GameState.Harvesting ? true : false;
        }

        private void OnClick() {
            if (_gameManager.GameState != _state)
                _gameManager.ChangeGameState(_state);
            else
                _gameManager.ChangeGameState(GameState.Running);
        }

        private void ChangeButtonColor(object sender, GameManager.OnStateChangedEventArgs e) {
            if (e.NewState == _state)
                _plantButtonImage.color = _selectedColor;
            else if (e.OldState == _state)
                _plantButtonImage.color = _deselectedColor;
        }
    }
}
