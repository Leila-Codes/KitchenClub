using System;
using Cooking;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CookingStep : MonoBehaviour
    {
        public enum Action
        {
            Collect,
            Prepare,
            Place,
            Cook,
            Serve
        }

        public string recipeName;
        public Action action;
        public Ingredient.Type ingredientType;
        public bool completed;

        public Image icon;
        public TMP_Text textOut;
        public GameObject completedIcon;

        private IconSet _iconSet;

        private void Awake()
        {
            _iconSet = FindObjectOfType<IconSet>();
        }
        
        private void Update()
        {
            // Render step text
            if (action is Action.Collect or Action.Prepare)
            {
                textOut.text = action + " the " + ingredientType;
            }
            else
            {
                textOut.text = action + " " + recipeName;
            }

            // Render appropriate action icon.
            switch (action)
            {
                case Action.Collect:
                    icon.sprite = _iconSet.GetIngredientIcon(ingredientType);
                    break;
                case Action.Place:
                    icon.sprite = _iconSet.GetIngredientIcon(Ingredient.Type.Banana);
                    break;
                default:
                    icon.sprite = _iconSet.GetActionIcon(action);
                    break;
            }
            
            // Render whether complete or not.
            completedIcon.gameObject.SetActive(completed);
        }
    }
}