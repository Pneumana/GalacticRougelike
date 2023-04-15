using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DataPersistanceManager : MonoBehaviour
{
    public GameData gameData;

    public List<IDataPersistance> dataPersistances;
    [Header("File Storage Config")]
    [SerializeField] private string filename;
    private FileDataHandler dataHandler;

    public static DataPersistanceManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("there are 2 data managers in this scene");
        }
        Instance = this;
    }
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.dataPath + "/saves/", filename);
        dataPersistances = FindAllDataPersistanceObjects();
        LoadGame();
    }
    public void PlayerStarted()
    {
        dataPersistances = FindAllDataPersistanceObjects();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        if (gameData == null)
        {
            Debug.Log("no save data found");
            NewGame();
        }

        foreach(IDataPersistance dataObj in dataPersistances)
        {
            dataObj.LoadData(gameData);
        }
        Debug.Log("loaded game with ship " + gameData.shipframe);
    }
    public void SaveGame()
    {
        foreach (IDataPersistance dataObj in dataPersistances)
        {
            dataObj.SaveData(ref gameData);
        }
        Debug.Log("saved game with ship " + gameData.shipframe);
        dataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistancesObjs = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistancesObjs);
    }
}
