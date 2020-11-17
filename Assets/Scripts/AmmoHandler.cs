﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoHandler : MonoBehaviour
{
    public GameObject ammoUI;
    public static List<GameObject> bulletQueue;
    [HideInInspector]
    public bool isReloading;

    private AudioManager audioManager;
    private int magazinSize;
    private PlayerControls controls;
    private bool isFull;
    private IEnumerator reloadingNumerator;
    private IEnumerator fillMagazin;


    public void Awake()
    {
        controls = new PlayerControls();
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>(); ;
        controls.Basic.Reload.performed += reloadMapping => Reload();
    }


    void Start()
    {
        reloadingNumerator = Reloading();
        fillMagazin = FillMagazin();
        isReloading = false;
        isFull = true;
        bulletQueue = new List<GameObject>();
        magazinSize = PlayerStats.magazinSize;
        
        int i = 0;
        float x = 0.02f;
        while (i < magazinSize)
        {
            bulletQueue.Add(Instantiate(ammoUI, GameObject.Find("Magazin").GetComponent<RectTransform>().position, Quaternion.identity));
            bulletQueue[i].name = "Bullet" + i.ToString();
            bulletQueue[i].transform.SetParent(GameObject.Find("Magazin").transform);
            bulletQueue[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            bulletQueue[i].GetComponent<RectTransform>().localPosition = new Vector3(x, 0, -3);
            x += 0.07f;
            i++;
          
        }
    }
    

    public static void PopBullet()
    {
        if(!CheckIfEmpty())
        {
            Destroy(bulletQueue[bulletQueue.Count - 1]);
            bulletQueue.Remove(bulletQueue[bulletQueue.Count - 1]);
        }
        else
        {

        }
    }


    public static bool CheckIfEmpty()
    {
        if (bulletQueue.Count != 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    void Reload()
    {
        if(GameObject.Find("Player_s").GetComponent<PlayerController>().shooting == true)
        {

        }
        else
        {
            if (bulletQueue.Count == magazinSize)
            {
                if (isFull)
                {
                    StartCoroutine(GfullAudio());
                    isFull = false;
                }
                else
                {

                }
            }
            else if (isReloading)
            {

            }
            else
            {
                StartCoroutine(reloadingNumerator);
                StartCoroutine(fillMagazin);
                reloadingNumerator = Reloading();
                fillMagazin = FillMagazin();
                isReloading = true;
            }
        }
    }


    #region Coroutines

    IEnumerator Reloading()
    {
        while (bulletQueue.Count < magazinSize)
        {
            audioManager.gReload.Play();
            yield return new WaitForSeconds(1);
        }
    }


    IEnumerator FillMagazin()
    {
        int i = bulletQueue.Count;
        float x;
        if (CheckIfEmpty())
        {
            x = 0.02f;
        }
        else
        {
            x = bulletQueue[bulletQueue.Count - 1].transform.localPosition.x + 0.07f;
        }

        while (i < magazinSize)
        {
            bulletQueue.Add(Instantiate(ammoUI, GameObject.Find("Magazin").GetComponent<RectTransform>().position, Quaternion.identity));
            bulletQueue[i].name = "Bullet" + i.ToString();
            bulletQueue[i].transform.SetParent(GameObject.Find("Magazin").transform);
            bulletQueue[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            bulletQueue[i].GetComponent<RectTransform>().localPosition = new Vector3(x, 0, -3);
            x += 0.07f;
            i++;
            yield return new WaitForSeconds(0.1f - PlayerStats.reloadSpeed);
        }
        audioManager.gReload.Stop();
        audioManager.mLoadingDone.Play();
        isReloading = false;
    }


    IEnumerator GfullAudio()
    {
        audioManager.gFull.Play();
        yield return new WaitForSeconds(0.5f);
        isFull = true;
    }

    #endregion


    #region Enable/Disable
    private void OnEnable()
    {
        controls.Enable();
    }


    private void OnDisable()
    {
        controls.Disable();
    }

    #endregion
}
