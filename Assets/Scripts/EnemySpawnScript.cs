using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject zombie;
    public GameObject bats;

    private LevelManager levelManager;
    private bool zombiesMown;
    private AudioManager audioManager;
    private int spawnCounter;
    private bool pause;
    private LevelBalancing levelBalancingManager;

    int zombieCount;
    int batCount;

    private bool zombieSpawning;
    private bool batSpawning;

    private void Awake()
    {
        levelBalancingManager = GameObject.Find("GameHandler").GetComponent<LevelBalancing>();
        levelManager = GameObject.Find("GameHandler").GetComponent<LevelManager>();
        zombiesMown = false;
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>();
        spawnCounter = 0;
        zombieCount = 0;
        batCount = 0;
        pause = true;

        zombieSpawning = false;
        batSpawning = false;
    }


    void Update()
    {
        if (pause)
        {

        }
        else
        {
            if (levelManager.GetLevel() == 1)
            {
                if (!zombieSpawning)
                {
                    StartCoroutine(ZombieSpawner());
                    zombieSpawning = true;
                }
                if (!batSpawning)
                {
                    StartCoroutine(BatsSpawner());
                    batSpawning = true;
                }
            }
        }

        if(GameObject.Find("z") != null && !zombiesMown && !pause)
        {
            StartCoroutine(ZombieMoaner());
            zombiesMown = true;
        }
    }


    public void DraculaSpawnBat()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-153f, -100f), Random.Range(-10,10), -2);
        GameObject bat = Instantiate(bats, spawnPoint, Quaternion.identity);
        bat.name = "b";
        bat.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
        bat.GetComponent<Animator>().SetBool("spawnEnemy", true);
        batCount += 1;
    }

    private IEnumerator ZombieSpawner()
    {
        if (levelManager.GetLevel() == 1 && zombieCount < levelBalancingManager.GetEnemyCountList()[0])
        {
            if(levelManager.GetPhase() == 1)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(-153f, -100f), Random.Range(-100f, 100f), -2);
                GameObject z = Instantiate(zombie, spawnPoint, Quaternion.identity);
                z.name = "z";
                z.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
                z.GetComponent<Animator>().SetBool("spawnEnemy", true);
                zombieCount += 1;
            }
            else if (levelManager.GetPhase() == 2)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(-153f, -100f), Random.Range(-100f, 100f), -2);
                GameObject z = Instantiate(zombie, spawnPoint, Quaternion.identity);
                z.name = "z";
                z.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
                z.GetComponent<Animator>().SetBool("spawnEnemy", true);
                zombieCount += 1;
            }
            else if (levelManager.GetPhase() == 3)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(-153f, -100f), Random.Range(-100f, 100f), -2);
                GameObject z = Instantiate(zombie, spawnPoint, Quaternion.identity);
                z.name = "z";
                z.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
                z.GetComponent<Animator>().SetBool("spawnEnemy", true);
                zombieCount += 1;
            }
        }
        yield return new WaitForSeconds(levelBalancingManager.GetEnemySpawnStats()[0]);
        zombieSpawning = false;
    }


    private IEnumerator BatsSpawner()
    {
        if (levelManager.GetLevel() == 1 && batCount < levelBalancingManager.GetEnemyCountList()[1])
        {
            if (levelManager.GetPhase() == 1)
            {

            }
            else if (levelManager.GetPhase() == 2)
            {
                Vector3 spawnPoint = new Vector3(Random.Range(-153f, -100f), Random.Range(-100f, 100f), -2);
                GameObject bat = Instantiate(bats, spawnPoint, Quaternion.identity);
                bat.name = "b";
                bat.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
                bat.GetComponent<Animator>().SetBool("spawnEnemy", true);
                batCount += 1;
            }
            else if (levelManager.GetPhase() == 3)
            {
                Vector3 spawnPoint = new Vector3(-153f, Random.Range(-100f, 100f), -2);
                GameObject bat = Instantiate(bats, spawnPoint, Quaternion.identity);
                bat.name = "b";
                bat.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
                bat.GetComponent<Animator>().SetBool("spawnEnemy", true);
                batCount += 1;
            }
            else if (levelManager.GetPhase() == 4)
            {
                Vector3 spawnPoint = new Vector3(-153f, Random.Range(-100f, 100f), -2);
                GameObject bat = Instantiate(bats, spawnPoint, Quaternion.identity);
                bat.name = "b";
                bat.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
                bat.GetComponent<Animator>().SetBool("spawnEnemy", true);
                batCount += 1;
            }
        }
        yield return new WaitForSeconds(levelBalancingManager.GetEnemySpawnStats()[1]);
        batSpawning = false; ;
    }


    private IEnumerator ZombieMoaner()
    {
        audioManager.zombies.Play();
        yield return new WaitForSeconds(38f);
        zombiesMown = false;
    }


    public void Pause()
    {
        pause = true;
    }


    public void UnPause()
    {
        pause = false;
    }


    public void ResetSpawnCounter()
    {
        zombieCount = 0;
        batCount = 0;
    }
}
