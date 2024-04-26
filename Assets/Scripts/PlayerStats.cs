using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Bits { get; private set; }
    public int startingBits;
    public static int Crystals { get; private set; }
    public int startingCrystals;
    public static int towerCost = 100;
    public static int mergeCost = 10;

    private const int maxBits = 9999;
    private const int maxCrystals = 999;

    void Start()
    {
        Bits = startingBits;
        Crystals = startingCrystals;
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


    /*public void LoadData(GameData data)
    {
        PlayerStats.Chrystals = data.Chrystals;
    }
    public void SaveData(ref GameData data)
    {
        data.Chrystals = PlayerStats.Chrystals;
    }*/
}
