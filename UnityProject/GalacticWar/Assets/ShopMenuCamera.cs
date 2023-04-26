using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenuCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Equipment()
    {
        Camera.main.transform.position = new Vector3(30f, 0, -10);
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
