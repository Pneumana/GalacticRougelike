using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayAreaArrow : MonoBehaviour
{
    public GameObject player = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            var offset = player.transform.up * 2.5f;
            var newpos = offset + player.transform.position;
            transform.position = newpos;
            float rot_z = Mathf.Atan2(0 - newpos.y, 0 - newpos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
       
    }
}
