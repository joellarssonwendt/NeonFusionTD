using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int levelNumber;
    public int Chrystals;
    public List<Tile> listOfTiles;

    public GameData()
    {
        this.Chrystals = 100;
    }

}
