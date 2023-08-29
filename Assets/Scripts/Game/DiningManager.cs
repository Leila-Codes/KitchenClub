using System.Collections;
using System.Collections.Generic;
using Cooking;
using Dining;
using Interaction;
using UnityEngine;

namespace Game
{
    public delegate void CustomerEvent(Customer customer);

    public class DiningManager : MonoBehaviour
    {
        private const int CustomerWaitPeriod = 10;
        private const int CustomerFoodWaitPeriod = 120; // 2 Minutes

        public Menu menu;
        
        [Header("Dining Subsystem")]
        public GameObject[] customerTemplates = { };
        public int customersToArrive;
    
        /* ===== CUSTOMER AREAS ===== */
        [Header("Customer Common Areas")]
    
        [Tooltip("Queueing area for customers to wait.")]
        public CustomerQueue queuingArea;
    
        [Tooltip("Dining room tables available for seating.")]
        public Table[] tables;
        /* === END CUSTOMER AREAS === */

        /** ==== CUSTOMER EVENTS ===== */
        public event CustomerEvent CustomerSpawned;
        public event CustomerEvent CustomerLeft;
        
        /* === END CUSTOMER EVENTS === */
        

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

            int waitTime = 5;
            for (int i = 0; i < customersToArrive; i++)
            {
                Debug.Log("Customer " + i + " will arrive in " + waitTime + " seconds.");
                
                // Spawn customer (after waitTime)
                StartCoroutine(SpawnCustomer(waitTime));
                
                waitTime += Random.Range(30, 120);
            }
        }

        // When a customer arrives at the door...
        void CustomerArrives(Customer customer)
        {
            Debug.Log("A customer by the name '" + customer.firstName + "' has just arrived.");

            // Add the customer to our list of customers.
            _customers.Add(customer);

            // Configure listener for when the customer is successfully greeted by the player.
            customer.CustomerLeft += OnCustomerLeft;
            
            // Configure listener for when the customer has been shown to the their table.
            customer.InteractComplete += CustomerCollected;
            
            // Configure a patience timer for waiting at the entrance.
            customer.impatience.SetEndTime(CustomerWaitPeriod);
            customer.impatience.StartTimer();
            
            // Trigger event for customer spawn.
            CustomerSpawned?.Invoke(customer);
        }

        // When the player successfully greets the customer at the door.
        void CustomerCollected(Interactable interactable)
        {
            Customer customer = (Customer)interactable;
            
            // Remove listener.
            customer.InteractComplete -= CustomerCollected;
            
            Debug.Log("Customer " + customer.firstName + " has been shown to their seat.");
            
            // Stop the impatience timer
            customer.impatience.StopTimer();
            customer.impatience.RestartTimer();
            
            // Prepare listener for when customer has selected their preferred menu choice.
            customer.CustomerOrderSelected += CustomerOrderReady;
            // Begin thinking about what to order...
            StartCoroutine(customer.SelectMenuItem(Random.Range(5, 8)));
            
            // Position the player on their table and "Sit" down.
            Chair selectedSeat = null;
            foreach (Table table in tables)
            {
                selectedSeat = table.NextAvailableSeat();
                if (selectedSeat != null) break;
                
                
                break;
            }

            if (selectedSeat == null)
            {
                Debug.LogError("There are no more available seats in the restaurant!");
                return;
            } 
            
            selectedSeat.SetOccupation(customer.gameObject);
            customer.Sit();
        }

        // When a customer has selected their order.
        void CustomerOrderReady(CustomerOrder order)
        {
            Debug.Log("Customer " + order.customer.firstName + " is ready to place their order.");
            
            // Configure listener for order taken by the player.
            order.customer.InteractComplete += CustomerOrderTaken;
            
            // Begin new impatience timer
            order.customer.impatience.SetEndTime(CustomerWaitPeriod);
            order.customer.impatience.StartTimer();
        }

        // When the customer's order is taken by the player.
        void CustomerOrderTaken(Interactable interactable)
        {
            Customer customer = (Customer)interactable;

            // Remove listener
            customer.InteractComplete -= CustomerOrderTaken;
            
            Debug.Log("Customer " + customer.firstName + "'s order of " + customer.order.name + " has been sent to the Kitchen.");

            // Reset the customer's impatience
            customer.impatience.SetEndTime(CustomerFoodWaitPeriod); // Allow two minutes for the order to be fulfilled.
            customer.impatience.RestartTimer();

            // Add the order to the kitchen for cooking.
            _kitchen.AddOrder(((Customer)interactable).order);
            
            // Add handler for order complete.
            // TODO: Make this handler global/one time.
            _kitchen.OrderCompleted += CustomerOrderFulfilled;
        }
        
        // When the order has finished being cooked.
        void CustomerOrderFulfilled(CustomerOrder order)
        {
            // Remove listener.
            // TODO: Make this handler global/one time.
            _kitchen.OrderCompleted -= CustomerOrderFulfilled;
            
            Debug.Log("Order up! - " + order.name + " for " + order.customer.firstName + " is ready!");

            order.customer.InteractComplete += CustomerOrderDelivered;
        }

        void CustomerOrderDelivered(Interactable interactable)
        {
            Customer customer = (Customer)interactable;
            
            Debug.Log("Congrats! You just served a delicious " + customer.order.name + " to the customer.");
            
            // TODO: spawn plate/food in front of player at the table.
            
            // Stop the timers!!
            customer.impatience.StopTimer();
            
            // Reset customer's mood
            customer.SetMood(Customer.Mood.Happy);
        }

        IEnumerator SpawnCustomer(int spawnDelaySecs)
        {
            yield return new WaitForSeconds(spawnDelaySecs);
            
            GameObject newCustomerObj = Instantiate(customerTemplates[Random.Range(0, customerTemplates.Length)]);
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            Customer newCustomer = newCustomerObj.GetComponent<Customer>();
            _customers.Add(newCustomer);
            
            // Position the customer in the world (the waiting area)
            queuingArea.AddCustomer(newCustomerObj);
            
            // Notify customer manager that the customer has just arrived.
            CustomerArrives(newCustomer);
        }

        void OnCustomerLeft(Customer customer)
        {
            // TODO: play sound.
            
            Debug.Log("Customer " + customer.firstName + " is now so fed up, they have left the restaurant.");
            
            // Animate the customer leaving the restaurant
            StartCoroutine(AnimateCustomerLeave(customer));
        }
        
        IEnumerator AnimateCustomerLeave(Customer customer)
        {
            customer.Stand();
            yield return new WaitForSeconds(1f);
            
            // Trigger customer leave event for side-effects
            CustomerLeft?.Invoke(customer);
            
            // Remove the customer from play.
            Destroy(customer.gameObject);
            _customers.Remove(customer);
        }
    }
}
