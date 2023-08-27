using UnityEngine;

namespace Game
{
    public delegate void ScoreChanged(float newScore);
    
    public class GameManager : MonoBehaviour
    {

        private float _playerScore;

        public event ScoreChanged ScoreUpdated;

        private int _customersServed;
        private int _mealsServed;
        private int _ordersTaken;
        private float _score;

        [Header("Cooking Subsystem")]
        public GameObject fridge;

        private Hintable _fridgeHint;
        public GameObject cupboard;
        private Hintable _cupboardHint;
        public GameObject oven;
        private Hintable _ovenHint;
        public GameObject hob;
        private Hintable _hobHint;
    
    
        // Start is called before the first frame update
        void Start()
        {
            _fridgeHint = fridge.GetComponentInChildren<Hintable>();
            _cupboardHint = cupboard.GetComponentInChildren<Hintable>();
            _ovenHint = oven.GetComponentInChildren<Hintable>();
            _hobHint = hob.GetComponentInChildren<Hintable>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void IncrementScore(float amount)
        {
            _playerScore += amount;
            ScoreUpdated?.Invoke(_playerScore);
        }
    }
}
