using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ManualCannon : MonoBehaviour
{
    public GameObject cursor;

    private Animator animator;
    public string bullet;

    public bool attackCooldown;
    public float chargeMax = -1;
    public float currentCharge;
    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("Cursor");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //look at cursor
        Vector2 target = new Vector2(cursor.transform.position.x, cursor.transform.position.y);
        Vector2 current = new Vector2(transform.position.x,transform.position.y);
        float rot_z = Mathf.Atan2(target.y - current.y, target.x - current.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        //cannon does not have charge up
        if (Input.GetKey(KeyCode.Mouse0) && !attackCooldown && chargeMax <= 0)
        {
            //shoot
            animator.SetTrigger("shoot");
        }
        //cannon has charge up
        if (chargeMax > 0)
        {
            //charge up cannon
            if (Input.GetKey(KeyCode.Mouse0) && !attackCooldown)
            {
                currentCharge += Time.deltaTime;
            }
            //reset charge on cannon
            if (!Input.GetKey(KeyCode.Mouse0) && !attackCooldown && currentCharge > 0)
            {
                currentCharge -= Time.deltaTime;
            }
            //fire on mouse up
            if (chargeMax <= currentCharge && Input.GetKeyUp(KeyCode.Mouse0) && !attackCooldown)
            {
                animator.SetTrigger("shoot");
                currentCharge = 0;
            }
            //fire on overcharge
            if (chargeMax * 2 <= currentCharge && Input.GetKey(KeyCode.Mouse0) && !attackCooldown)
            {
                animator.SetTrigger("shoot");
                currentCharge = 0;
            }
        }
    }

    public void Shoot()
    {
        //modify the 1 by whatever multiplier is used for the animation speed
        attackCooldown = true;
        var newbullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullets/" + bullet)) as GameObject;
        newbullet.layer = gameObject.layer;
        newbullet.transform.position = transform.position;
        newbullet.transform.localRotation = transform.rotation;
        //Debug.Log("pew!");
    }
    public void ReArmCannon()
    {
        attackCooldown = false;
    }
}
