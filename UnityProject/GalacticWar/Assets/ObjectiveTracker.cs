using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTracker : MonoBehaviour, IObjective
{
    bool spawnedItem;
    public List<GameObject> objectives = new List<GameObject> ();
    GameObject player = null;
    public List<GameObject> objectMarkers = new List<GameObject> ();
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
            var marker = new GameObject("ObjectiveMarker");
            SpriteRenderer renderer = marker.AddComponent<SpriteRenderer>();
            renderer.sprite = Resources.Load<Sprite>("Textures/UI/objective");
            if(player != null)
                marker.transform.parent = player.transform;
            objectMarkers.Add(marker);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        else
        {
            for (int i = 0; i < objectives.Count; i++)
            {
                var lookangle = Mathf.Atan2(objectives[i].transform.position.y - player.transform.position.y, objectives[i].transform.position.x - player.transform.position.x) * Mathf.Rad2Deg;
                objectMarkers[i].transform.rotation = Quaternion.Euler(0, 0, lookangle -90) ;
                objectMarkers[i].transform.position = player.transform.position;
                var distance = Vector2.Distance(player.transform.position, objectMarkers[i].transform.position);
                if (distance < 5)
                    objectMarkers[i].GetComponent<SpriteRenderer>().color = new Color(1,1,1, 1 - (distance * 0.1f));
                else
                {
                    objectMarkers[i].GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
                }

            }
        }
        
    }
    public void OnKill(GameObject go)
    {
        objectives.Remove(go);
        if(objectives.Count == 0)
        {
            Debug.Log("all objectives killed");
            //create extraction zone at 0,0
            foreach (GameObject game in objectMarkers)
            {
                Destroy(game);
            }
            if (!spawnedItem)
            {
                var exit = Instantiate(Resources.Load<GameObject>("Prefabs/GameLogic/ExtractionZone"));
                exit.transform.position = new Vector3(0, 0, 0);
                spawnedItem = true;
            }
            
        }
        else
        {
            foreach(GameObject game in objectMarkers)
            {
                Destroy(game);
            }
            objectMarkers.Clear();
            foreach(GameObject objective in objectives)
            {
                var marker = new GameObject("ObjectiveMarker");
                SpriteRenderer renderer = marker.AddComponent<SpriteRenderer>();
                renderer.sprite = Resources.Load<Sprite>("Textures/UI/objective");
                marker.transform.parent = player.transform;
                objectMarkers.Add(marker);
            }
            
        }
    }
}
