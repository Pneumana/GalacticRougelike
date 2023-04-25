using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEquippablePrefab : MonoBehaviour, IDataPersistance
{
    public GameObject spawnedship;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadData(GameData data)
    {
        SpawnShip(data.shipframe);
        //disable the controls on the prefab
    }
    public void UpdateData()
    {
        var shipname = DataPersistanceManager.Instance.gameData.shipframe;
        SpawnShip(shipname);
    }
    void SpawnShip(string name)
    {
        var newship = GameObject.Instantiate(Resources.Load("Prefabs/PlayerShips/" + name)) as GameObject;
        spawnedship = newship;
        newship.transform.position = transform.position;
        newship.transform.parent = transform;
        newship.name = "Playership";
        if (newship.GetComponent<PlayerControl>() != null)
        {
            newship.GetComponent<PlayerControl>().enabled = false;
        }
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        List<GameObject> childObjects = new List<GameObject>();
        foreach (Transform child in allChildren)
        {
            childObjects.Add(child.gameObject);
        }
        foreach(GameObject child in childObjects)
        {
            if(child.GetComponent<PlayerLoadedData>() != null)
            {
                //child.GetComponent<PlayerLoadedData>().enabled = false;
            }
            
            if (child.name == "Main Camera")
            {
                Destroy(child);
            }
        }
    }
    void LateUpdate()
    {

    }
    public void ReloadShip()
    {
        Destroy(spawnedship);
        var shipname = DataPersistanceManager.Instance.gameData.shipframe;
        SpawnShip(shipname);
    }
    public void SaveData(ref GameData data)
    {
        //this object will not exist to save
    }
}
