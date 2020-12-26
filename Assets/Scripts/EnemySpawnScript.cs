using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject zombie;

    private LevelManager levelManager;
    private bool isSpawning;
    private bool zombiesMown;
    private AudioManager audioManager;
    private int spawnCounter;
    private bool pause;

    void Start()
    {
        levelManager = GameObject.Find("GameHandler").GetComponent<LevelManager>();
        isSpawning = false;
        zombiesMown = false;
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>();
        spawnCounter = 0;
        pause = false;
    }


    void Update()
    {
        if (pause)
        {

        }
        else if (isSpawning)
        {

        }
        else
        {
            if (levelManager.GetLevel() == 1)
            {
                StartCoroutine(SpawnTimer(0.1f));
            }
            isSpawning = true;
        }

        if(GameObject.Find("z") != null && !zombiesMown)
        {
            StartCoroutine(ZombieMoaner());
            zombiesMown = true;
        }
    }


    private IEnumerator SpawnTimer(float time)
    {
        if (levelManager.GetLevel() == 1 && spawnCounter < levelManager.GetEnemyCounter())
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-153f, -100f), Random.Range(-100f, 100f),-2);
            GameObject z = Instantiate(zombie, spawnPoint, Quaternion.identity);
            z.name = "z";
            z.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
            z.GetComponent<Animator>().SetBool("spawnEnemy",true);
            spawnCounter += 1;
        }
        yield return new WaitForSeconds(time);
        isSpawning = false;
    }


    private IEnumerator ZombieMoaner()
    {
        audioManager.zombies.Play();
        yield return new WaitForSeconds(38f);
        zombiesMown = false;
    }


    public void Pause()
    {
        if (pause)
        {
            pause = false;
            audioManager.zombies.UnPause();
        }
        else
        {
            pause = true;
            audioManager.zombies.Pause();
        }
    }
}
