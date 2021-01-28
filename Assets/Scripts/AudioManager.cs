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
    public AudioSource batDies;
    public AudioSource powerUp1;
    public AudioSource powerUp2;
    public AudioSource confrim;
    public AudioSource move;
    public AudioSource spinAmbient;
    public AudioSource Level1Music;
    public AudioSource countDracula01;
    public AudioSource countDraculaEndYou;
    public AudioSource countDraculaTruePower;

    
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
        batDies.volume = 0.75f;
        powerUp1.volume = 0.75f;
        powerUp2.volume = 0.75f;
        confrim.volume = 0.75f;
        move.volume = 0.75f;
        spinAmbient.volume = 0.75f;
        Level1Music.volume = 0.5f;
        countDracula01.volume = 1.5f;
}


    public void GameOver()
    {
        gameOverZombie.Play();
        gameOverMusic.Play();
    }


    //Fades out the desired track in n seconds
    public void FadeOut(AudioSource audioTrack, float seconds, float lowerBound = 0)
    {
        StartCoroutine(FadeOutThread(audioTrack,seconds,lowerBound));
    }


    private IEnumerator FadeOutThread(AudioSource audioTrack, float seconds, float lowerBound)
    {
        float originalVolume = audioTrack.volume;
        float step = originalVolume / (seconds * 100);
        float looper = 0;

        while (looper < seconds)
        {
            if((audioTrack.volume > lowerBound))
            {
                audioTrack.volume -= step;
            }
            yield return new WaitForSeconds(0.01f);
            looper += 0.01f;
        }
        //audioTrack.Stop();
        //audioTrack.volume = originalVolume;
    }



    //Fades in the desired track in n seconds
    public void FadeIn(AudioSource audioTrack, float seconds, float ceiling = 0.75f)
    {
        StartCoroutine(FadeInThread(audioTrack, seconds, ceiling));
    }


    private IEnumerator FadeInThread(AudioSource audioTrack, float seconds, float ceiling)
    {
        float originalVolume = audioTrack.volume;
        audioTrack.volume = 0;
        audioTrack.Play();

        float looper = 0;
        float step = originalVolume/(seconds*100);
        while (looper < seconds)
        {
            if (audioTrack.volume < ceiling)
            {
                audioTrack.volume += step;
            }
            yield return new WaitForSeconds(0.01f);
            looper += 0.01f;
        }
        //audioTrack.volume = originalVolume;
    }


    //Increases Sound Over Time
    public void IncreaseVolumeOverTime(AudioSource audioTrack, float seconds, float ceiling = 0.75f)
    {
        StartCoroutine(IncreaseVolumeOverTimeThread(audioTrack, seconds, ceiling));
    }


    private IEnumerator IncreaseVolumeOverTimeThread(AudioSource audioTrack, float seconds, float ceiling)
    {
        float originalVolume = audioTrack.volume;
        float looper = 0;
        float step = originalVolume / (seconds * 100);

        while (looper < seconds)
        {
            if (audioTrack.volume < ceiling)
            {
                audioTrack.volume += step;
            }
            yield return new WaitForSeconds(0.01f);
            looper += 0.01f;
        }
    }
}
