using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentWave;
    public int Chrystals;
    public int currentHealth;
    public SerializableDictionary<string, string> turretPrefabNames;
    public SerializableDictionary<string, Vector3> turretPositions;

    public GameData()
    {
        this.Chrystals = 100;
        this.currentWave = 1;
        this.currentHealth = 10;
        turretPrefabNames = new SerializableDictionary<string, string>();
        turretPositions = new SerializableDictionary<string, Vector3>();
    }
}
