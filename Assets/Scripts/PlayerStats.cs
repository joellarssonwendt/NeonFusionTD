using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
    public static int Bits;
    public int startingBits;
    public static int Crystals; 
    public int startingCrystals;

    [Header("TowerCosts")]
    public static int normalTowerCost = 100;
    public static int iceTowerCost = 150;
    public static int lightningTowerCost = 200;
    public static int fireTowerCost = 250;

    public static int mergeCost = 10;

    private const int maxBits = 9999;
    private const int maxCrystals = 999;

    void Start()
    {
        //Bits = startingBits;
       // Crystals = startingCrystals;
    }

    public static void AddBits(int amount)
    {
        Bits += amount;
        if (Bits > maxBits)
        {
            Bits = maxBits;
        }
    }

    public static void AddCrystals(int amount)
    {
        Crystals += amount;
        if (Crystals > maxCrystals)
        {
            Crystals = maxCrystals;
        }
    }


    public void LoadData(GameData data)
    {
        PlayerStats.Bits = data.Bits;
        PlayerStats.Crystals = data.Crystals;
    }
    public void SaveData(ref GameData data)
    {
        data.Bits = PlayerStats.Bits;
        data.Crystals = PlayerStats.Crystals;
    }
}
