using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
    public static int Chrystals;
    public int startingChrystals;
    public static int towerCost = 100;

     void Start()
    {
       
    }

    public void LoadData(GameData data)
    {
        PlayerStats.Chrystals = data.Chrystals;
    }
    public void SaveData(ref GameData data)
    {
        data.Chrystals = PlayerStats.Chrystals;
    }
}
