using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBody : MonoBehaviour
{
    public int team;
    public int health;
    public int shield;
    public int currentHealth;
    public float currentShield;
    //delay before the shield starts regenerating
    public float shieldDelay = -1;
    private float hittimer;
    public float shieldRegen = 0.01f;
    public void Start()
    {
        currentHealth = health;
        currentShield = shield;
    }
    private void Update()
    {
        if(hittimer > 0)
            hittimer -= Time.deltaTime;
        if(shieldDelay <=0 && shieldDelay > -1 && currentShield< shield)
        {
            //restores 1% of max shield per second
            currentShield += (shield * shieldRegen) * Time.deltaTime;
        }
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
                health -= damage + healthDMG;
            }
                
        }
        else
        {
            //only damage health
            currentHealth -= damage + healthDMG;
        }
        if(currentHealth <= 0)
        {
            AutoTurret[] turrets = GetComponentsInChildren<AutoTurret>();
            //die
            Debug.Log("I AM HAVE DIE! GRAHHH");
            if(gameObject.GetComponent<PlayerControl>() != null)
            {
                gameObject.GetComponent<PlayerControl>().enabled = false;
            }
            if (gameObject.GetComponent<EnemyFighterAI>() != null)
            {
                gameObject.GetComponent<EnemyFighterAI>().enabled = false;
                
            }
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            //disable autocannons
            foreach (AutoTurret cannon in turrets)
            {
                cannon.enabled = false;
            }
        }
    }
}
