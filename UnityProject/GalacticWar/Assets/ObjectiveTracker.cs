using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour, IObjective
{
    public List<GameObject> objectives = new List<GameObject> ();
    // Start is called before the first frame update
    void Start()
    {
        //gets all objects tagged as objective
        var loadguns = GameObject.FindGameObjectsWithTag("Objective");
        foreach(GameObject obj in loadguns)
        {
            if (!objectives.Contains(obj))
            {
                objectives.Add(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnKill(GameObject go)
    {
        objectives.Remove(go);
        if(objectives.Count == 0)
        {
            Debug.Log("all objectives killed");
        }
    }
}
