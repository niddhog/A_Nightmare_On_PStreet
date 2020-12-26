using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverPrefab;
    public GameObject gameOverText;

    private PlayerController pCon;
    private FaceMouse fMouse;
    private EnemySpawnScript eSpawn;
    private ShellParticles sPar;
    private ParticleSystem sParS;
    private AmmoHandler aHan;
    private AudioManager aMan;

    private void Awake()
    {
        pCon = GameObject.Find("Player_s").GetComponent<PlayerController>();
        fMouse = GameObject.Find("Player_s").GetComponent<FaceMouse>();
        eSpawn = GameObject.Find("GameHandler").GetComponent<EnemySpawnScript>();
        sPar = GameObject.Find("ShellPivot").GetComponent<ShellParticles>();
        sParS = GameObject.Find("ShellParticleSystem").GetComponent<ParticleSystem>();
        aHan = GameObject.Find("GameHandler").GetComponent<AmmoHandler>();
        aMan = GameObject.Find("GameHandler").GetComponent<AudioManager>();
    }

    public void GameOver()
    {
        foreach (Transform child in GameObject.Find("Canvas").transform)
        {
            child.gameObject.SetActive(false);
        }

        GameObject end = Instantiate(gameOverPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        end.name = "GameOver";
        end.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
        end.transform.localScale = new Vector3(20, 20, 20);

        GameObject gameoverText = Instantiate(gameOverText, gameOverText.transform.position, Quaternion.identity);
        gameoverText.name = "GameOverText";
        gameoverText.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }


    public void CallGameover()
    {
        ShutDownScripts();
        StartCoroutine(GameOverCoroutine());
    }


    public IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        GameOver();
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
        }
        
    }


    private void ShutDownScripts()
    {
        //StopCoroutine(pCon.gunCoroutine);
        pCon.aboart = true;
        pCon.enabled = false;
        fMouse.enabled = false;
        eSpawn.enabled = false;
        sPar.enabled = false;
        aHan.enabled = false;
        sParS.enableEmission = false;
        foreach(Transform child in GameObject.Find("AudioSources").transform)
        {
            child.gameObject.GetComponent<AudioSource>().Stop();
        }
        aMan.GameOver();
    }
}
