using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using System.Numerics;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{

    #region Singleton

    private static PlatformScript _instance;
    public static PlatformScript Instance => _instance;

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

    public float speedMultiplier = 1;

    //Catch
    public bool catchIsAvailable = false;
    public GameObject catchedBall;
    public bool ballCatched = false;

    //Shoot
    public bool platformIsShooting = false;
    public Projectile laserShootPrefab;
    public GameObject leftParticles;
    public GameObject rightParticles;
    private float shootingDuration = 5;
    public float shootingDurationLeft=0;
    private AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        UnityEngine.Vector2 minScreenBounds = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector2(0, 0));
        UnityEngine.Vector2 maxScreenBounds = Camera.main.ScreenToWorldPoint(new UnityEngine.Vector2(Screen.width, Screen.height));

        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new UnityEngine.Vector2(Mathf.Clamp(mousePosition.x, minScreenBounds.x + 1, maxScreenBounds.x - 1), transform.position.y);     
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            if (catchIsAvailable)
            {
                if(!ballCatched)
                {
                    catchedBall = collision.gameObject;
                    Rigidbody2D catchedBallRb = catchedBall.GetComponent<Rigidbody2D>();
                    catchedBallRb.velocity = Vector3.zero;
                    ballCatched = true;
                    catchIsAvailable = false;
                }           
            }
            else
            {
                Rigidbody2D ballRB = collision.gameObject.GetComponent<Rigidbody2D>();
                Vector3 hitPoint = collision.contacts[0].point;
                Vector3 platformCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

                float difference = hitPoint.x - platformCenter.x;

                ballRB.AddForce(new Vector2(difference * 200, 0));

                ballRB.velocity = new Vector2(Mathf.Clamp(ballRB.velocity.x, -5, 5), ballRB.velocity.x);
                ballRB.velocity = new Vector2(ballRB.velocity.x, Mathf.Sqrt(60 - Mathf.Pow(ballRB.velocity.x, 2)));
                ballRB.velocity *= new Vector2(speedMultiplier, speedMultiplier);
            }           
        }
    }

    public void PlayCollectableSound()
    {
        _audioSource.Play();
    }

    public void StartShooting()
    {
        if (!this.platformIsShooting)
        {        
            this.platformIsShooting = true;
            StartCoroutine(StartShootingRoutine());
        }
        else
        {
            shootingDurationLeft = shootingDuration;
        }
    }

    public IEnumerator StartShootingRoutine()
    { 
        float fireCooldown = 0.7f;
        float fireCooldownLeft = 0;
        shootingDurationLeft = shootingDuration;     

        while (shootingDurationLeft >= 0)
        {
            fireCooldownLeft -= Time.deltaTime;
            shootingDurationLeft -= Time.deltaTime;

            if (fireCooldownLeft <= 0)
            {
                Shoot();
                fireCooldownLeft = fireCooldown;
            }
            yield return null;
        }

        this.platformIsShooting = false;

        leftParticles.SetActive(false);
        rightParticles.SetActive(false);

    }

    private void Shoot()
    {
        leftParticles.SetActive(false);
        rightParticles.SetActive(false);
        leftParticles.SetActive(true);
        rightParticles.SetActive(true);

        this.SpawnLaser(leftParticles);
        this.SpawnLaser(rightParticles);
    }

    private void SpawnLaser(GameObject particlesPoint)
    {
        Vector3 spawnPoint = new Vector3(particlesPoint.transform.position.x, particlesPoint.transform.position.y + 0.2f, particlesPoint.transform.position.z);
        Projectile laser = Instantiate(laserShootPrefab, spawnPoint, Quaternion.identity);
        Laser.projectiles.Add(laser);
        Rigidbody2D laserRb = laser.GetComponent<Rigidbody2D>();
        laserRb.isKinematic = false;
        laserRb.AddForce(new Vector2(0, 300f));
        
    }
}
