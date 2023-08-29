using System;
using System.Collections.Generic;
using Dining;
using Game;
using UnityEngine;

namespace UI
{
    public class CustomerPopupHandler : MonoBehaviour
    {
        [NonSerialized]
        private static readonly Vector2 CustomerMoodOffset = new(-40, 140);
        
        public GameObject popupTemplate;
        public Transform popupContainer;
        public DiningManager diningManager;
        public IconSet iconSet;

        private Dictionary<Customer, Popup> _customerMoods = new();
        private Popup _popup;

        private void Start()
        {
            diningManager.CustomerSpawned += OnCustomerEnter;
            diningManager.CustomerLeft += OnCustomerLeave;
        }

        // void ClearPopups()
        // {
            // if (_popup != null)
            // {
                // DestroyImmediate(_popup.gameObject);
                // _popup = null;
            // }
        // }

        void OnCustomerEnter(Customer customer)
        {
            GameObject popupObj = Instantiate(popupTemplate, popupContainer);
            Popup popup = popupObj.GetComponent<Popup>();
            AttachTo attachment = popupObj.AddComponent<AttachTo>();
            attachment.parent = customer.transform;
            attachment.screenOffset = CustomerMoodOffset;

            popup.icon = iconSet.GetMoodSprite(customer.GetMood());

            customer.MoodChanged += newMood => OnCustomerMoodChange(customer, newMood);
            
            _customerMoods.Add(customer, popup);
        }

        void OnCustomerLeave(Customer customer)
        {
            if (_customerMoods.TryGetValue(customer, out var moodPopup))
            {
                DestroyImmediate(moodPopup.gameObject);
            }
        }

        void OnCustomerMoodChange(Customer customer, Customer.Mood newMood)
        {
            if (_customerMoods.TryGetValue(customer, out var moodPopup))
            {
                moodPopup.iconOut.sprite = iconSet.GetMoodSprite(newMood);
            }
        }
        
    }
} 
