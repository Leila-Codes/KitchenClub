using UnityEngine;

namespace Dining
{
    public class Customer : MonoBehaviour
    {
        public enum State
        {
            Idle,
            Seated,
            // Browsing,
            Waiting,
            Angry
        }

        /** === PRIVATE MEMBER VARS === */
        private Animator _animator;

        private State _state = State.Idle;
        private bool _seated;
        /** === END PRIVATE MEMBERS === **/
        
        
        /** ===== ANIMATOR CONSTANTS ===== **/
        private static readonly int Seated = Animator.StringToHash("seated");
        private static readonly int Browsing = Animator.StringToHash("browsing");
        private static readonly int Waiting = Animator.StringToHash("waiting");
        private static readonly int Impatient = Animator.StringToHash("impatient");
        /** === END ANIMATOR CONSTANTS === **/
        
        void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetState(State newState)
        {
            if (!_seated) return; // cannot change state whilst the player isn't sitting down
            
            _state = newState;
            _animator.SetBool(Browsing, false);
            _animator.SetBool(Waiting, false);
            _animator.SetBool(Impatient, false);

            switch (_state)
            {
                // case State.Browsing:
                    // _animator.SetBool(Browsing, true);
                    // break;
                case State.Waiting:
                    _animator.SetBool(Waiting, true);
                    break;
                case State.Angry:
                    _animator.SetBool(Impatient, true);
                    break;
            }
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