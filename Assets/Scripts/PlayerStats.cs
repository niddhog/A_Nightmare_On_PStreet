﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static float rotationSpeed; //min: 1 max: 15
    public static float reloadSpeed; //min: 0.01 max: 0.099
    public static float bulletSpeed; //min: 300 max: 1000
    public static float firingSpeed; //min: 0.5 max: 0.99
    public static float bulletAccuracy; //min: 0.8 max: 1
    public static int magazinSize; //thresholds: 19, 34, 44, 56, 67, 77, 92
    public static float health;
    public static int bulletPower; //How much damage a projectile does
    public static bool GAMEOVER;

    public void Awake()
    {
        rotationSpeed = 2;
        reloadSpeed = 0.09f;
        bulletSpeed = 700;
        firingSpeed = 0.90f;
        bulletAccuracy = 1;
        magazinSize = 92;
        health = 100f;
        bulletPower = 10;
        GAMEOVER = false;
    }

    //can take negative values
    public static void AdjustHealth(float value)
    {
        if(health + value > 100)
        {

        }
        else if (health + value <= 0)
        {
            if (!GAMEOVER)
            {
                Debug.Log("You've been breached: GameOver");
                GameObject.Find("GameHandler").GetComponent<GameOverScript>().CallGameover();
                GAMEOVER = true;
                health = 0;
            }
        }
        else
        {
            health += value;
        }
        RepaintHealth();
    }


    public static IEnumerator ShakeHealth()
    {
        GameObject healthBar = GameObject.Find("HealthBar");
        GameObject healthFrame = GameObject.Find("HealthFrame");
        healthBar.transform.position += new Vector3(0.5f, 0.5f, 0);
        healthFrame.transform.position += new Vector3(0.5f, 0.5f, 0);
        yield return new WaitForSeconds(0.05f);
        healthBar.transform.position += new Vector3(-0.5f, -0.5f, 0);
        healthFrame.transform.position += new Vector3(-0.5f, -0.5f, 0);
        yield return new WaitForSeconds(0.01f);
    }


    private static void RepaintHealth()
    {
        GameObject healthBar = GameObject.Find("HealthBar");
        Vector3 healthVector = new Vector3(98f / 100f * health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        healthBar.transform.localScale = healthVector;
    }
}
