using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selector(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }

    public void Back()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
}
