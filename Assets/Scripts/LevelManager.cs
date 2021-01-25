using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton
public class LevelManager : MonoBehaviour
{
    private int level;
    private int phase;
    private bool sequenceOngoing;

    public GameObject heroPrefab;
    public GameObject smallSkullPrefab;
    public GameObject bossSkullPrefab;

    private SequenceManager sequenceManager;
    private EnemySpawnScript spawnScript;
    private LevelBalancing levelBalancingManager;
    private GameObject heroIcon;
    private int enemyCounter;
    private int maxEnemies;
    private GameProgressionManager progression;
    private float xStep;
    private float xTarget;
    private float xHero;
    private List<float> stepList;

    private bool levelInProgress;
    private bool movementInProgress;


    void Awake()

    {
        sequenceOngoing = false;
        sequenceManager = GameObject.Find("GameHandler").GetComponent<SequenceManager>();
        spawnScript = GameObject.Find("GameHandler").GetComponent<EnemySpawnScript>();
        levelBalancingManager = GameObject.Find("GameHandler").GetComponent<LevelBalancing>();
        stepList = new List<float>();
        levelInProgress = false;
        movementInProgress = false;
        level = 1;
        phase = 1;
        enemyCounter = 1;
        progression = GameObject.Find("GameHandler").GetComponent<GameProgressionManager>();
        levelBalancingManager.SetLevelStats();
    }


    public int GetLevel()
    {
        return level;
    }


    public int GetPhase()
    {
        return phase;
    }


    public void SetLevel(int l)
    {
        level = l;
    }


    public void ReduceEnemy()
    {
        enemyCounter -= 1;
        if(enemyCounter < 0)
        {
            enemyCounter = 0;
        }
        MoveHeroIcon();
    }


    public int GetEnemyCounter()
    {
        return maxEnemies;
    }


    //Sets up Icons for the level
    public void StartGameFlow()
    {
        heroIcon = Instantiate(heroPrefab, new Vector3(50f, 340f, -3), Quaternion.identity);
        heroIcon.name = "heroIcon";
        heroIcon.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        phase = 1;
        spawnScript.UnPause();
        if (level == 1)
        {
            enemyCounter = levelBalancingManager.GetEnemyCount();
            maxEnemies = enemyCounter;
            GameObject smallSkull1 = Instantiate(smallSkullPrefab, new Vector3(325f, 342f, -3), Quaternion.identity);
            smallSkull1.name = "smallSkull1";
            smallSkull1.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

            GameObject smallSkull2 = Instantiate(smallSkullPrefab, new Vector3(555f, 342f, -3), Quaternion.identity);
            smallSkull2.name = "smallSkull2";
            smallSkull2.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

            GameObject bossSkull = Instantiate(bossSkullPrefab, new Vector3(920f, 339f, -3), Quaternion.identity);
            bossSkull.name = "bossSkull";
            bossSkull.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

            StartCoroutine(Level1());
        }
        else{

        }
    }


    private void MeasureIconStep()
    {        
        stepList = new List<float>();
        stepList.Add(0f);
        if (phase == 1)
        {
            xTarget = GameObject.Find("smallSkull1").transform.position.x;
        }
        else if (phase == 2)
        {
            if(GameObject.Find("smallSkull2") == null)
            {
                xTarget = GameObject.Find("bossSkull").transform.position.x;
            }
            else
            {
                xTarget = GameObject.Find("smallSkull2").transform.position.x;
            }          
        }
        else if (phase == 3)
        {
            if (GameObject.Find("smallSkull3") == null)
            {
                xTarget = GameObject.Find("bossSkull").transform.position.x;
            }
            else
            {
                xTarget = GameObject.Find("smallSkull3").transform.position.x;
            }
        }
        else if (phase == 4)
        {
            if (GameObject.Find("smallSkull4") == null)
            {
                xTarget = GameObject.Find("bossSkull").transform.position.x;
            }
            else
            {
                xTarget = GameObject.Find("smallSkull4").transform.position.x;
            }
        }
        else if (phase == 5)
        {
            if (GameObject.Find("smallSkull5") == null)
            {
                xTarget = GameObject.Find("bossSkull").transform.position.x;
            }
            else
            {
                xTarget = GameObject.Find("smallSkull5").transform.position.x;
            }
        }
        else if (phase == 6)
        {
            xTarget = GameObject.Find("bossSkull").transform.position.x;
        }

        xHero = GameObject.Find("heroIcon").transform.position.x;
        xStep = (xTarget - xHero) / enemyCounter; //here also subtract other enemy counters
        for(int i = 0; i < enemyCounter; i++)
        {
            xHero += xStep;
            stepList.Add(xHero);
        }
    }


    private void MoveHeroIcon()
    {
        if (stepList.Count > 1)
        {
            stepList.RemoveAt(0);
        }
        if (!movementInProgress)
        {
            StartCoroutine(MoveIcon());
            movementInProgress = true;
        }
    }


    public float[] GetSpawnRate()
    {
        float[] spawnArray = new float[] {0,0};
        if(level == 1)
        {
            if(phase == 1)
            {
                spawnArray[0] = 2f;
                spawnArray[1] = 0f;
            }
            else if (phase == 2)
            {
                spawnArray[0] = 2f;
                spawnArray[1] = 1f;
            }
            else if (phase == 3)
            {

            }
            return spawnArray;
        }
        else
        {
            return spawnArray;
        }
    }


    public void StartSequence()
    {
        sequenceOngoing = true;
        StartCoroutine(sequenceManager.StartSequence(level, phase));
    }


    public void FinishSequence()
    {
        sequenceOngoing = false;
    }


    private IEnumerator Level1()
    {
        MeasureIconStep();
        while (phase == 1)
        {
            if (enemyCounter == 0)
            {
                //stuff happens
                //next phase starts
                StartSequence();
                while (sequenceOngoing)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                spawnScript.ResetSpawnCounter();
                phase = 2;
            }
            yield return new WaitForSeconds(0.1f);
        }

        levelBalancingManager.SetLevelStats();
        enemyCounter = levelBalancingManager.GetEnemyCount();
        maxEnemies = enemyCounter;
        MeasureIconStep();

        while (phase == 2)
        {
            if (enemyCounter == 0)
            {
                //stuff happens
                //next phase starts
                yield return new WaitForSeconds(2f);
                spawnScript.ResetSpawnCounter();
                phase = 3;
            }
            yield return new WaitForSeconds(0.1f);
        }

        levelBalancingManager.SetLevelStats();
        enemyCounter = levelBalancingManager.GetEnemyCount();
        maxEnemies = enemyCounter;
        MeasureIconStep();

        while (phase == 3)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }


    private IEnumerator MoveIcon()
    {
        while (heroIcon.transform.position.x < stepList[0])
        {
            heroIcon.transform.position += new Vector3(0.5f, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        movementInProgress = false;
    }
}
