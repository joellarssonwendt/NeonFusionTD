using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDataPersistence
{
    public static int Bits { get; private set; } = 0;
    public static int StartingBits { get; set; } = 0;
    public static int Crystals { get; private set; } = 0;
    public static int StartingCrystals { get; set; } = 0;

    public static event Action<int> OnBitsChanged;
    public static event Action<int> OnCrystalsChanged;

    [Header("TowerCosts")]
    public static int normalTowerCost = 100;
    public static int iceTowerCost = 150;
    public static int lightningTowerCost = 200;
    public static int fireTowerCost = 250;

    public static int mergeCost = 10;

    private const int maxBits = 9999;
    private const int maxCrystals = 999;

    public static void AddBits(int amount)
    {
        Bits += amount;
        if (Bits > maxBits)
        {
            Bits = maxBits;
        }

        OnBitsChanged?.Invoke(Bits);
    }

    public static void AddCrystals(int amount)
    {
        Crystals += amount;
        if (Crystals > maxCrystals)
        {
            Crystals = maxCrystals;
        }

        OnCrystalsChanged?.Invoke(Crystals);

    }

    public static void NotifyBitsChanged()
    {
        OnBitsChanged?.Invoke(Bits);
    }

    public static void NotifyCrystalsChanged()
    {
        OnCrystalsChanged?.Invoke(Crystals);
    }

    public void LoadData(GameData data)
    {
        PlayerStats.Bits = data.Bits;
        PlayerStats.Crystals = data.Crystals;
        Debug.Log($"Loaded Bits: {PlayerStats.Bits}, Loaded Crystals: {PlayerStats.Crystals}");
        OnBitsChanged?.Invoke(Bits);
        OnCrystalsChanged?.Invoke(Crystals);
    }
    public void SaveData(ref GameData data)
    {
        data.Bits = PlayerStats.Bits;
        data.Crystals = PlayerStats.Crystals;
    }

}
