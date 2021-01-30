using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject gFirePrefab;
    [HideInInspector]
    public bool shooting;
    public bool aboart;


    private Vector2 mousePosition;
    private PlayerControls controls;
    private AudioManager audioManager;
    private IEnumerator gunCoroutine;
    private bool gunCStarted;
    private bool bulletBlock;
    private bool warmUp;
    private CameraController cameraController;
    private bool pause;


    private void Awake()
    {
        pause = false;
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>();
        controls = new PlayerControls();
        cameraController = new CameraController();
        controls.Basic.Shoot.performed += shootMapping => shooting = true;
        controls.Basic.Shoot.canceled += shootMapping => shooting = false;
    }


    void Start()
    {
        mousePosition = Vector2.zero;
        gunCoroutine = GattlingRunning();
        gunCStarted = false;
        bulletBlock = false;
        warmUp = true;
        aboart = false;
    }


    void Update()
    {
        if (PlayerStats.GAMEOVER || pause)
        {

        }
        else
        {
            mousePosition = FaceMouse.mousePosition;

            if (shooting)
            {
                if (GameObject.Find("GameHandler").GetComponent<AmmoHandler>().isReloading == true)
                {

                }
                else
                {
                    if (!gunCStarted)
                    {
                        if (!AmmoHandler.CheckIfEmpty())
                        {
                            ShellParticles.StartShellEmission();
                        }
                        audioManager.mgStart.Play();
                        StartCoroutine(gunCoroutine);
                        gunCStarted = true;
                    }
                    Shoot();
                }
            }
            else
            {
                if (gunCStarted)
                {
                    ShellParticles.StopShellEmission();
                    audioManager.mgEnd.Play();
                    audioManager.mgRunning.Stop();
                    warmUp = true;
                }

                StopCoroutine(gunCoroutine);
                gunCoroutine = GattlingRunning();
                gunCStarted = false;
            }
        }
    }


    void Shoot()
    {
        if (!bulletBlock)
        {
            bulletBlock = true;
            StartCoroutine(BulletBlockTime());
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
    IEnumerator GattlingRunning()
    {
        while (true)
        {
            if (aboart)
            {
                break;
            }
            if (!audioManager.mgRunning.isPlaying)
            {
                audioManager.mgRunning.Play();
            }
            yield return new WaitForSeconds(0.01f);
        }
    }


    IEnumerator BulletBlockTime()
    {
        if (warmUp)
        {
            yield return new WaitForSeconds(PlayerStats.warmUpTime);
            warmUp = false;
        }
        if (AmmoHandler.CheckIfEmpty())
        {
            ShellParticles.StopShellEmission();
            audioManager.emptyShot.Play();
        }
        else
        {
            GameObject fire = Instantiate(gFirePrefab, GameObject.Find("GunTipp").GetComponent<Transform>().position, Quaternion.identity);
            fire.name = "gFire";
            fire.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());

            GameObject bullet = Instantiate(bulletPrefab, GameObject.Find("BulletContainer").transform.position, Quaternion.identity);
            bullet.name = "Bullet";
            bullet.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
            StartCoroutine(cameraController.ShakeCamera());
            AmmoHandler.PopBullet();
            audioManager.normalShot.Play();
            GameObject.Find("Player_s").GetComponent<Animator>().SetBool("fire", true);
        }
        yield return new WaitForSeconds(1f - PlayerStats.firingSpeed);
        bulletBlock = false;
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
