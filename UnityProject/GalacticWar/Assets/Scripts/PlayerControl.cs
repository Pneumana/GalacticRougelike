using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //units per second that the player travels forwards with no input
    public float baseSpeed;
    //turn rate in radians
    public float turnSpeed;

    public Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float y = baseSpeed;
        if (Input.GetKey(KeyCode.W))
        {
            y *= 2.5f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y *= 0.1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.rotation += turnSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.rotation -= turnSpeed * Time.deltaTime;
        }
        body.velocity = transform.up * y;
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize += 0.1f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize -= 0.1f;
        }
    }
}
