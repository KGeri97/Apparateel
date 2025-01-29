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
            _deselectedColor = _gameVisualManager.UIButtonDeselectedColor;
            _selectedColor = _gameVisualManager.UIButtonSelectedColor;
        }

        private void OnClick() {
            if (_gameManager.State != _state)
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
