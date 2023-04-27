using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopkeeperDialouge : MonoBehaviour
{
    string[] greetings = new string[] {"'Got a new shipment today, lemme know if anything catches yer eye'", "'Good to see yer still alive and kickin'", "'Wonder if those Flayer and Umbral fellas will ever get tired of dukeing it out...'"};
    string[] thanks = new string[] { "'Always a pleasure'", "'Pleasure doin business with ya'", "'Ooo, that's a good one'", "'They won't know what hit 'em'" };
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = greetings[Random.Range(0, greetings.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SayThankYouMechanic()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = thanks[Random.Range(0, thanks.Length)];
    }
    public void YouBroke()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "'Ye don't have enough cash'";
    }
}
