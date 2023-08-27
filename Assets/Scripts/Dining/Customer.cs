using Cooking;
using Interaction;
using UnityEngine;

namespace Dining
{

    public delegate void MoodChanged(Customer.Mood currentMood);
    
    public class Customer : Interactable
    {
        // ReSharper disable once StringLiteralTypo
        private static readonly string[] Names = {"Jeannette", "Marly", "Blossom", "Sabrina", "Rosalee", "Isaia", "Olga", "Nora", "Aurel", "Diana"};
        
        public enum Mood
        {
            Happy,
            Indifferent,
            Annoyed,
            Sad
        }

        [HideInInspector]
        public Timer timer;

        [HideInInspector]
        public CustomerOrder order;

        public event MoodChanged MoodChanged;
        
        /** === PRIVATE MEMBER VARS === */
        private Animator _animator;

        private string _name;

        private Mood _mood = Mood.Indifferent;

        private bool _seated;
        /** === END PRIVATE MEMBERS === **/
        
        
        /** ===== ANIMATOR CONSTANTS ===== **/
        private static readonly int Seated = Animator.StringToHash("seated");
        /** === END ANIMATOR CONSTANTS === **/
        
        void Start()
        {
            _animator = GetComponent<Animator>();

            _name = Names[Random.Range(0, Names.Length)];
        }

        public Mood GetMood()
        {
            return _mood;
        }

        public void SetMood(Mood newMood)
        {
            _mood = newMood;
            MoodChanged?.Invoke(newMood);
        }
        
        public void Stand()
        {
            _seated = false;
            _animator.SetBool(Seated, _seated);
        }

        public void Sit()
        {
            _seated = true;
            _animator.SetBool(Seated, _seated);
        }
    }
}