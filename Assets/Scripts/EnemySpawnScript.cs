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
        Vector3 draculaPosition = GameObject.Find("dracula").transform.position;
        Vector3 spawnTarget = new Vector3(0, Random.Range(draculaPosition.y-20,draculaPosition.y+20), 0);
        spawnTarget.y = Mathf.Clamp(spawnTarget.y,-100,100);
        spawnTarget.x = Random.Range(draculaPosition.x - 10, draculaPosition.x + 10);

        Vector3 spawnPoint = new Vector3(spawnTarget.x, spawnTarget.y, -41);
        GameObject b = Instantiation_CAE.Instantiation(bats, spawnPoint, Quaternion.identity, "b", "PrefabSink");
        Instantiation_CAE.SetAnimatorBool(b, "spawnEnemy", true);
        batCount++;
    }

    private IEnumerator ZombieSpawner()
    {
        if (levelManager.GetLevel() == 1 && zombieCount < levelBalancingManager.GetEnemyCountList()[0])
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-153f, -100f), Random.Range(-100f, 100f), -2);
            GameObject z = Instantiation_CAE.Instantiation(zombie, spawnPoint, Quaternion.identity, "z", "PrefabSink");
            Instantiation_CAE.SetAnimatorBool(z, "spawnEnemy", true);
            zombieCount++;
        }
        yield return new WaitForSeconds(levelBalancingManager.GetEnemySpawnStats()[0]);
        zombieSpawning = false;
    }


    private IEnumerator BatsSpawner()
    {
        if (levelManager.GetLevel() == 1 && batCount < levelBalancingManager.GetEnemyCountList()[1])
        {
            Vector3 spawnPoint = new Vector3(-153f, Random.Range(-100f, 100f), -41);
            GameObject b = Instantiation_CAE.Instantiation(bats, spawnPoint, Quaternion.identity, "b", "PrefabSink");
            Instantiation_CAE.SetAnimatorBool(b, "spawnEnemy", true);
            batCount++;
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
