using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{

    #region Singleton

    private static TextManager _instance;
    public static TextManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    public GameObject livesText;
    TextMeshProUGUI tmp_livesText;

    public GameObject scoreText;
    TextMeshProUGUI tmp_scoreText;

    public GameObject levelText;
    TextMeshProUGUI tmp_levelText;

    // Start is called before the first frame update
    void Start()
    {
        tmp_livesText = livesText.GetComponent<TextMeshProUGUI>();
        tmp_scoreText = scoreText.GetComponent<TextMeshProUGUI>();
        tmp_levelText = levelText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateLivesText()
    {
        this.tmp_livesText.text = "Lives: " + GameManager.Instance.lives;
    }

    public void updatescoreText()
    {
        this.tmp_scoreText.text = "Score: " + GameManager.Instance.Score;
    }

    public void updateLevelText() 
    {
        this.tmp_levelText.text = "Level: " + GameManager.Instance.Level;
    }
}
