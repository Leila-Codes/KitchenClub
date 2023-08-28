using System.Collections.Generic;
using Cooking;
using Interaction;
using UI;
using UnityEngine;

namespace Game
{
    public delegate void CustomerOrderEvent(CustomerOrder order);
    
    public class KitchenManager : MonoBehaviour
    {
        public PlayerCharacter player;
        public Interactable choppingBoard;
        public event CustomerOrderEvent OrderStarted;
        public event CustomerOrderEvent OrderUpdated;
        public event CustomerOrderEvent OrderCompleted;
        

        private readonly Queue<CustomerOrder> _orders = new();
        private CustomerOrder _activeOrder;

        private Queue<Ingredient> _ingredientsToCollect;
        private Ingredient _currentIngredient;
        private Timer _cookingTimer;

        private GameManager _gameManager;

        // TODO: UI Tooltip Magic

        private void Start()
        {
            _gameManager = GetComponent<GameManager>();
        }

        public void NextOrder()
        {
            if (_orders.Count == 0) return;

            _activeOrder = _orders.Dequeue();
            _ingredientsToCollect = new Queue<Ingredient>(_activeOrder.ingredients);

            Debug.Log("Next Order Please! - The kitchen is now preparing " + _activeOrder.name + " for " +
                      _activeOrder.customer.firstName);
            
            OrderStarted?.Invoke(_activeOrder);

            NextItem();
        }

        public void NextItem()
        {
            _currentIngredient = _ingredientsToCollect.Dequeue();

            Debug.Log("The player needs to collect " + _currentIngredient.type + " from " +
                      _currentIngredient.location.name);

            _currentIngredient.location.InteractComplete += ItemCollected;
            _currentIngredient.location.ShowHint();
        }

        private void ItemCollected(Interactable trigger)
        {
            // Make sure the player isn't already carrying something.
            if (player.carrying == null)
            {
                _currentIngredient.IsCollected = true;
                    
                Debug.Log("Yes! You have successfully collected " + _currentIngredient.type + " from " +
                          _currentIngredient.location.name);
                if (_currentIngredient.requiresPreparation)
                    Debug.Log("This item requires some preparation before it can be added to the pot!");

                // Remove the collection listener.
                _currentIngredient.location.InteractComplete -= ItemCollected;
                _currentIngredient.location.HideHint();


                // Assign the ingredient to the player's "inventory"
                player.carrying = _currentIngredient;

                // Debug.Log("Player successfully collected" + _currentIngredient.type);

                // If this ingredient requires preparation...
                if (player.carrying!.requiresPreparation)
                {
                    // Setup listener for chopping board completion.
                    choppingBoard.InteractComplete += ItemPrepared;

                    choppingBoard.ShowHint();
                }
                else
                {
                    // Otherwise, setup listener for placing in the "cooking" location.
                    _activeOrder.target.InteractComplete += ItemPlaced;

                    _activeOrder.target.ShowHint();
                }
                
                OrderUpdated?.Invoke(_activeOrder);
            }
        }

        // Player has successfully "prepped" the currently held ingredient.
        private void ItemPrepared(Interactable trigger)
        {
            // Make sure player is carrying something
            if (player.carrying == null) return;

            // Update the ingredient's prepared state.
            player.carrying!.IsPrepared = true;

            Debug.Log("OK! Ingredient " + _currentIngredient.type + " is now ready for the " +
                      _activeOrder.target.name);

            // Remove the listener
            choppingBoard.InteractComplete -= ItemPrepared;

            // Move the hint to the new target/objective.
            choppingBoard.HideHint();
            _activeOrder.target.ShowHint();

            // Setup handler for when player "places" the item on the correct cookware location.
            _activeOrder.target.InteractComplete += ItemPlaced;

            OrderUpdated?.Invoke(_activeOrder);
        }

        // Player has successfully "placed" the currently held ingredient.
        private void ItemPlaced(Interactable trigger)
        {
            // Make sure the player is carrying something
            if (player.carrying == null) return;

            // Make sure the item held, has been prepared (if required)
            if (player.carrying.requiresPreparation && !player.carrying.IsPrepared) return;

            // Remove listener
            _activeOrder.target.InteractComplete -= ItemPlaced;
            _activeOrder.target.HideHint();

            Debug.Log("Excellent! The ingredient " + _currentIngredient.type + " has now been added.");

            // Remove the carrying reference, enabling the player to pick up something else.
            player.carrying = null;

            if (_ingredientsToCollect.Count == 0)
            {
                // If present - Start cooking.
                _activeOrder.IsCooking = true;
                
                _cookingTimer = ((CookingAppliance)_activeOrder.target).Timer();
                _cookingTimer.SetEndTime(_activeOrder.cookingTime);
                _cookingTimer.StartTimer();

                Debug.Log("All ingredients are now ready in " + _activeOrder.target.name + "! Let the cook commence!");

                _cookingTimer.TimerComplete += CookingComplete;
            }
            else
            {
                // Else - Move to next ingredient in queue.
                NextItem();
            }
            
            OrderUpdated?.Invoke(_activeOrder);
        }

        // The item in the cookware has successfully cooked.
        private void CookingComplete()
        {
            Debug.Log("Cooking is now finished in " + _activeOrder.target.name + " - Collect it before it burns!");
            _activeOrder.target.InteractComplete += MealCollected;
            _activeOrder.target.ShowHint();

            OrderUpdated?.Invoke(_activeOrder);
            
            // TODO: Handle overcooked/burnt food.
        }

        // Player has successfully "collected" the meal from the cookware.
        private void MealCollected(Interactable trigger)
        {
            // Stop the timer!!
            int timeElapsed = _cookingTimer.StopTimer();

            // Remove listener
            _activeOrder.target.InteractComplete -= MealCollected;
            _activeOrder.target.HideHint();

            // Determine food quality.
            float quality = 1,
                deltaSeconds = timeElapsed - _activeOrder.cookingTime;

            // Did the timer exceed the acceptable amount? (3 seconds)
            if (deltaSeconds > 3)
            {
                // Determine quality loss.
                // If greater than 8 seconds, it's destroyed!!
                if (deltaSeconds > 8)
                {
                    quality = 0.1f;
                }
                else
                {
                    float qualityDeficit = deltaSeconds / 8;
                    quality = 1 - qualityDeficit;
                }
            }

            Debug.Log("Meal " + _activeOrder.name + " collected from " + _activeOrder.target.name +
                      " - Quality score is: " + quality);

            // Add to player's score for today.
            _gameManager.IncrementScore(quality);

            // Reset customer's satisfaction.
            // _activeOrder.customer.SetMood(Customer.Mood.Happy);

            // Notify customer manager that order is complete.
            OrderCompleted?.Invoke(_activeOrder);

            // Move to the next order.
            NextOrder();
        }


        // Player has successfully "cooked" an item.
        public void AddOrder(CustomerOrder order)
        {
            _orders.Enqueue(order);

            if (_activeOrder == null)
            {
                NextOrder();
            }
        }
    }
}