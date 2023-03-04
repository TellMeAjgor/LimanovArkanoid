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

    private void Start()
    {
        this.lives = this.tmpLives;
        Ball.OnBallDeath += OnBallDeath;
        Physics2D.IgnoreLayerCollision(6, 7);
        Physics2D.IgnoreLayerCollision(6, 6);
        Laser.projectiles.Clear();
    }

    private void OnBallDeath(Ball obj)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {

            this.lives--;
            TextManager.Instance.updateLivesText();
            foreach (var collectable in CollectableManager.Instance.Active)
            {
                collectable.removeEffect();
            }

            if (this.lives < 1)
            {
                //Wczytaj ekran przegranej
                SceneManager.LoadSceneAsync("LoseScreen");
            }
            else
            {
                ResetBallPosition();
                //Wczytaj ponownie poziom
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
            //SceneManager.LoadSceneAsync(GetLevel()+1);
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
        lives = tmpLives;
        Score = 0;
        PlatformScript.Instance.speedMultiplier = 1;
        TextManager.Instance.updateLivesText();
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
    
}
