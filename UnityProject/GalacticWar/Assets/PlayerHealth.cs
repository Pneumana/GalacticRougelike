using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    GameObject player;
    public TextMeshProUGUI health;
    public TextMeshProUGUI shield;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            health.text = "Health: " + player.GetComponent<ShipBody>().currentHealth.ToString();
            int intShield = (int)player.GetComponent<ShipBody>().currentShield;
            shield.text = "Shield: " + intShield.ToString();
        }
    }
}
