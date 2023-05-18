using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPausedControls : MonoBehaviour
{
    public bool forcePause;
    public bool paused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused)
                paused = false;
            else
                paused = true;
            Pause();
        }
    }
    public void Pause()
    {
        if(forcePause)
            Time.timeScale = 0;
        else
        {
            if(!paused)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;
        }
    }
}
