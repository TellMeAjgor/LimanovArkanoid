using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /*public string camera;
    public string levelName;*/
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Main");
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}   