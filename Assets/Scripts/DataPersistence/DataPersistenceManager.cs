using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
 public static DataPersistenceManager instance {  get; private set; }

    private GameData gameData;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Det finns redan en DataPersistenceManager");
            return;
        }
        instance = this;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {

    }
    public void SaveGame()
    {

    }
}
