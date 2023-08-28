using System;
using System.Collections;
using Cooking;
using Interaction;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Timer = Cooking.Timer;

namespace Dining
{
    public delegate void MoodChanged(Customer.Mood currentMood);

    public delegate void CustomerLeft(Customer customer);

    public delegate void CustomerOrderSelect(CustomerOrder order);

    public class Customer : Interactable
    {
        // ReSharper disable once StringLiteralTypo
        private static readonly string[] Names =
            { "Jeannette", "Marly", "Blossom", "Sabrina", "Rosalee", "Isaia", "Olga", "Nora", "Aurel", "Diana" };

        public enum Mood
        {
            Happy,
            Indifferent,
            Sad,
            Annoyed
        }

        [HideInInspector] public Timer impatience;

        [HideInInspector] public CustomerOrder order;

        /** ======== EVENTS ======== */
        public event MoodChanged MoodChanged;
        public event CustomerLeft CustomerLeft;

        public event CustomerOrderSelect CustomerOrderSelected;
        /** ===== END OF EVENTS ===== */
           

        /** === PRIVATE MEMBER VARS === */
        private Animator _animator;

        public string firstName;

        private Mood _mood = Mood.Happy;

        private Menu _menu;

        private bool _seated;

        /** === END PRIVATE MEMBERS === **/
        /** ===== ANIMATOR CONSTANTS ===== **/
        private static readonly int Seated = Animator.StringToHash("seated");

        /** === END ANIMATOR CONSTANTS === **/
        private void Awake()
        {
            _menu = FindObjectOfType<Menu>();
            
            _animator = GetComponent<Animator>();
            
            impatience = GetComponent<Timer>();
            if (impatience == null)
                impatience = transform.AddComponent<Timer>();
          
            firstName  = Names[Random.Range(0, Names.Length)];
        }

        private void OnDestroy()
        {
            // Stop the impatience timer/couroutine if already running.
            impatience.StopTimer();
        }

        void Start()
        {
            impatience.TimerComplete += ImpatienceHandler;
        }

        public Mood GetMood()
        {
            return _mood;
        }

        public void SetMood(Mood newMood)
        {
            _mood = newMood;
            
            Debug.Log("Customer " + firstName + " is now " + _mood);
            
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

        public IEnumerator SelectMenuItem(float thinkingTimeSecs)
        {
            yield return new WaitForSeconds(thinkingTimeSecs);
            order = _menu.SelectItem(this);
            
            CustomerOrderSelected?.Invoke(order);
        }

        private void ImpatienceHandler()
        {
            // Check to see if the customer is already at maximum impatience 
            if (_mood < Mood.Annoyed)
            {
                // Not yet - increment impatience/mood.
                SetMood(_mood + 1);
                impatience.RestartTimer();
            }
            else
            {
                // Yes - Leave the restaurant.
                CustomerLeft?.Invoke(this);
            }
        }
    }
}