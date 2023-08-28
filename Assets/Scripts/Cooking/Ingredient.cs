using System;
using Interaction;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cooking
{
    [Serializable]
    public class Ingredient
    {
        public enum Type
        {
            Banana,
            Apple,
            Grape1,
            Grape2,
            Pineapple,
            Kiwi,
            Cherry,
            Strawberry,
            Carrot,
            Tomato,
            Aubergine,
            Potato,
            Broccoli,
            Onion,
            Chilli,
            Mushroom,
            Meat,
            Poultry,
            Fish,
            Lobster
        }

        public Type type;
        public bool requiresPreparation = true;
        public Interactable location;

        [NonSerialized] public bool IsCollected;
        [NonSerialized] public bool IsPrepared;
    }
}
