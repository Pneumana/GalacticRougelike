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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var shipbody = collision.gameObject.GetComponent<ShipBody>();
        if (shipbody != null)
        {
                shipbody.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
