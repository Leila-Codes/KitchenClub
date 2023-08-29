using System;
using Cooking;
using Game;
using Interaction;
using JetBrains.Annotations;
using UnityEngine;

namespace UI
{
    public class ObjectivePopupHandler : MonoBehaviour
    {
        [NonSerialized]
        private static readonly Vector2 ObjectivePopupOffset = new(5, 50);
        
        public GameObject popupTemplate;
        public Transform popupContainer;
        public KitchenManager kitchenManager;
        public IconSet iconSet;
    
        private Popup _popup;

        private void Start()
        {
            kitchenManager.OrderCompleted += _ => ClearPopup();
            kitchenManager.CurrentStepUpdated += PlaceCookingPopup;
        }

        void ClearPopup()
        {
            if (_popup != null)
            {
                DestroyImmediate(_popup.gameObject);
                _popup = null;
            }
        }

        void PlaceCookingPopup(Ingredient.Type ingredient, CookingStep.Action action, [CanBeNull] Interactable target)
        {
            ClearPopup();

            GameObject popupObj = Instantiate(popupTemplate, popupContainer);
            _popup = popupObj.GetComponent<Popup>();
            
            // Determine which icon to show
            Sprite icon;
            if (action is CookingStep.Action.Collect)
                icon = iconSet.GetIngredientIcon(ingredient);
            else
                icon = iconSet.GetActionIcon((CookingStep.Action)action);

            _popup.icon = icon;

            if (target != null)
            {
                AttachTo attachTo = popupObj.AddComponent<AttachTo>();
                attachTo.screenOffset = ObjectivePopupOffset;
                attachTo.parent = target.transform;
            }
        }
    }
} 
