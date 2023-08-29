using System;
using Dining;
using TMPro;
using UnityEngine;

namespace UI
{
    public class NameTag : AttachTo
    {
        [NonSerialized]
        private static readonly Vector2 NameOffset = new(0, 120);
        
        private TMP_Text _text;
        private Customer _customer;
    
        private new void Start()
        {
            base.Start();
            screenOffset = NameOffset;
        
            _text = GetComponent<TMP_Text>();
            _customer = parent.GetComponent<Customer>();

            _text.text = _customer.firstName;
        }
    }
}
