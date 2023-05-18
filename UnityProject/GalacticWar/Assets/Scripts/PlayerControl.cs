using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    //units per second that the player travels forwards with no input
    public float baseSpeed;
    //turn rate in radians
    public float turnSpeed;
    bool cheats;
    public Rigidbody2D body;
    public GameObject cursor;
    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        cursor = GameObject.Find("Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Shop", LoadSceneMode.Single);
        }*/
        float y = baseSpeed;
        if (!cheats)
        {
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
            
            if (Input.GetKeyDown(KeyCode.F1))
            {
                cheats = true;
            }
        }
        //cheats
        if (cheats)
        {
            float x = 0;
            y = 0;
            gameObject.GetComponent<ShipBody>().currentHealth = gameObject.GetComponent<ShipBody>().health;
            body.rotation = 0;
            if (Input.GetKey(KeyCode.W))
            {
                y = 20f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                y = -20f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                x = -20;
            }
            if (Input.GetKey(KeyCode.D))
            {
                x = 20;
            }
            body.velocity = new Vector2(x , y);
            if (Input.GetKeyDown(KeyCode.F2))
            {
                cheats = false;
            }

        }
        //scale camera size off of distance to the cursor from this
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Camera.main.orthographicSize -= 0.5f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Camera.main.orthographicSize += 0.5f;
        }
        if (cursor != null)
            CursorDistance();
        else
            cursor = GameObject.Find("Cursor");
    }
    private void CursorDistance()
    {
        Vector2 worldPosition = Camera.main.WorldToScreenPoint(transform.position);
        var dis = Vector2.Distance(worldPosition, Input.mousePosition);

        dis *= 0.05f;
        dist = dis;
        Camera.main.orthographicSize = Mathf.Min(Mathf.Max(10, dis), 25);
    }
}
