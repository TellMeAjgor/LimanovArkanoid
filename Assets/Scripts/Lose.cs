using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public GameObject scoreText;
    TextMeshProUGUI tmp_scoreText;

    // Start is called before the first frame update
    void Start()
    {
        tmp_scoreText = scoreText.GetComponent<TextMeshProUGUI>();
        tmp_scoreText.text = "SCORE: " + PlayerPrefs.GetInt("Score");
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
        SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("Level"));
    }
}
