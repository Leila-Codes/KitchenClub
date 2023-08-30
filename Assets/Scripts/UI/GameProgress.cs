using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameProgress : MonoBehaviour
    {
        public GameConfig gameConfig;
        public GameManager gameManager;
        public Image filler;

        private float _newScale = 0;
    
        private void Start()
        {
            gameManager.ScoreUpdated += OnScoreChanged;
        }

        private void Update()
        {
            float newScaleX = Mathf.MoveTowards(filler.rectTransform.localScale.x, _newScale, Time.deltaTime);
            if (Mathf.Approximately(Mathf.Abs(_newScale - newScaleX), .1f))
            {
                newScaleX = _newScale;
            }

            filler.rectTransform.localScale = new Vector3(newScaleX, 1, 1);
        }

        private void OnScoreChanged(float newScore)
        {
            _newScale = newScore / gameConfig.maxScore;
        }
    }
}
