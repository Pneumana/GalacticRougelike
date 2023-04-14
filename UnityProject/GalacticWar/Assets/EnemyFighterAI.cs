using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighterAI : MonoBehaviour
{
    //this is just the movement of the ships.
    public int team;
    public Vector2 target;

    public float speed;
    public int turnSpeed;
    private Rigidbody2D body;
    public AlertOwner aggroRange;
    public float lockedOn;
    public GameObject lockedOnObj;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (lockedOn > 0)
        {
            lockedOn -= Time.deltaTime;
        }

        Vector2 current = new Vector2(transform.position.x, transform.position.y);
        float rot_z = Mathf.Atan2(target.y - current.y, target.x - current.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, 0f, rot_z - 90), turnSpeed * Time.deltaTime);
        
        if (aggroRange.targets.Count > 0)
        {
            if ((Vector2.Distance(target, current) > speed)) 
            {
                float closestDistance = Mathf.Infinity;
                GameObject closestTarget = null;
                float currentDistance = 0;
                foreach (GameObject potentialTarget in aggroRange.targets)
                {
                    currentDistance = Vector2.Distance(potentialTarget.transform.position, transform.position);
                    if (currentDistance < closestDistance)
                    {
                        closestDistance = currentDistance;
                        closestTarget = potentialTarget;
                    }
                }
                /*if (Vector2.Distance(target, current) < speed)
                {*/
                lockedOnObj = closestTarget;
                lockedOn = 5;
                target = new Vector2(lockedOnObj.transform.position.x, lockedOnObj.transform.position.y);
                //}
            }
        }
        else 
        {
            if (Vector2.Distance(target, current) < speed)
            {
                Patrol();
            }
        }
        //will keep targeting the target outside of their aggro range until the locked on counter expires
        if (lockedOn > 0)
        {
            if (Vector2.Distance(target, current) < speed)
            {
                target = -transform.up * speed;
            }
            else
            {
                target = lockedOnObj.transform.position;
            }
        }
        body.velocity = transform.up *speed;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, target, Color.red);
    }
    public void SetTargetToAttacker(Vector2 attacker)
    {
        target = attacker;
    }

    void Patrol()
    {
        target = new Vector2(transform.position.x + Random.Range(-5,5), transform.position.y + Random.Range(-5, 5));
    }
}
