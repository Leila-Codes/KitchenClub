using Cooking;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Popup : MonoBehaviour
    {
        public IconSet iconSet;
        public Ingredient.Type ingredient = Ingredient.Type.Broccoli;
        public CookingStep.Action action;

        public Image popupIcon;

        public Popup(CookingStep.Action action)
        {
            this.action = action;
        }

        private void Start()
        {
            if (action is CookingStep.Action.Collect or CookingStep.Action.Prepare)
                popupIcon.sprite = iconSet.GetIngredientIcon(ingredient);
            else
                popupIcon.sprite = iconSet.GetActionIcon((CookingStep.Action)action);
        }
    }
}
