using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelManager : MonoBehaviour
{
    public GameObject levelText;
    TextMeshProUGUI tmp_levelText;

    public GameObject scoreText;
    TextMeshProUGUI tmp_scoreText;

    // Start is called before the first frame update
    void Start()
    {
        tmp_levelText = levelText.GetComponent<TextMeshProUGUI>();
        tmp_scoreText = scoreText.GetComponent<TextMeshProUGUI>();
        UpdateLevelText();
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLevelText()
    {
        this.tmp_levelText.text = "You passed level " + (PlayerPrefs.GetInt("Level")-4);
    }

    public void UpdateScoreText()
    {
        this.tmp_scoreText.text = "Score: " + PlayerPrefs.GetInt("Score");
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
