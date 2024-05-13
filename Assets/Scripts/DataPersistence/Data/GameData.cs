using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int Bits;
    public int Crystals;
    public int currentWave;
    public int currentHealth;
    public bool normalNormalUnlocked, fireFireUnlocked, normalFireUnlocked, normalIceUnlocked, normalLightningUnlocked, iceIceUnlocked, iceLightningUnlocked, iceFireUnlocked, lightningFireUnlocked, lightningLightningUnlocked;
    public SerializableDictionary<string, string> turretPrefabNames;
    public SerializableDictionary<string, Vector3> turretPositions;
    public SerializableDictionary<string, bool> MergeDictionary;


    public GameData()
    {
        //dessa utgör startvärden för sparade variabler när de inte finns någon annan sparad data
        this.Bits = 1000;
        this.Crystals = 20;
        this.currentWave = 1;
        this.currentHealth = 1;
        turretPrefabNames = new SerializableDictionary<string, string>();
        turretPositions = new SerializableDictionary<string, Vector3>();
        MergeDictionary = new SerializableDictionary<string, bool>();

        //normalNormalUnlocked = fireFireUnlocked = normalFireUnlocked = normalIceUnlocked = normalLightningUnlocked = iceIceUnlocked = iceLightningUnlocked = iceFireUnlocked = lightningFireUnlocked = lightningLightningUnlocked = false;
    }
}