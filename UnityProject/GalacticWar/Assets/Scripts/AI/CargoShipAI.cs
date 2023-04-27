using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoShipAI : MonoBehaviour
{

    public float speed;
    private Rigidbody2D body;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = transform.up * speed;
        if(GetComponent<ShipBody>().isDead )
        {
            body.velocity = Vector2.zero;
        }
    }
}
