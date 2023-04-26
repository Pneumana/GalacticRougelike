using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMission : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //write the player's current health to game data
            var datamanager = DataPersistanceManager.Instance;
            datamanager.gameData.health = collision.gameObject.GetComponent<ShipBody>().currentHealth;
            datamanager.gameData.money += datamanager.Payout;
            datamanager.Payout = 0;
            SceneManager.LoadScene("Shop", LoadSceneMode.Single);
        }
    }
}
