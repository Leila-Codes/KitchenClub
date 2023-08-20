using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Interaction.Customers
{
    public class CollectCustomer : Interactable
    {
        public CustomerManager customerManager;
        private CustomerQueue _customerQueue;
        
        private void Start()
        {
            _customerQueue = GetComponent<CustomerQueue>();
            
            InteractComplete += OnInteractComplete;
        }

        private void OnInteractComplete()
        {
            GameObject customer = _customerQueue.DequeueCustomer();
            if (customer == null) return;
            
            // TODO: Find a table for this customer.
            Dining.Chair seat = customerManager.nextAvailableSeat();
            if (seat == null)
            {
                Debug.LogError("WTF!? There's NO CHAIRS LEFT!!!");
                return;
            }

            seat.SetOccupation(customer);
        }
    }
}
