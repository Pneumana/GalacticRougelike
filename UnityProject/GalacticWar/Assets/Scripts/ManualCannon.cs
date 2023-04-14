using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ManualCannon : MonoBehaviour
{
    public GameObject cursor;
    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = new Vector2(cursor.transform.position.x, cursor.transform.position.y);
        Vector2 current = new Vector2(transform.position.x,transform.position.y);
        float rot_z = Mathf.Atan2(target.y - current.y, target.x - current.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //shoot
        }
    }
}
