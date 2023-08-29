using System;
using System.Collections.Generic;
using Cooking;
using Dining;
using UI;
using UnityEngine;

[Serializable]
public class IconSet : MonoBehaviour
{
    private Dictionary<string, Sprite> _foodSprites = new();
    private Dictionary<string, Sprite> _actionSprites = new();
    private Dictionary<string, Sprite> _moodSprites = new();
    
    private void Awake()
    {
        Sprite[] foodSheet = Resources.LoadAll<Sprite>("UI/Icons/food_collection");
        Sprite[] actionSheet = Resources.LoadAll<Sprite>("UI/Icons/actions");
        Sprite[] moodSheet = Resources.LoadAll<Sprite>("UI/Icons/moods");
        
        foreach (Sprite sprite in foodSheet)
        {
            _foodSprites.Add(sprite.name, sprite);
        }

        foreach (Sprite sprite in actionSheet)
        {
            _actionSprites.Add(sprite.name, sprite);
        }
        
        foreach (Sprite sprite in moodSheet)
        {
            _moodSprites.Add(sprite.name, sprite);
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

    public Sprite GetCustomerActionIcon(Customer.Action action)
    {
        return _moodSprites[action.ToString().ToLower()];
    }
    
    public Sprite GetMoodSprite(Customer.Mood mood)
    {
        return _moodSprites[mood.ToString().ToLower()];
    }
    
}