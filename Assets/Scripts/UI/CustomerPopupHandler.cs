using System;
using System.Collections.Generic;
using Dining;
using Game;
using UnityEngine;
using UnityEngine.UI;

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

        private readonly Dictionary<Customer, Image> _customerMoods = new();
        private Popup _popup;

        private void Start()
        {
            diningManager.CustomerSpawned += OnCustomerEnter;
            diningManager.CustomerLeft += OnCustomerLeave;
        }

        void OnCustomerEnter(Customer customer)
        {
            GameObject popupObj = Instantiate(popupTemplate, popupContainer);
            Image popup = popupObj.transform.GetChild(0).GetComponent<Image>();
            AttachTo attachment = popupObj.AddComponent<AttachTo>();
            attachment.parent = customer.transform;
            attachment.screenOffset = CustomerMoodOffset;
                
            popup.sprite = iconSet.GetMoodSprite(customer.GetMood());

            customer.MoodChanged += newMood => OnCustomerMoodChange(customer, newMood);
            
            _customerMoods.Add(customer, popup);
        }

        void OnCustomerLeave(Customer customer)
        {
            if (_customerMoods.TryGetValue(customer, out var moodPopup))
            {
                DestroyImmediate(moodPopup.gameObject);
                _customerMoods.Remove(customer);
            }
        }

        void OnCustomerMoodChange(Customer customer, Customer.Mood newMood)
        {
            if (_customerMoods.TryGetValue(customer, out var moodPopup))
            {
                moodPopup.sprite = iconSet.GetMoodSprite(newMood);
            }
        }
        
    }
} 
