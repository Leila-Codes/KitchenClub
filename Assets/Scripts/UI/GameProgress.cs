using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameProgress : MonoBehaviour
    {
        public float maxScore;
        public GameManager gameManager;
        public Image filler;

        private float _maxWidth;
    
        private void Start()
        {
            gameManager.ScoreUpdated += OnScoreChanged;
        }

        private void OnScoreChanged(float newScore)
        {
            filler.rectTransform.localScale = new Vector3(
                newScore / maxScore,
                0,
                0
            );
            // filler.fillAmount = newScore / maxScore;
        }
    }
}
