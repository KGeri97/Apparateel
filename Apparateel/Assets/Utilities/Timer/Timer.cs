using System;
using UnityEngine;

namespace Apparateel.Utilities.Timer {
    public class Timer {
        private float _duration;
        private bool _isRunning;
        private bool _isRepeating;
        private float _elapsedTime;
        private Action _callback;

        public float Duration => _duration;
        public float Elapsed => _elapsedTime;
        public float Progress => _elapsedTime / _duration;
        public bool IsRunning => _isRunning;
        public bool IsRepeating => _isRepeating;

        public Timer(float duration, Action callback) {
            _duration = duration;
            _callback = callback;
            _isRunning = false;
            _isRepeating = false;
            _elapsedTime = 0f;
        }

        public void Start() {
            _isRunning = true;
        }

        public void Update() {
            if (_isRunning) {
                _elapsedTime += Time.deltaTime;

                if (_elapsedTime >= _duration) {
                    _elapsedTime = _duration;
                    _isRunning = false;
                    _callback.Invoke();

                    if (_isRepeating) {
                        Reset();
                        Start();
                    }
                }
            }
        }

        public void Reset() {
            _elapsedTime = 0f;
            _isRunning = false;
        }

        public void Stop() {
            _isRunning = false;
        }

        public void SetDuration(float newDuration) {
            _duration = newDuration;
        }

        public void Repeat(bool value) {
            _isRepeating = value;
        }

        public void SetCallback(Action newCallback) {
            _callback = newCallback;
        }
    }
}