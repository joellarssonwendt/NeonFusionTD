using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int levelNumber;
    public int diamondAmount;
    public List<Tile> listOfTiles;

    public GameData()
    {
        this.levelNumber = 0;
        this.diamondAmount = 100;
    }

}
