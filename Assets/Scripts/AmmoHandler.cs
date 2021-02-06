using System.Collections;
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
    private bool pause;


    public void Awake()
    {
        controls = new PlayerControls();
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>(); ;
        controls.Basic.Reload.performed += reloadMapping => Reload();
    }


    void Start()
    {
        pause = false;
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
        if(pause)
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
                GameObject.Find("Player_s").GetComponent<PlayerController>().shooting = false;
                StartCoroutine(reloadingNumerator);
                StartCoroutine(fillMagazin);
                reloadingNumerator = Reloading();
                fillMagazin = FillMagazin();
                isReloading = true;
            }
        }
    }


    public void Pause()
    {
        pause = true;
    }


    public void UnPause()
    {
        pause = false;
    }


    #region Coroutines

    IEnumerator Reloading()
    {
        bool terminate = false;
        while (bulletQueue.Count < magazinSize)
        {
            audioManager.gReload.Play();
            float count = 0;

            while (count < 1)
            {
                if (bulletQueue.Count < magazinSize)
                {
                    terminate = true;
                    break;
                }
                yield return new WaitForSeconds(0.01f);
                count += 0.01f;
            }
            if (terminate) { break; }
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
            try
            {
                bulletQueue[i].name = "Bullet" + i.ToString();
                bulletQueue[i].transform.SetParent(GameObject.Find("Magazin").transform);
                bulletQueue[i].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                bulletQueue[i].GetComponent<RectTransform>().localPosition = new Vector3(x, 0, -3);
            }
            catch(System.IndexOutOfRangeException ex)
            {
                Debug.Log(ex);
            }
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
