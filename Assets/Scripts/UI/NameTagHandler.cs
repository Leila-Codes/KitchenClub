using System.Collections;
using System.Collections.Generic;
using Dining;
using Game;
using UI;
using UnityEngine;

public class NameTagHandler : MonoBehaviour
{
    public DiningManager diningManager;
    public GameObject nameTagPrefab;
    public Transform parent;
    
    private Dictionary<Customer, NameTag> _nameTags = new();
    
    // Start is called before the first frame update
    void Start()
    {
        diningManager.CustomerSpawned += OnCustomerEnter;
        diningManager.CustomerLeft += OnCustomerLeave;
    }

    void OnCustomerEnter(Customer customer)
    {
        GameObject nameTagObj = Instantiate(nameTagPrefab, parent);
        NameTag nameTag = nameTagObj.GetComponent<NameTag>();

        nameTag.parent = customer.transform;

        _nameTags.Add(customer, nameTag);
    }

    void OnCustomerLeave(Customer customer)
    {
        if (_nameTags.TryGetValue(customer, out NameTag tag))
        {
            DestroyImmediate(tag.gameObject);
            _nameTags.Remove(customer);
        }
    }
}
