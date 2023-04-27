using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class ShipBody : MonoBehaviour
{
    public int team;
    public int health;
    public int shield;
    public int currentHealth;
    public float currentShield;
    public bool isDead;
    //delay before the shield starts regenerating
    public float shieldDelay = -1;
    public float hittimer;
    public float shieldRegen = 0.01f;
    private List<IObjective> objectives;
    public void Start()
    {
        currentHealth = health;
        currentShield = shield;
    }
    private void Update()
    {
        if(hittimer > 0)
        {
            hittimer -= Time.deltaTime;
        }
        if(hittimer <=0 && hittimer > -1 && currentShield< shield)
        {
            //restores 1% of max shield per second
            currentShield += (shield * shieldRegen) * Time.deltaTime;
        }
    }
    void Die()
    {
        AutoTurret[] turrets = GetComponentsInChildren<AutoTurret>();
        ManualCannon[] cannons = GetComponentsInChildren<ManualCannon>();
        //die
        Debug.Log("I AM HAVE DIE! GRAHHH");
        if (gameObject.GetComponent<PlayerControl>() != null)
        {
            
            gameObject.GetComponent<PlayerControl>().enabled = false;
        }
        if (gameObject.GetComponent<EnemyFighterAI>() != null)
        {
            
            gameObject.GetComponent<EnemyFighterAI>().enabled = false;

        }
       /* if (gameObject.GetComponent<CargoShipAI>() != null)
        {
            gameObject.GetComponent<CargoShipAI>().enabled = false;

        }*/
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        //disable autocannons
        foreach (AutoTurret cannon in turrets)
        {
            cannon.enabled = false;
        }
        foreach (ManualCannon cannon in cannons)
        {
            cannon.enabled = false;
        }
        if (!isDead)
        {
            var newship = GameObject.Instantiate(Resources.Load("Prefabs/FVX/explosion")) as GameObject;
            newship.transform.position = transform.position;
            newship.transform.parent = transform;
        }
        objectives = GetObjectiveKeepers();
        foreach (IObjective objective in objectives)
        {
            objective.OnKill(gameObject);
        }
        //create explosionFX
        isDead = true;
    }
    public void TakeDamage(int damage, int healthDMG = 0, int shieldDMG = 0, bool piercesShield = false, bool isEMP = false)
    {
        //healthDMG is bonus damage recieved to health specifically
        //shieldDMG is the same but for shields.
        //pierces shiled ignores shields entirely
        //isEMP will stun the ship.

        hittimer = shieldDelay;
        if (!piercesShield)
        {
            //normal damage
            if (currentShield > 0)
            {
                currentShield -= damage + shieldDMG;
            }
            if (currentShield <= 0)
            {
                currentHealth -= damage + healthDMG;
            }
                
        }
        else
        {
            //only damage health
            currentHealth -= damage + healthDMG;
        }
        if(currentHealth <= 0)
        {
            Die();
        }
        //overkill damage destroys the game object
    }
    private List<IObjective> GetObjectiveKeepers()
    {
        IEnumerable<IObjective> dataPersistancesObjs = FindObjectsOfType<MonoBehaviour>().OfType<IObjective>();
        return new List<IObjective>(dataPersistancesObjs);
    }
}
