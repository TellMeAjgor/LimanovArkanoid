using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void LevelList()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void TryAgain()
    {
        SceneManager.LoadSceneAsync("Main");
        SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive);
    }
}
