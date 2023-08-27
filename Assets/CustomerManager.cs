using System.Collections;
using System.ComponentModel;
using Dining;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public GameObject[] customerTemplates = { };
    public int customersToArrive;

    /* ===== CUSTOMER AREAS ===== */
    [Header("Customer Common Areas")] [Description("In-game locations for customers to be bound to in the restaurant.")]
    public GameObject queuingArea;

    public Table[] tables;

    private Hintable[] _tableHints;
    /* === END CUSTOMER AREAS === */

    private CustomerQueue _customerQueue;

    public void Start()
    {
        _customerQueue = queuingArea.GetComponent<CustomerQueue>();
        configureSpawns();

        _tableHints = new Hintable[tables.Length];
        for (int i = 0; i < tables.Length; i++)
        {
            _tableHints[i] = tables[i].GetComponentInChildren<Hintable>();
        }
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

    public Chair nextAvailableSeat()
    {
        foreach (Table table in tables)
        {
            Chair availability = table.NextAvailableSeat();

            if (availability != null) return availability;
        }

        return null;
    }

    public void HintTable(int tableIndex)
    {
        // ReSharper disable once ExpressionIsAlwaysNull
        if (tableIndex >= 0 && tableIndex < _tableHints!.Length)
        {
            _tableHints[tableIndex].ShowHint();
        }
    }
    
    public void UnhintTable(int tableIndex)
    {
        // ReSharper disable once ExpressionIsAlwaysNull
        if (tableIndex >= 0 && tableIndex < _tableHints!.Length)
        {
            _tableHints[tableIndex].HideHint();
        }
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