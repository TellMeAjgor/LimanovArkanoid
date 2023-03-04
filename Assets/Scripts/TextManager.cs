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

    public GameObject scoreText;
    TextMeshProUGUI tmp_scoreText;

    // Start is called before the first frame update
    void Start()
    {
        tmp_scoreText = scoreText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatescoreText()
    {
        this.tmp_scoreText.text = "Score: " + GameManager.Instance.Score;
    }
}
