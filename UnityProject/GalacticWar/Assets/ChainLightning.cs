using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ChainLightning : MonoBehaviour
{
    // Start is called before the first frame update
    float jumpInterval;
    List<GameObject> targets = new List<GameObject>();
    List<GameObject> alreadyHit = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(jumpInterval > 0)
            jumpInterval-= Time.deltaTime;
        if (jumpInterval <= 0)
        {
            var closestDist = Mathf.Infinity;
            GameObject closest = null;
            //attempt jump
            foreach(GameObject target in targets)
            {
                if(Vector2.Distance(transform.position, target.transform.position) < closestDist)
                {
                    if (!alreadyHit.Contains(target))
                    {
                        closestDist = Vector2.Distance(transform.position, target.transform.position);
                        closest = target;
                    }
                }
                
            }
            if (closest != null)
            {
                alreadyHit.Add(closest);
                transform.position = closest.transform.position;
                Debug.Log("jumped to " + closest.transform.position.ToString());
                jumpInterval = 0.25f;
            }
            else
            {
                //Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //cannot target ships on their team
        var ship = collision.gameObject.GetComponent<ShipBody>();
        if (ship != null)
            targets.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        var ship = collision.gameObject.GetComponent<ShipBody>();
        if (ship != null)
           targets.Remove(collision.gameObject);
    }
}
