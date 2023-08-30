using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    // Number of customers to spawn in the restaurant.
    public int customersToServe;
    
    // From 5 - 60 seconds.
    [Range(5, 60)]
    public int customerWaitTime;

    // From 30 seconds to 5 mins
    [Range(30, 300)]
    public int customerFoodWaitTime;

    // The maximum score the player can achieve.
    public float maxScore;
}
