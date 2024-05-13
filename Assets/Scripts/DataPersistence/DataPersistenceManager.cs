using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor.SearchService;
using Unity.VisualScripting;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    [SerializeField] private GameObject healthSystem;
    public GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public bool playerDied = false;
    public bool noData = false;
    public static DataPersistenceManager instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Hittade mer än en DataPersistenceManager i scenen.");
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
        // laddar data från fil med dataHandler
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("Ingen data hittades, starta default values");
            noData = true;
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        //Debug.Log("Crystaller när man laddar upp: " + gameData.Bits.ToString());
        // Debug.Log("wave när man laddar upp: " + gameData.currentWave.ToString());
    }
    public void SaveGame()
    {   
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (playerDied)
            {
                NewGame();
                Debug.Log("Eftersom HP är 0 så rensas data");
            }
            dataPersistenceObj.SaveData(ref gameData);
        }
        if (playerDied)
        {
            NewGame();
            Debug.Log("Eftersom HP är 0 så rensas data");
        }
        dataHandler.Save(gameData);
        
    }
    public void LoadNewGame()
    {
        NewGame();
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {

            dataPersistenceObj.LoadData(gameData);
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            if (playerDied)
            {
                NewGame();
                Debug.Log("Eftersom HP är 0 så rensas data");
            }
            dataPersistenceObj.SaveData(ref gameData);
        }
        if (playerDied)
        {
            NewGame();
            Debug.Log("Eftersom HP är 0 så rensas data");
        }
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