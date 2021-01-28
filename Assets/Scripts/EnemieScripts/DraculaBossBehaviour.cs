using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraculaBossBehaviour : MonoBehaviour
{
    private int health;
    private int speed;
    private bool startBoss;
    private Animator draculaAnimator;
    private BoxCollider2D draculaHitBox;


    void Awake()
    {
        health = 100;
        speed = 10;
        startBoss = false;
        draculaHitBox = GetComponent<BoxCollider2D>();
        draculaHitBox.enabled = false;
        draculaAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        if (startBoss)
        {
            draculaHitBox.enabled = true;
            draculaAnimator.SetBool("spawnbats", true);
            startBoss = false;
        }
    }


    public void StartDraculaBossFight()
    {
        startBoss = true;
    }




}
