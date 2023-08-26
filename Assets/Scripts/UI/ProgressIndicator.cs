using System.Collections;
using UnityEngine;

namespace UI
{
    public class ProgressIndicator : MonoBehaviour
    {
        public float value;

        private RectTransform _container;
        private RectTransform _uiIndicator;
        private float _initOffset;

        private void Start()
        {
            _container = GetComponent<RectTransform>();
            _uiIndicator = transform.GetChild(0).GetComponent<RectTransform>();

            _initOffset = (-_container.rect.width / 2);
        }

        // Resize UI Indicator based on progress
        // Assumes progress is a decimal percentage (0 -> 1)
        private void Update()
        {
            float newScale = Mathf.Lerp(_uiIndicator.localScale.x, value, Time.deltaTime),
                newOffset = (1 - value) * _initOffset,
                newPos = Mathf.Lerp(_uiIndicator.localPosition.x, newOffset, Time.deltaTime);

            if (newScale > 0.99f) newScale = 1;
            if (newPos > -0.1f) newPos = 0;
            
            _uiIndicator.localScale = new Vector3(
                newScale,
                1,
                1
            );

            _uiIndicator.localPosition = new Vector3(
                newPos,
                _uiIndicator.localPosition.y,
                _uiIndicator.localPosition.z
            );
        }
    }
}