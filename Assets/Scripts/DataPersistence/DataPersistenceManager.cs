using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
 public static DataPersistenceManager instance {  get; private set; }

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Det finns redan en DataPersistenceManager");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        // Todod ladda sparad data fr�n file med data handler
        //om det inte finns n�n data, starta new game
        if(this.gameData == null)
        {
            Debug.Log("Ingen data hittades, b�rja med default data");
            NewGame();
        }
        // TODO, pusha alla loaded data till alla scripts som beh�ver de.
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("Loaded crystal count = " + gameData.Chrystals);
    }
    public void SaveGame()
    {
        // Todo, l�gg in alla data i andra scripts s� de kan uppdatera det
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        //TODO , spara den datan i en fil med hj�lp av data handler
        Debug.Log("Saved crystal count = " + gameData.Chrystals);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
