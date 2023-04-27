using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Camera.main != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
        }
        
    }
}
