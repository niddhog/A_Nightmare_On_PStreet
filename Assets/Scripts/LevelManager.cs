using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton
public class LevelManager : MonoBehaviour
{
    private int level;
    public GameObject heroPrefab;
    public GameObject smallSkullPrefab;
    public GameObject bossSkullPrefab;

    private GameObject heroIcon;
    private int zombieCounter;
    private int phase;
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
        stepList = new List<float>();
        levelInProgress = false;
        movementInProgress = false;
        level = 1;
        phase = 1;
        zombieCounter = 1;
        progression = GameObject.Find("GameHandler").GetComponent<GameProgressionManager>();
    }


    public int GetLevel()
    {
        return level;
    }


    public void SetLevel(int l)
    {
        level = l;
    }


    public void ReduceZombie()
    {
        zombieCounter -= 1;
        if(zombieCounter < 0)
        {
            zombieCounter = 0;
        }
        Debug.Log("Z Count: " + zombieCounter);
        MoveHeroIcon();
    }


    public int GetEnemyCounter()
    {
        return maxEnemies;
    }


    public void SetProgressUI()
    {
        heroIcon = Instantiate(heroPrefab, new Vector3(50f, 340f, -3), Quaternion.identity);
        heroIcon.name = "heroIcon";
        heroIcon.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        phase = 1;
        if (level == 1)
        {
            zombieCounter = 15;
            maxEnemies = zombieCounter;
            GameObject smallSkull1 = Instantiate(smallSkullPrefab, new Vector3(325f, 342f, -3), Quaternion.identity);
            smallSkull1.name = "smallSkull1";
            smallSkull1.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

            GameObject smallSkull2 = Instantiate(smallSkullPrefab, new Vector3(555f, 342f, -3), Quaternion.identity);
            smallSkull2.name = "smallSkull2";
            smallSkull2.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

            StartCoroutine(Level1());
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
            xTarget = GameObject.Find("smallSkull2").transform.position.x;
        }
        xHero = GameObject.Find("heroIcon").transform.position.x;
        xStep = (xTarget - xHero) / zombieCounter; //here also subtract other enemy counters
        for(int i = 0; i < zombieCounter; i++)
        {
            xHero += xStep;
            stepList.Add(xHero);
        }
    }


    private void MoveHeroIcon()
    {
        if(phase == 1)
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
    }


    private IEnumerator Level1()
    {
        MeasureIconStep();
        while (phase == 1)
        {
            if (zombieCounter == 0)
            {
                //stuff happens
                //next phase starts
                zombieCounter = 15;
                maxEnemies = zombieCounter;
                phase = 2;
            }
            Debug.Log("Level: " + level + " Phase: " + phase);
            yield return new WaitForSeconds(0.1f);
        }
        while (phase == 2)
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Level: " + level + " Phase: " + phase);
        }
        while (phase == 3)
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log("Level: " + level + " Phase: " + phase);
        }
    }


    private IEnumerator MoveIcon()
    {
        while (heroIcon.transform.position.x < stepList[0])
        {
            heroIcon.transform.position += new Vector3(0.5f, 0, 0);
            yield return new WaitForSeconds(0.1f);
        }
        movementInProgress = false;
    }
}
