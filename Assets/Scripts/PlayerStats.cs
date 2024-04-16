using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Chrystals;
    public int startingChrystals;
    public static int towerCost = 100;

     void Start()
    {
        Chrystals = startingChrystals;
    }
}
