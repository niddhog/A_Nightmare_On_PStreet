using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject zombie;

    private LevelManager levelManager;
    private bool isSpawning;

    void Start()
    {
        levelManager = GameObject.Find("GameHandler").GetComponent<LevelManager>();
        isSpawning = false;
    }


    void Update()
    {
        if (isSpawning)
        {

        }
        else
        {
            if (levelManager.GetLevel() == 1)
            {
                StartCoroutine(SpawnTimer(10f));
            }
            isSpawning = true;
        }
    }


    private IEnumerator SpawnTimer(float time)
    {
        if (levelManager.GetLevel() == 1)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-153f, -100f), Random.Range(-108f, 113f),-2);
            GameObject z = Instantiate(zombie, spawnPoint, Quaternion.identity);
            z.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
            z.GetComponent<Animator>().SetBool("spawnEnemy",true);
        }
        yield return new WaitForSeconds(time);
        isSpawning = false;
    }
}
