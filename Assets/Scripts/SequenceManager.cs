using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public GameObject darkMagicPrefab;

    private LevelManager levelManager;
    private PauseScript pause;

    void Start()
    {
        levelManager = GameObject.Find("GameHandler").GetComponent<LevelManager>();
        pause = GameObject.Find("GameHandler").GetComponent<PauseScript>();
    }


    public IEnumerator StartSequence(int level, int phase)
    {
        pause.PauseGame();
        if (level == 1)
        {
            if (phase == 1)
            {
                yield return new WaitForSeconds(0.5f);
                GameObject magic1 = Instantiate(darkMagicPrefab, new Vector3(-180,-45,70), Quaternion.identity);
                magic1.name = "magic1";
                magic1.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(),false);
                yield return new WaitForSeconds(0.5f);
                GameObject magic2 = Instantiate(darkMagicPrefab, new Vector3(-171, -37, 70), Quaternion.identity);
                magic2.name = "magic2";
                magic2.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                yield return new WaitForSeconds(0.5f);
                GameObject magic3 = Instantiate(darkMagicPrefab, new Vector3(-183, -62, 70), Quaternion.identity);
                magic3.name = "magic2";
                magic3.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
                yield return new WaitForSeconds(0.5f);
            }
            else if (phase == 2)
            {

            }
            else if (phase == 3)
            {

            }
        }
        yield return new WaitForSeconds(3f);
        pause.UnpauseGame();
        levelManager.FinishSequence();
    }
}
