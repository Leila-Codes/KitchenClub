using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    public int maxCustomers = 5;

    [Description("The area in game world to queue customers")]
    public GameObject waitingArea;

    private readonly Queue<GameObject> _customers = new();

    public void AddCustomer(GameObject customer)
    {
        if (_customers.Count >= maxCustomers) return;

        _customers.Enqueue(customer);

        // Re-position the customer to directly on top of the waiting area.
        customer.transform.position = waitingArea.transform.position;

        // Face in the queueing direction
        customer.transform.rotation = Quaternion.Euler(0, 90, 0);
        
        // Adjust customer's position based on # of customers in the queue.
        customer.transform.Translate(
            (3f - _customers.Count) * -customer.transform.right
        );
    }

    public GameObject DequeueCustomer()
    {
        return _customers.Count > 0 ? _customers.Dequeue() : null;
    }
}