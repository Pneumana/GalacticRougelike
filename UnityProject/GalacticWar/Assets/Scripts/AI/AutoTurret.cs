using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AutoTurret : MonoBehaviour
{
    public AlertOwner aggroRange;
    private Animator animator;
    public string bullet;

    public bool attackCooldown;
    public int inheiritedTeam; 
    public Vector2 looksAt;
    public bool nothingNear;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inheiritedTeam = GetComponentInParent<ShipBody>().team;
    }

    // Update is called once per frame
    void Update()
    {
        GetClosest();
        Vector2 target = new Vector2(looksAt.x, looksAt.y);
        Vector2 current = new Vector2(transform.position.x, transform.position.y);
        float rot_z = Mathf.Atan2(target.y - current.y, target.x - current.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        if(!attackCooldown && !nothingNear)
            animator.SetTrigger("shoot");
    }

    void GetClosest()
    {
        nothingNear = true;
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
        if(closestTarget != null)
        {
            nothingNear = false;
            looksAt = new Vector2(closestTarget.transform.position.x, closestTarget.transform.position.y);
        }
    }
    public void Shoot()
    {
        //modify the 1 by whatever multiplier is used for the animation speed
        attackCooldown = true;
        var newbullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullets/" + bullet)) as GameObject;
        newbullet.transform.position = transform.position;
        newbullet.transform.localRotation = transform.rotation;
        //Debug.Log("pew!");
    }
    public void ReArmCannon()
    {
        attackCooldown = false;
    }
}
