using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadedData : MonoBehaviour, IDataPersistance
{
    public int arrayIndex;
    public bool isGun = true;
    void SpawnGun(string name)
    {
        var newgun = GameObject.Instantiate(Resources.Load("Prefabs/Guns/" + name)) as GameObject;
        newgun.transform.parent = transform.parent;
        newgun.transform.position = transform.position;
        Destroy(this.gameObject);
    }
    void SpawnShip(string name)
    {
        var newship = GameObject.Instantiate(Resources.Load("Prefabs/PlayerShips/" + name)) as GameObject;
        newship.transform.position = transform.position;
        if(GameObject.Find("RestrictArea") != null)
        {
            GameObject.Find("RestrictArea").GetComponent<RestrictPlayArea>().player = newship;
        }
        if (GameObject.Find("PointToCenter") != null)
        {
            GameObject.Find("PointToCenter").GetComponent<PlayAreaArrow>().player = newship;
        }
        Destroy(this.gameObject);
    }
    public void LoadData(GameData data)
    {
        if (isGun)
        {
            Debug.Log("issued spawn request for weapon " + data.weapons[arrayIndex]);
            SpawnGun(data.weapons[arrayIndex]);
        }
        else 
        {
            SpawnShip(data.shipframe);
        }
            
    }
    void LateUpdate()
    {
        if (isGun)
        {
            Debug.Log("issued spawn request for weapon " + DataPersistanceManager.Instance.gameData.weapons[arrayIndex]);
            SpawnGun(DataPersistanceManager.Instance.gameData.weapons[arrayIndex]);
        }
    }
    public void SaveData(ref GameData data)
    {
        //this object will not exist to save
    }
}
