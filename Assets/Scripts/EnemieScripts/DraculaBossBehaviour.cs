using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaBossBehaviour : MonoBehaviour
{
    private int speed;
    private bool startBoss;
    private bool spawnBats;
    private bool beams;
    private bool isBlinking;
    private Animator draculaAnimator;
    private BoxCollider2D draculaHitBox;
    private AudioManager audioManager;
    private BloodManager blood;
    private BossBarManager bossBarManager;
    private ParticleSystem orbParticles;
    private ParticleSystem beamBatParticles;
    private EnemySpawnScript enemySpawnManager;
    float count;
    float spawnCount;
    private ParticleSystem batsParticleSystem;
    private Vector3 targetLocation;

    public GameObject bossBarPrefab;
    public GameObject bossTagPrefab;


    void Awake()
    {
        speed = 50;
        startBoss = false;
        spawnBats = false;
        beams = false;
        draculaHitBox = GetComponent<BoxCollider2D>();
        draculaHitBox.enabled = false;
        draculaAnimator = GetComponent<Animator>();
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>();
        blood = GameObject.Find("GameHandler").GetComponent<BloodManager>();
        enemySpawnManager = GameObject.Find("GameHandler").GetComponent<EnemySpawnScript>();
        isBlinking = false;
        orbParticles = GameObject.Find("orbParticles").GetComponent<ParticleSystem>();
        beamBatParticles = GameObject.Find("BeamBats").GetComponent<ParticleSystem>();
        batsParticleSystem = GameObject.Find("batParticles").GetComponent<ParticleSystem>();
        orbParticles.Stop();
        beamBatParticles.Stop();
        count = 0;
        spawnCount = 0;
        targetLocation = new Vector3 (0,0,0);
    }


    void Update()
    {
        if (startBoss)
        {
            draculaHitBox.enabled = true;
            draculaAnimator.SetBool("spawnbats", true);
            startBoss = false;
        }

        if (spawnBats)
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
                transform.GetComponent<BoxCollider2D>().enabled = false;
                spawnCount = 0;
                spawnBats = false;
            }
        }

        if (beams)
        {
            if(transform.position.x >= targetLocation.x)
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

    }


    public void SpawnBatsMove()
    {
        transform.GetComponent<BoxCollider2D>().enabled = true;
        spawnBats = true;
        batsParticleSystem.transform.position += new Vector3(20f, 0f, 0f);
        batsParticleSystem.Play();
        audioManager.batsFlying.Play();
    }


    public void BeamMove()
    {
        targetLocation = new Vector3(Random.Range(-153f, -100f), Random.Range(-100f, 100f), 0);
        beamBatParticles.Play();
        StartCoroutine(BeamBatsTimer());
    }

    public void StartDraculaBossFight()
    {
        startBoss = true;
        bossBarManager = GameObject.Find("bossBar").GetComponent<BossBarManager>();      
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

        GameObject bossTag = Instantiate(bossTagPrefab, new Vector3(-469, 467, -6), Quaternion.identity);
        bossTag.name = "bossTag";
        bossTag.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet")
        {
            blood.SpawnBlood(collision.gameObject);
            Destroy(collision.gameObject);
            bossBarManager.Damage(PlayerStats.bulletPower);
            if (!isBlinking)
            {
                audioManager.draculaTakesDamage.Play();
                StartCoroutine(DraculaBlinks());
                isBlinking = true;
            }
        }
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
        beamBatParticles.Stop();
        beams = true;
    }

}
