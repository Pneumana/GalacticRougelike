using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;
    public float lifespan;
    public int damage;
    public int team;
    public float acceleration = 0;
    public float maxAcceleration = 0;
    public string payload;
    public bool pierces = true;
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        body.velocity = speed * transform.up;

        if (lifespan > 0)
            lifespan -= Time.deltaTime;
        if(lifespan<= 0)
            Destroy(gameObject);
        if (speed < maxAcceleration)
            speed += acceleration * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var shipbody = collision.gameObject.GetComponent<ShipBody>();
        if (shipbody != null)
        {
                shipbody.TakeDamage(damage);
        }
        //spawn payload
        if(payload != "")
        {
            var impact = GameObject.Instantiate(Resources.Load("Prefabs/Bullets/" + payload)) as GameObject;
            impact.transform.position = transform.position;
            impact.layer = gameObject.layer;
        }
        
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var shipbody = collision.gameObject.GetComponent<ShipBody>();
        if (shipbody != null)
        {
            shipbody.TakeDamage(damage);
        }
        if (!pierces)
            Destroy(gameObject);
    }
}
