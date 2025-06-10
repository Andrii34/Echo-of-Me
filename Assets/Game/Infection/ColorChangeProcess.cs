using UnityEngine;


    public class ColorChangeProcess : IInfectionProcess
    {
        private Renderer _renderer;
        private Color _startColor;
        private Color _targetColor;
        private float _duration;
        private float _elapsedTime;

        public ColorChangeProcess(Renderer renderer, Color targetColor, float duration)
        {
            _renderer = renderer;
            _startColor = renderer.material.color;
            _targetColor = targetColor;
            _duration = duration;
            _elapsedTime = 0f;
        }

        public void UpdateProcess(float deltaTime)
        {
            if (_elapsedTime < _duration)
            {
                _elapsedTime += deltaTime;
                float t = Mathf.Clamp01(_elapsedTime / _duration);
                _renderer.material.color = Color.Lerp(_startColor, _targetColor, t);
            }
        }

        public void OnInfectionEnd()
        {
            
            _renderer.material.color = _targetColor;
        }
    }


