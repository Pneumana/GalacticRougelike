using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;
    public float lifespan;
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
}
