using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ToolPlacePoint : MonoBehaviour
{
    public List<GameObject> items;
    
    public void Place(GameObject other)
    {
        for (int i = 0; i < items.Capacity; i++)
        {
            if (items[i] == null)
            {
                items[i] = other;
                return;
            }
        }

        throw new ConstraintException("This placement area already contains the maximum number of items.");
    }

    public GameObject Take(GameObject other)
    {
        int itemIndex;
        for (itemIndex = 0; itemIndex < items.Capacity; itemIndex++)
        {
            if (items[itemIndex] == other)
                break;
        }

        return itemIndex < items.Capacity ? items[itemIndex] : null;
    }
}
