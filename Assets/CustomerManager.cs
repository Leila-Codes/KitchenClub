using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject[] customerTemplates = { };
    public int customersToArrive;

    /* ===== CUSTOMER AREAS ===== */
    [Header("Customer Common Areas")] [Description("In-game locations for customers to be bound to in the restaurant.")]
    public GameObject queuingArea;
    public Dining.Table[] tables;
    /* === END CUSTOMER AREAS === */

    private CustomerQueue _customerQueue;

    public void Start()
    {
        _customerQueue = queuingArea.GetComponent<CustomerQueue>();
        configureSpawns();
    }

    private void configureSpawns()
    {
        float nextSpawnTime = 0;
        for (int i = 0; i < customersToArrive; i++)
        {
            // nextSpawnTime += Mathf.Floor(Random.Range(5, 30));
            // nextSpawnTime += Mathf.Floor(Random.Range(3, 5));
            nextSpawnTime += 5;
            StartCoroutine(SpawnCustomer(nextSpawnTime));
            Debug.Log("Next customer spawns in " + nextSpawnTime + " seconds");
        }
    }

    public Dining.Chair nextAvailableSeat()
    {
        foreach (Dining.Table table in tables)
        {
            Dining.Chair availability = table.NextAvailableSeat();

            if (availability != null) return availability;
        }

        return null;
    }


    private IEnumerator SpawnCustomer(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Select random customer from the template list and spawn.
        int i = Random.Range(0, customerTemplates.Length);
        GameObject selected = customerTemplates[i];

        _customerQueue.AddCustomer(
            Instantiate(selected, transform)
        );
    }
}