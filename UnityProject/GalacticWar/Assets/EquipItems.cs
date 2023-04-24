using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItems : MonoBehaviour
{
    public int indexOffest;
    public List<string> displayIDs = new List<string> { "None", "None", "None", "None" };
    public GameObject[] displays;
    public int selected;
    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
    }

    // Update is called once per frame
    void UpdateDisplay()
    {
        //kill all display prefabs
        for(int i =0; i <4; i++)
        {
            if ((i + indexOffest) < DataPersistanceManager.Instance.gameData.weapons.Count)
                displayIDs[i] = DataPersistanceManager.Instance.gameData.weapons[i + indexOffest];
            else
                displayIDs[i] = "None";
            displays[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/guns/" + displayIDs[i]);
        }
        //spawn 
    }
    public void PanLeft()
    {
        if(indexOffest > 0)
        {
            indexOffest-= 4;
        }
        UpdateDisplay();
    }
    public void PanRight()
    {
        if (indexOffest < 12)
            indexOffest += 4;
        UpdateDisplay();
    }
    public void GetSelected(int index)
    {
        selected = indexOffest + index;
        Debug.Log("picked slot " + selected);
        Debug.Log(DataPersistanceManager.Instance.gameData.weapons[selected]);
    }
}
