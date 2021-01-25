using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private FaceMouse fMouse;
    private EnemySpawnScript spawnScript;
    private PlayerController pController;
    private AmmoHandler ammoScript;
    private AudioManager audioManager;

    void Start()
    {
        fMouse = GameObject.Find("Player_s").GetComponent<FaceMouse>();
        pController = GameObject.Find("Player_s").GetComponent<PlayerController>();
        spawnScript = GameObject.Find("GameHandler").GetComponent<EnemySpawnScript>();
        ammoScript = GameObject.Find("GameHandler").GetComponent<AmmoHandler>();
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>();
    }

    public void PauseGame()
    {
        fMouse.Pause();
        spawnScript.Pause();
        pController.Pause();
        pController.aboart = true;
        ammoScript.Pause();
        ShellParticles.StopShellEmission();
        audioManager.mgRunning.Stop();
    }

    public void UnpauseGame()
    {
        fMouse.UnPause();
        spawnScript.UnPause();
        pController.UnPause();
        ammoScript.UnPause();
    }
}
