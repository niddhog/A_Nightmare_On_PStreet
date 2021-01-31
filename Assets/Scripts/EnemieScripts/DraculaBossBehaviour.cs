using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraculaBossBehaviour : MonoBehaviour
{
    private int speed;
    private int maxHealth;
    private bool startBoss;
    private bool spawnBats;
    private bool beams;
    private bool isBlinking;
    private bool draculaDied;
    private Animator draculaAnimator;
    private BoxCollider2D draculaHitBox;
    private AudioManager audioManager;
    private BloodManager blood;
    private BossBarManager bossBarManager;
    private ParticleSystem orbParticles;
    private ParticleSystem beamBatParticles;
    private EnemySpawnScript enemySpawnManager;
    private NumberManager numberManager;
    private LevelManager levelManager;
    float count;
    float spawnCount;
    private ParticleSystem batsParticleSystem;
    private ParticleSystem deathParticleSystem;
    private Vector3 targetLocation;
    private EnvironmentManager environmentManager;
    private CameraController cameraManager;
    private bool shakeDracula;
    private float shakeCount;
    private float shakeAmount;
    private bool lockShake;
    private int healthRegen;
    private PauseScript pause;

    public GameObject bossBarPrefab;
    public GameObject bossTagPrefab;


    void Awake()
    {
        maxHealth = 250;
        speed = 30;
        healthRegen = 90;
        startBoss = false;
        spawnBats = false;
        beams = false;
        shakeDracula = false;
        draculaHitBox = GetComponent<BoxCollider2D>();
        draculaHitBox.enabled = false;
        draculaAnimator = GetComponent<Animator>();
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>();
        environmentManager = GameObject.Find("GameHandler").GetComponent<EnvironmentManager>();
        blood = GameObject.Find("GameHandler").GetComponent<BloodManager>();
        enemySpawnManager = GameObject.Find("GameHandler").GetComponent<EnemySpawnScript>();
        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraController>();
        isBlinking = false;
        levelManager = GameObject.Find("GameHandler").GetComponent<LevelManager>();
        orbParticles = GameObject.Find("orbParticles").GetComponent<ParticleSystem>();
        beamBatParticles = GameObject.Find("BeamBats").GetComponent<ParticleSystem>();
        batsParticleSystem = GameObject.Find("batParticles").GetComponent<ParticleSystem>();
        deathParticleSystem = GameObject.Find("DraculaDiesParticles").GetComponent<ParticleSystem>();
        numberManager = GameObject.Find("GameHandler").GetComponent<NumberManager>();
        pause = GameObject.Find("GameHandler").GetComponent<PauseScript>();
        orbParticles.Stop();
        beamBatParticles.Stop();
        deathParticleSystem.Stop();
        count = 0;
        spawnCount = 0;
        targetLocation = new Vector3 (0,0,0);
        bossBarManager = null;
        draculaDied = false;
        shakeCount = 0;
        lockShake = false;
        shakeAmount = 1f;
    }


    void Update()
    {
        if (startBoss)
        {
            draculaHitBox.enabled = true;
            draculaAnimator.SetBool("spawnbats", true);
            startBoss = false;
        }

        if (spawnBats && !shakeDracula)
        {
            if (count >= 0.5f)
            {
                enemySpawnManager.DraculaSpawnBat();
                spawnCount++;
                count = 0;
            }
            count += Time.deltaTime;
            if (spawnCount == 4)
            {
                batsParticleSystem.Stop();
                orbParticles.Stop();
                audioManager.batsFlying.Stop();
                batsParticleSystem.transform.position -= new Vector3(20f, 0f, 0f);
                transform.GetComponent<Animator>().SetBool("beam", true);
                spawnCount = 0;
                spawnBats = false;
            }
        }

        if (beams && !shakeDracula)
        {
            if (transform.position.x >= targetLocation.x)
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            }
            else if (transform.position.x < targetLocation.x)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }

            if (transform.position.y >= targetLocation.y)
            {
                transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            }
            else if (transform.position.y < targetLocation.y)
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }

            if(transform.position.x >= targetLocation.x - 0.5f && transform.position.x <= targetLocation.x + 0.5f &&
                transform.position.y >= targetLocation.y - 0.5f && transform.position.y <= targetLocation.y + 0.5f)
            {
                transform.GetComponent<Animator>().SetBool("beam", false);
                transform.GetComponent<Animator>().SetBool("reappear", true);
                orbParticles.Play();
                beams = false;           
            }
        }
        if (bossBarManager != null && !draculaDied)
        {
            if (bossBarManager.GetHealth() <= 0)
            {
                pause.PauseGame();
                transform.GetComponent<Animator>().SetBool("dies", true);
                DraculaDies();
                draculaDied = true;
            }
        }
        if (shakeDracula)
        {
            if(shakeCount <= 0.25 && !lockShake)
            {
                shakeCount += 1 * Time.deltaTime;
                transform.position -= new Vector3(shakeAmount*Time.deltaTime, 0, 0);
            }
            else if (shakeCount > 0)
            {
                lockShake = true;
                shakeCount -= 1 * Time.deltaTime;
                transform.position += new Vector3(shakeAmount*Time.deltaTime, 0, 0);
            }
            else
            {
                lockShake = false;
            }
        }
    }


    public void SpawnBatsMove()
    {
        draculaHitBox.enabled = true;
        spawnBats = true;
        batsParticleSystem.transform.position += new Vector3(20f, 0f, 0f);
        batsParticleSystem.Play();
        audioManager.batsFlying.Play();
    }


    public void BeamMove()
    {
        bool legitLoction = false;
        while (!legitLoction)
        {
            targetLocation = new Vector3(Random.Range(-153f, -100f), Random.Range(-100f, 100f), 0);
            if(targetLocation.y > transform.position.x + 75)
            {
                legitLoction = true;
            }
        }
        beamBatParticles.Play();
        StartCoroutine(BeamBatsTimer());
    }

    public void StartDraculaBossFight()
    {
        startBoss = true;
    }


    public void SetLifeBar()
    {
        bossBarManager.FillUpHealth(maxHealth,healthRegen);
    }


    public void EnableOrbParticles()
    {
        orbParticles.Play();
    }


    public void PlaceBossBar()
    {
        GameObject bossBar = Instantiate(bossBarPrefab, new Vector3(0, 0, -5), Quaternion.identity);
        bossBar.name = "bossBar";
        bossBar.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

        GameObject bossTag = Instantiate(bossTagPrefab, new Vector3(-479, 467, -6), Quaternion.identity);
        bossTag.name = "bossTag";
        bossTag.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        bossBarManager = GameObject.Find("bossBar").GetComponent<BossBarManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet")
        {
            audioManager.hit01.Play();
            blood.SpawnBlood(collision.gameObject);
            Destroy(collision.gameObject);
            float incomingDamage = PlayerStats.GetBulletPower();
            incomingDamage = Mathf.Round(incomingDamage);
            numberManager.SpawnDamageNumber(transform.position, incomingDamage);
            bossBarManager.Damage(incomingDamage);


            bossBarManager.Damage(PlayerStats.GetBulletPower());
            if (!isBlinking)
            {
                audioManager.draculaTakesDamage.Play();
                StartCoroutine(DraculaBlinks());
                isBlinking = true;
            }
        }
    }


    public void DraculaDies()
    {
        shakeDracula = true;
        foreach(Transform prefab in GameObject.Find("PrefabSink").transform)
        {
            if(prefab.name == "z")
            {
                prefab.GetComponent<ZombieScript>().SetHealth(0);
            }
            else if (prefab.name == "b")
            {
                prefab.GetComponent<BatScript>().SetHealth(0);
            }
            else
            {

            }
        }
        audioManager.draculaDies.Play();
        deathParticleSystem.Play();
        beamBatParticles.Play();
        cameraManager.Shake(10f, 1, 50);
        environmentManager.ChangeFogIntensityOverTime(1, 1f);
        environmentManager.ChangeFogColor(1, 0, 0, 0);
        StartCoroutine(DraculaEvaporates());
    }


    private IEnumerator DraculaBlinks()
    {
        float count = 1;
        float time = 0f;
        Color tmp = transform.GetComponent<SpriteRenderer>().color;
        tmp.a = 0;
        tmp.r = 0;
        tmp.g = 0.2f;
        tmp.b = 0.2f;
        while(time < count)
        {
            if (time < 0.5f)
            {
                transform.GetComponent<SpriteRenderer>().color -= tmp;
            }
            else
            {
                transform.GetComponent<SpriteRenderer>().color += tmp;
            }
            time += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        isBlinking = false;
    }


    private IEnumerator BeamBatsTimer()
    {
        yield return new WaitForSeconds(0.3f);
        if (!draculaDied)
        {
            beamBatParticles.Stop();
        }
        draculaHitBox.enabled = false;
        beams = true;
    }

    private IEnumerator DraculaEvaporates()
    {
        yield return new WaitForSeconds(4f);
        deathParticleSystem.Stop();
        beamBatParticles.Stop();
        batsParticleSystem.Stop();
        orbParticles.Stop();
        yield return new WaitForSeconds(1.5f);
        audioManager.batsFlying.Stop();
        audioManager.zombies.Stop();
        audioManager.FadeOut(audioManager.Level1Music, 3);
        transform.GetComponent<Animator>().SetBool("vanquish", true);
        levelManager.SetEnemyCount(0);
    }
}
