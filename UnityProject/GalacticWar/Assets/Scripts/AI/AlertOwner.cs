using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertOwner : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    public ShipBody mybody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cannot target ships on their team
        var ship = collision.gameObject.GetComponent<ShipBody>();
        if (ship != null)
            if (ship.team != mybody.team)
                targets.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var ship = collision.gameObject.GetComponent<ShipBody>();
        if (ship != null)
            if (ship.team != mybody.team)
                targets.Remove(collision.gameObject);
    }

}
