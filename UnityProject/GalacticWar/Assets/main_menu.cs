using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class main_menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()

    {
        
    }

    public void GoToShop()
    {
      SceneManager.LoadScene("Shop",LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Credits()

    {
        SceneManager.LoadScene("Credits",LoadSceneMode.Single);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main_Menu",LoadSceneMode.Single);
        
    }
}

