using System.Collections.Generic;
using Cooking;
using UnityEngine;

namespace Dining
{
    public class Menu : MonoBehaviour
    {
        public List<CustomerOrder> items;

        public CustomerOrder SelectItem(Customer customer)
        {
            CustomerOrder source = items[Random.Range(0, items.Count)];
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (Ingredient sourceIngredient in source.ingredients)
            {
                ingredients.Add(
                    new Ingredient() { location = sourceIngredient.location, type = sourceIngredient.type, requiresPreparation = sourceIngredient.requiresPreparation}
                );
            }

            return new CustomerOrder()
            {
                customer = customer, ingredients = ingredients, target = source.target,
                cookingTime = source.cookingTime, name = source.name
            };
        }
    }
}