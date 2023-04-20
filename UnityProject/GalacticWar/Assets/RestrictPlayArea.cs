using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictPlayArea : MonoBehaviour
{
    public float warningRadius;
    public float killRadius;
    public GameObject player = null;
    public float currentDist;
    public GameObject turnback;
    // Start is called before the first frame update
    void Start()
    {
        //fetch player in area
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            var distance = Vector2.Distance(transform.position, player.transform.position);
            currentDist = distance;
            if (distance > warningRadius)
            {
                //turn on the turn back text
                turnback.SetActive(true);
            }
            else
            {
                //turn off the turn back text
                turnback.SetActive(false);
            }
            if (distance > killRadius)
            {
                    player.GetComponent<ShipBody>().TakeDamage(999999, 0,0,true);
            }
        }
        
    }
}
