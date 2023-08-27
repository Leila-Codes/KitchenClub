using System;
using System.Collections.Generic;
using Cooking;
using Dining;
using Interaction;
using Unity.VisualScripting;
using UnityEngine;
using Timer = Cooking.Timer;

namespace Game
{
    public class KitchenManager : MonoBehaviour
    {
        public PlayerCharacter player;
        public Interactable choppingBoard;

        private Queue<CustomerOrder> _orders = new();
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
            _activeOrder = _orders.Dequeue();

            _ingredientsToCollect = new Queue<Ingredient>(_activeOrder.ingredients);

            NextItem();
        }

        public void NextItem()
        {
            _currentIngredient = _ingredientsToCollect.Peek();

            _currentIngredient.location.InteractComplete += ItemCollected;
            _currentIngredient.location.ShowHint();
        }

        private void ItemCollected(Interactable trigger)
        {
            // Make sure the player isn't already carrying something.
            if (player.carrying != null)
            {
                // Remove the collection listener.
                _currentIngredient.location.InteractComplete -= ItemCollected;
                _currentIngredient.location.HideHint();

                
                // Assign the ingredient to the player's "inventory"
                player.carrying = _currentIngredient;
                
                Debug.Log("Player successfully collected" + _currentIngredient.type);

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
            }
        }

        // Player has successfully "prepped" the currently held ingredient.
        private void ItemPrepared(Interactable trigger)
        {
            // Make sure player is carrying something
            if (player.carrying == null) return;
            
            // Update the ingredient's prepared state.
            player.carrying!.isPrepared = true;
            
            Debug.Log("Player successfully prepared" + player.carrying.type);

            // Remove the listener
            choppingBoard.InteractComplete -= ItemPrepared;
            choppingBoard.HideHint();


            // Setup handler for when player "places" the item on the correct cookware location.
            _activeOrder.target.InteractComplete += ItemPlaced;
        }

        // Player has successfully "placed" the currently held ingredient.
        private void ItemPlaced(Interactable trigger)
        {
            // Make sure the player is carrying something
            if (player.carrying == null) return;
            
            // Make sure the item held, has been prepared (if required)
            if (player.carrying.requiresPreparation && !player.carrying.isPrepared) return;

            // Remove listener
            _activeOrder.target.InteractComplete -= ItemPlaced;
            _activeOrder.target.HideHint();

            Debug.Log("Player successfully placed" + player.carrying.type);
            
            // Remove the carry reference, enabling the player to pick up something else.
            player.carrying = null;

            if (_ingredientsToCollect.Count == 0)
            {
                // If present - Start cooking.
                _cookingTimer = _activeOrder.target.transform.AddComponent<Timer>();
                _cookingTimer.Setup(_activeOrder.cookingTime);
                _cookingTimer.Start();
                
                _cookingTimer.TimerComplete += CookingComplete;
            } else {
                // Else - Move to next ingredient in queue.
                NextItem();
            }
        }

        // The item in the cookware has successfully cooked.
        private void CookingComplete()
        {
            _activeOrder.target.InteractComplete += MealCollected;
            _activeOrder.target.ShowHint();
        }

        // Player has successfully "collected" the meal from the cookware.
        private void MealCollected(Interactable trigger)
        {
            // Stop the timer!!
            int timeElapsed = _cookingTimer.Stop();
            
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
                    quality = 1 - (deltaSeconds / 5);
                }
            }
            
            // Add to player's score for today.
            _gameManager.IncrementScore(quality);
            
            // TODO: reset the customer's satisfaction.
            _activeOrder.customer.SetMood(Customer.Mood.Happy);

            // TODO: have player serve up the food.
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