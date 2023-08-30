using System;
using Cooking;
using Game;
using Interaction;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ObjectivePopupHandler : MonoBehaviour
    {
        [NonSerialized] private static readonly Vector2 ObjectivePopupOffset = new(5, 50);

        public GameObject popupTemplate;
        public Transform popupContainer;
        public KitchenManager kitchenManager;
        public IconSet iconSet;

        private GameObject _popupObj;
        private Image _popup;
        private AttachTo _popupAttachment;

        private void Start()
        {
            kitchenManager.OrderCompleted += _ => HidePopup();
            kitchenManager.CurrentStepUpdated += RefreshCookingPopup;

            
            _popupObj = Instantiate(popupTemplate, popupContainer);
            
            _popup = _popupObj.transform.GetChild(0).GetComponent<Image>();
            
            _popupAttachment = _popupObj.AddComponent<AttachTo>();
            _popupAttachment.screenOffset = ObjectivePopupOffset;
            
            HidePopup();
        }

        void HidePopup()
        {
            _popupObj.SetActive(false);
        }

        void ShowPopup()
        {
            _popupObj.SetActive(true);
        }

        void RefreshCookingPopup(
            Ingredient.Type ingredient,
            CookingStep.Action action,
            [CanBeNull] Interactable target
        )
        {
            // Hide the popup
            HidePopup();

            // Determine the icon to display
            switch (action)
            {
                case CookingStep.Action.Collect:
                    _popup.sprite = iconSet.GetIngredientIcon(ingredient);
                    break;
                default:
                    _popup.sprite = iconSet.GetActionIcon(action);
                    break;
            }

            // Reposition the popup (if necessary)
            if (target)
            {
                _popupAttachment.parent = target.transform;
                _popupAttachment.enabled = true;
            }
            else
            {
                _popupAttachment.enabled = false;
            }
            
            // Show the popup again
            ShowPopup();
        }
    }
}