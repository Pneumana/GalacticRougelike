using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthUpdater : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = DataPersistanceManager.Instance.gameData.health.ToString() + " Health";
    }
}
