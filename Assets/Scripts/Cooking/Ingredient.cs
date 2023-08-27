using System;
using Interaction;
using UnityEngine;

namespace Cooking
{
    [Serializable]
    public class Ingredient
    {
        public enum Type
        {
            Banana,
            Broccoli,
            Carrot,
            Chilli,
            Fish,
            Poultry,
            Meat,
            Mushroom,
            Onion,
            Tomato
        }

        public Type type;
        public bool requiresPreparation = true;
        public Interactable location;
        
        [NonSerialized]
        public bool isPrepared;
    }
}
