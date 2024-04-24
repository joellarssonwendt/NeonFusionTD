using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

 public static DataPersistenceManager instance {  get; private set; }

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
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
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        // ladda all sparad data fr�n fil med data handler
        //this.gameData = dataHandler.Load();

        //om det inte finns n�n data, starta new game
        if(this.gameData == null)
        {
            Debug.Log("Ingen data hittades, b�rja med default data");
            NewGame();
        }
        // pusha alla loaded data till alla scripts som beh�ver de.
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

    }
    public void SaveGame()
    {
        // L�gg in alla data i andra scripts s� de kan uppdatera det
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // spara den datan i en fil med hj�lp av data handler
        dataHandler.Save(gameData);

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
