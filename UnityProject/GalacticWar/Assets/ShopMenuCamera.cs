using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopMenuCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
        }
    }
    public void Equipment()
    {
        Camera.main.transform.position = new Vector3(30f, 0, -10);
        GameObject.Find("Slots").GetComponent<EquipItems>().UpdateDisplay();
    }
    public void Jobs()
    {
        Camera.main.transform.position = new Vector3(-30f, 0, -10);
    }
    public void Shop()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10);
    }
}
