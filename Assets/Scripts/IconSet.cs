using System;
using System.Collections.Generic;
using Cooking;
using UI;
using UnityEngine;

[Serializable]
public class IconSet : MonoBehaviour
{
    private Dictionary<string, Sprite> _foodSprites = new();
    private Dictionary<string, Sprite> _actionSprites = new();
    
    private void Awake()
    {
        Sprite[] foodSheet = Resources.LoadAll<Sprite>("UI/Icons/food_collection");
        Sprite[] actionSheet = Resources.LoadAll<Sprite>("UI/Icons/actions");
        
        foreach (Sprite sprite in foodSheet)
        {
            _foodSprites.Add(sprite.name, sprite);
        }

        foreach (Sprite sprite in actionSheet)
        {
            _actionSprites.Add(sprite.name, sprite);
        }
    }

    public Sprite GetIngredientIcon(Ingredient.Type ingredientType)
    {
        return _foodSprites[ingredientType.ToString().ToLower()];
    }

    public Sprite GetActionIcon(CookingStep.Action actionType)
    {
        return _actionSprites[actionType.ToString().ToLower()];
    }
    
    
}