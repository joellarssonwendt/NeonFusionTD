using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Chrystals;
    [SerializeField] public int startingChrystals = 10;

    [SerializeField] public static int towerCost = 1;

     void Start()
    {

        Chrystals = startingChrystals;
    }
}
