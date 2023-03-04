using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    #region Singleton

    private static GameManager _instance;
    public static GameManager Instance => _instance;

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

    public bool isGameStarted { get; set; }
    public int Score { get; set; }

    public int lives { get; set; }
    public int tmpLives = 3;

    private GameObject[] hearts;
    private AudioSource _audioSource;

    private void Start()
    {
        hearts = GameObject.FindGameObjectsWithTag("heart");
        PlaceHearts();
        _audioSource = GetComponent<AudioSource>();
        changeLives(tmpLives);
        Ball.OnBallDeath += OnBallDeath;
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(6, 6);
        Laser.projectiles.Clear();
    }

    private void PlaceHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            float pos_y = hearts[i].transform.localPosition.y;
            float pos_x = -7 + i * 0.6f;
            hearts[i].transform.localPosition = new Vector2(pos_x, pos_y);
        }
    }

    private void OnBallDeath(Ball obj)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {
            _audioSource.Play();
            changeLives(lives - 1);
            foreach (var collectable in CollectableManager.Instance.Active)
            {
                collectable.removeEffect();
            }

            if (this.lives < 1)
            {
                PlayerPrefs.SetInt("Level", GetLevel());
                PlayerPrefs.SetInt("Score", Score);
                SceneManager.LoadSceneAsync("LoseScreen");
            }
            else
            {
                ResetBallPosition();
            }
        }
    }

    private void ResetBallPosition()
    {
        BallsManager.Instance.ResetBalls();
        isGameStarted = false;
    }

    public void LoadNextLevel()
    {
        ResetBallPosition();
        CollectableManager.Instance.ResetCollectables();
        
        if (GetLevel() >= 37)
        {
            SceneManager.LoadSceneAsync("WinScreen");
        }
        else
        {
            PlayerPrefs.SetInt("Level", GetLevel());
            PlayerPrefs.SetInt("Score", Score);
            SceneManager.LoadScene(38);
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("Level")+1);
    }

    // Resetting values when changing level
    public void resetValues()
    {
        Score = 0;
        PlatformScript.Instance.speedMultiplier = 1;
        changeLives(tmpLives);
        TextManager.Instance.updatescoreText();
    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }

    public int GetLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void changeLives(int new_val)
    {
        lives = new_val;
        for (int i = 1; i <= hearts.Length; i++)
        {
            hearts[i-1].GetComponent<Renderer>().enabled = (i <= lives);
        }
    }
}
