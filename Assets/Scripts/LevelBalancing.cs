using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBalancing : MonoBehaviour
{
    private int[] enemyCount = { };
    private float[] enemySpawnRate = { };
    private int zombieAmount;
    private int batsAmount;
    private float zombieSRate;
    private float batsSRate;
    private LevelManager levelManager;
    private float spawnTime;

    private void Awake()
    {
        levelManager = GameObject.Find("GameHandler").GetComponent<LevelManager>();

        zombieAmount = 0;
        batsAmount = 0;
        zombieSRate = 0;
        batsSRate = 0;

        enemyCount = new[] { zombieAmount, batsAmount, 0, 0, 0 };
        enemySpawnRate = new[] { zombieSRate, batsSRate, 0, 0, 0 };
    }

    public int[] GetEnemyCountList()
    {
        return enemyCount;
    }


    public float[] GetEnemySpawnStats()
    {
        return enemySpawnRate;
    }


    public void SetLevelStats()
    {
        if(levelManager.GetLevel() == 1)
        {
            if(levelManager.GetPhase() == 1)
            {
                zombieAmount = 6;
                batsAmount = 0;

                zombieSRate = 1;
            }
            else if (levelManager.GetPhase() == 2)
            {
                zombieAmount = 10;
                batsAmount = 4;

                zombieSRate = 1.5f;
                batsSRate = 5;
            }
            else if (levelManager.GetPhase() == 3)
            {
                zombieAmount = 20;
                batsAmount = 6;

                zombieSRate = 1.5f;
                batsSRate = 4;
            }
        }
        else if (levelManager.GetLevel() == 2)
        {

        }
        enemyCount = new[] { zombieAmount, batsAmount, 0, 0, 0 };
        enemySpawnRate = new[] { zombieSRate, batsSRate, 0, 0, 0 };
    }


    public int GetEnemyCount()
    {
        int value = 0;
        foreach(int i in enemyCount)
        {
            value += i;
        }
        return value;
    }
}
