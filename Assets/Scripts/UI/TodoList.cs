using System.Collections;
using System.Collections.Generic;
using Cooking;
using Dining;
using Game;
using UI;
using UnityEngine;

public class TodoList : MonoBehaviour
{
    public KitchenManager kitchen;
    public GameObject stepPrefab;
    public Transform stepsContainer;

    private List<CookingStep> _steps;

    // Start is called before the first frame update
    void Start()
    {
        kitchen.OrderStarted += RenderNewOrder;
        kitchen.OrderUpdated += OrderUpdated;
        kitchen.OrderCompleted += OrderComplete;
    }

    void AddNewStep(int index, Ingredient.Type ingredient, CookingStep.Action action, string recipeName)
    {
        // Instantiate the new step object.
        GameObject stepGameObject = Instantiate(stepPrefab, stepsContainer);
        CookingStep stepData = stepGameObject.GetComponent<CookingStep>();
        RectTransform rect = stepGameObject.GetComponent<RectTransform>();
        
        // Position the new item.
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 1f);
        rect.anchoredPosition = new Vector2(0, -(20 + 40 * index));
        
        // Configure it's "delicious" data
        stepData.ingredientType = ingredient;
        stepData.action = action;
        stepData.recipeName = recipeName;
        
        _steps.Add(stepData);
    }

    void RenderNewOrder(CustomerOrder order)
    {
        _steps = new List<CookingStep>();

        int i = 0;

        foreach (var ingredient in order.ingredients)
        {
            
            // Add a collect step for this ingredient
            AddNewStep(i, ingredient.type, CookingStep.Action.Collect, order.name);
            
            // Increment i
            i++;
            
            // If the step requires preparation...
            if (ingredient.requiresPreparation)
            {
                // Add a second "preparation" step
                AddNewStep(i, ingredient.type, CookingStep.Action.Prepare, order.name);

                // Increment I again as we added an extra step.
                i++;
            }
        }
        
        // Add a cook step
        AddNewStep(i++, Ingredient.Type.Apple, CookingStep.Action.Cook, order.name);
        AddNewStep(i++, Ingredient.Type.Apple, CookingStep.Action.Serve, order.name);
    }

    void OrderUpdated(CustomerOrder order)
    {
        int i = 0;

        foreach (var ingredient in order.ingredients)
        {
            if ((!ingredient.requiresPreparation || ingredient.IsCollected ) && !_steps[i].completed)
            {
                _steps[i].completed = true;
                break;
            }

            if (ingredient.requiresPreparation && ingredient.IsPrepared && !_steps[i].completed)
            {
                _steps[++i].completed = true;
                break;
            }
        }

        if (order.IsCooking)
        {
            _steps[^2].completed = true;
        }
    }

    void OrderComplete(CustomerOrder order)
    {
        _steps[^1].completed = true;
        StartCoroutine(ClearTodoList());
    }

    IEnumerator ClearTodoList()
    {
        yield return new WaitForSeconds(2);
        foreach (var step in _steps)
        {
            DestroyImmediate(step.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}