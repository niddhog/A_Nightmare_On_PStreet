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
    }
}
