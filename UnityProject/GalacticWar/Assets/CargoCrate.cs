using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CargoCrate : MonoBehaviour
{
    public string spawnedResource;
    public int hitsToKill = 1;
    private List<IObjective> objectives;
    private void Update()
    {
        if (hitsToKill <= 0)
        {
            if (Resources.Load("Prefabs/Resources/" + spawnedResource) != null)
            {
                var res = GameObject.Instantiate(Resources.Load("Prefabs/Resources/" + spawnedResource)) as GameObject;
                res.transform.position = transform.position;
            }
            objectives = GetObjectiveKeepers();
            foreach (IObjective objective in objectives)
            {
                objective.OnKill(gameObject);
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<BasicProjectile>() != null)
        {
            Debug.Log("kill");
            hitsToKill -= 1;
            
        }
        
    }
    private List<IObjective> GetObjectiveKeepers()
    {
        IEnumerable<IObjective> dataPersistancesObjs = FindObjectsOfType<MonoBehaviour>().OfType<IObjective>();
        return new List<IObjective>(dataPersistancesObjs);
    }
}
