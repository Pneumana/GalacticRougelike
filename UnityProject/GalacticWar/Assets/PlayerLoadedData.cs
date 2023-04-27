using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLoadedData : MonoBehaviour, IDataPersistance
{
    public int arrayIndex;
    public bool isGun = true;
    bool mousedOver;
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
        newship.GetComponent<ShipBody>().currentHealth = DataPersistanceManager.Instance.gameData.health;
        if(GameObject.Find("RestrictArea") != null)
        {
            GameObject.Find("RestrictArea").GetComponent<RestrictPlayArea>().player = newship;
        }
        if (GameObject.Find("PointToCenter") != null)
        {
            GameObject.Find("PointToCenter").GetComponent<PlayAreaArrow>().player = newship;
        }
        var ui = Instantiate(Resources.Load("Prefabs/PlayerHealth")) as GameObject;
        var loadguns = GameObject.FindGameObjectsWithTag("LoadGun");
        foreach(GameObject loader in loadguns)
        {
            var loaderData = loader.GetComponent<PlayerLoadedData>();
            var goofiestVar = DataPersistanceManager.Instance.gameData.weapons[DataPersistanceManager.Instance.gameData.equippedWeapons[loaderData.arrayIndex]];
            if (goofiestVar == "Shield")
            {
                newship.GetComponent<ShipBody>().shield += 5;
            }
            if (goofiestVar == "Hull")
            {
                newship.GetComponent<ShipBody>().health += 15;
            }
            loaderData.SpawnGun(goofiestVar);
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
            var gamedata = DataPersistanceManager.Instance.gameData;
            try
            {
                transform.Find("Circle").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/guns/" + gamedata.weapons[gamedata.equippedWeapons[arrayIndex]]);
                transform.localScale = new Vector3(2, 2, 1);
            }
            catch
            {

            }
        }

    }
    public void Clicked()
    {
        Debug.Log("clicked");
        var slots = GameObject.Find("Slots");
        if (slots != null)
        {
            var newequip = slots.GetComponent<EquipItems>().selected;
            if (!DataPersistanceManager.Instance.gameData.equippedWeapons.Contains(newequip))
            {
                DataPersistanceManager.Instance.gameData.equippedWeapons[arrayIndex] = newequip;
                Debug.Log(arrayIndex + " set to " + DataPersistanceManager.Instance.gameData.weapons[newequip]);
            }
                
            else
                Debug.Log("That weapon is already equipped");
            Debug.Log(DataPersistanceManager.Instance.gameData.equippedWeapons);
        }
        if (isGun)
        {
            if(DataPersistanceManager.Instance.gameData.equippedWeapons[arrayIndex] > -1)
            {
                transform.Find("Circle").gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/guns/" + DataPersistanceManager.Instance.gameData.weapons[DataPersistanceManager.Instance.gameData.equippedWeapons[arrayIndex]]);
                transform.localScale = new Vector3(2, 2, 1);
            }
        }
    }
    public void SaveData(ref GameData data)
    {
        //this object will not exist to save
    }
    public void OnMouseEnter()
    {
        mousedOver = true;
        Debug.Log("mouse over");
    }
    public void OnMouseExit()
    {
        mousedOver = false;
    }

}
