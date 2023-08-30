using Dining;
using UnityEngine;

namespace Game
{
    public delegate void ScoreChanged(float newScore);

    public delegate void GameEnded();
    
    public class GameManager : MonoBehaviour
    {
        /** ====== EVENTS ====== */
        public event ScoreChanged ScoreUpdated;
        public event GameEnded GameWin;
        public event GameEnded GameLost;
        
        /** === END OF EVENTS === */


        public DiningManager diningManager;
        
        private float _playerScore;
        private int _customersServed;
        private int _mealsServed;
        private int _ordersTaken;
        private int _customersLeft;
        private float _score;

        // Start is called before the first frame update
        void Start()
        {
            ScoreUpdated?.Invoke(_playerScore);

            diningManager.CustomerLeft += OnCustomerLeave;
        }
        
        public void IncrementScore(float amount)
        {
            _playerScore += amount;
            ScoreUpdated?.Invoke(_playerScore);
        }

        void OnCustomerLeave(Customer customer)
        {
            _customersLeft++;
            
            // Check Win/Loss Condition
            if (_customersLeft >= diningManager.customersToArrive)
            {
                if (_playerScore >= 0.5)
                {
                    // Trigger Win
                    GameWin?.Invoke();
                }
                else
                {
                    // Trigger loss
                    GameLost?.Invoke();
                }
            }
        }
    }
}
