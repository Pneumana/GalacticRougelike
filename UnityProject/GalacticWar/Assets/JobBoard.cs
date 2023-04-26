using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JobBoard : MonoBehaviour
{
    string[] conflictType = new string[] { "Flayer controled", "Umbral controled", "Ongoing Conflict" };
    string[] jobType = new string[] { "Elimination", "Sabotage", "Defense" };
    string[] jobDesc = new string[] { "Destroy the ships that are present here.", "Steal supplies from a passing transport", "Defend our station from invading marines" };
    public GameObject[] eliminationButtons;
    public GameObject[] saboButtons;
    public GameObject[] defenseButtons;
    // Start is called before the first frame update
    void Start()
    {
        var elim = Random.Range(0, eliminationButtons.Length);
        var sabo = Random.Range(0, saboButtons.Length);
        var defense = Random.Range(0, defenseButtons.Length);
        for(int i = 0; i < eliminationButtons.Length; i++)
        {
            if (eliminationButtons[i] != eliminationButtons[elim])
            {
                Debug.Log(eliminationButtons[i].name + " is not " + eliminationButtons[elim]);
                Destroy(eliminationButtons[i]);
            }
            else
            {
                Debug.Log(eliminationButtons[i].name + " IS " + eliminationButtons[elim]);
            }
        }
        for (int i = 0; i < saboButtons.Length; i++)
        {
            if (saboButtons[i] != saboButtons[sabo])
                Destroy(saboButtons[i]);
        }
        for (int i = 0; i < defenseButtons.Length; i++)
        {
            if (defenseButtons[i] != defenseButtons[defense])
                Destroy(defenseButtons[i]);
        }
    }
    public void StartMission(string scenename)
    {
        int payout = Random.Range(100, 250);
        DataPersistanceManager.Instance.Payout = payout;
        SceneManager.LoadScene(scenename, LoadSceneMode.Single);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
