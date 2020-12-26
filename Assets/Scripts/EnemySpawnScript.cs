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

    private void Awake()
    {
        levelManager = GameObject.Find("GameHandler").GetComponent<LevelManager>();
        isSpawning = false;
        zombiesMown = false;
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>();
        spawnCounter = 0;
        pause = true;
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
                StartCoroutine(SpawnTimer(levelManager.GetSpawnRate()));
            }
            isSpawning = true;
        }

        if(GameObject.Find("z") != null && !zombiesMown && !pause)
        {
            StartCoroutine(ZombieMoaner());
            zombiesMown = true;
        }
    }


    private IEnumerator SpawnTimer(float time)
    {
        Debug.Log("Enemy Counter: "+levelManager.GetEnemyCounter());
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
        pause = true;
    }


    public void UnPause()
    {
        pause = false;
    }

    public void ResetSpawnCounter()
    {
        spawnCounter = 0;
    }
}
