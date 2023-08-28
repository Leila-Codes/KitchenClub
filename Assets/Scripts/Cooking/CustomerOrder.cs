using System;
using System.Collections.Generic;
using Interaction;
using UnityEngine;

namespace Cooking
{

    [Serializable]
    public class CustomerOrder
    {
        [Header("Target to cook this meal on")]
        public Interactable target;
        
        [Header("List of ingredients")]
        public List<Ingredient> ingredients;
        
        // [Header("Destination Table")]
        // public Dining.Table destination;
        public string name;

        [Header("Owner of this order (Customer)")]
        [HideInInspector]
        public Dining.Customer customer;
        
        public int cookingTime;
        
        [NonSerialized] private bool _complete; 
        [NonSerialized] public bool IsCooking; 

        public void SetCompleted(bool complete)
        {
            _complete = complete;
        }

        public bool IsComplete()
        {
            return _complete;
        }
    }
}
