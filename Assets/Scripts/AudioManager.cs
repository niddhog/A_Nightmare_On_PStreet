using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource mgStart;
    public AudioSource mgRunning;
    public AudioSource mgEnd;
    public AudioSource normalShot;
    public AudioSource emptyShot;
    public AudioSource gReload;
    public AudioSource gFull;
    public AudioSource mLoadingDone;
    public AudioSource hit01;
    public AudioSource hit02;
    public AudioSource hit03;
    public AudioSource zombies;
    public AudioSource wireDamage;
    public AudioSource gameOverZombie;
    public AudioSource gameOverMusic;
    public AudioSource sirene;
    public AudioSource darkMagic01;
    public AudioSource draculaLaugh;
    public AudioSource batsFlying;


    public void Awake()
    {
        mgRunning.volume = 0.25f;
        mgStart.volume = 0.25f;
        mgEnd.volume = 0.25f;
        normalShot.volume = 0.25f;
        emptyShot.volume = 0.25f;
        gReload.volume = 0.5f;
        gFull.volume = 0.5f;
        mLoadingDone.volume = 0.75f;
        hit01.volume = 0.5f;
        hit02.volume = 0.25f;
        hit03.volume = 0.5f;
        zombies.volume = 0.75f;
        wireDamage.volume = 0.5f;
        gameOverZombie.volume = 0.5f;
        gameOverMusic.volume = 0.5f;
        sirene.volume = 0.75f;
        darkMagic01.volume = 0.75f;
        draculaLaugh.volume = 0.75f;
    }


    public void GameOver()
    {
        gameOverZombie.Play();
        gameOverMusic.Play();
    }
}
