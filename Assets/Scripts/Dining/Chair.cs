using System;
using System.Collections;
using UnityEngine;

namespace Dining
{
    public class Chair : MonoBehaviour
    {
        private Animator _animator;
        private GameObject _occupiedBy = null;
        
        private static readonly int Open = Animator.StringToHash("open");

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetOccupation(GameObject customer)
        {
            _occupiedBy = customer;

            customer.transform.position = transform.position;
            customer.transform.rotation = Quaternion.LookRotation(-transform.up);

            (customer.GetComponent<Customer>()).Sit();

            StartCoroutine(AnimateSeat());
        }

        public bool IsAvailable()
        {
            return _occupiedBy == null;
        }

        private IEnumerator AnimateSeat()
        {
            yield return new WaitForSeconds(0.2f);
            _animator.SetBool(Open, true);
            yield return new WaitForSeconds(1f);
            _animator.SetBool(Open, false);
        }
    }
}