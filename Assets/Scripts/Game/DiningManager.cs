using System.Collections;
using System.Collections.Generic;
using Cooking;
using Dining;
using Interaction;
using Unity.VisualScripting;
using UnityEngine;
using Timer = Cooking.Timer;

namespace Game
{
    public class DiningManager : MonoBehaviour
    {
        private const int CustomerWaitPeriod = 30;

        public Menu menu;
        
        [Header("Dining Subsystem")]
        public GameObject[] customerTemplates = { };
        public int customersToArrive;
    
        /* ===== CUSTOMER AREAS ===== */
        [Header("Customer Common Areas")]
    
        [Tooltip("Queueing area for customers to wait.")]
        public GameObject queuingArea;
    
        [Tooltip("Dining room tables available for seating.")]
        public Table[] tables;
        /* === END CUSTOMER AREAS === */

        private readonly List<Customer> _customers = new();
        
        
        /* ===== OTHER MANAGERS ===== */
        private GameManager _gameManager;
        private KitchenManager _kitchen;
        /* ==== END OF MANAGERS ==== */

        // Start is called before the first frame update
        void Start()
        {
            _gameManager = GetComponent<GameManager>();
            _kitchen = GetComponent<KitchenManager>();

            // TODO: spawn some customers

            // TODO: timers against customer's requests

            // TODO: player collect's customers orders, which get passed to the kitchen
        }

        void CustomerArrives(Customer customer)
        {
            // Add the customer to our list of customers.
            _customers.Add(customer);

            // Configure a patience timer for waiting at the entrance.
            Timer customerPatience = customer.AddComponent<Timer>();
            customerPatience.Setup(CustomerWaitPeriod);

            // Add listener for collection of customer.
            customer.InteractComplete += CustomerCollected;
        }

        void CustomerCollected(Interactable interactable)
        {
            // Remove listener.
            interactable.InteractComplete -= CustomerCollected;

            // Begin thinking about what to order...
            Customer customer = (Customer)interactable;
            StartCoroutine(CustomerBrowse((Customer)interactable, Random.Range(5, 8)));
        }

        IEnumerator CustomerBrowse(Customer customer, int duration)
        {
            customer.timer.Stop();
            
            // Wait for an amount of time to make a decision.
            yield return new WaitForSeconds(duration);
            
            // TODO: select an item off the menu.
            customer.order = menu.SelectItem(customer);
            
            customer.timer.Reset();
            customer.timer.TimerComplete += OrderTaken; 
            customer.timer.Start();
        }

        void OrderTaken()
        {
            _kitchen.AddOrder();
        }

        void SpawnCustomer()
        {
            GameObject newCustomer = Instantiate(customerTemplates[Random.Range(0, customerTemplates.Length)]);
            _customers.Add(newCustomer.GetComponent<Customer>());
        }

        IEnumerator CustomerLeaves(Customer customer)
        {
            // TODO: play sound.
            
            customer.Stand();
            yield return new WaitForSeconds(1f);
            _customers.Remove(customer);
            Destroy(customer);
        }
    }
}
