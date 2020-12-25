using System.Collections;
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
    public static int health;

    public void Awake()
    {
        rotationSpeed = 2;
        reloadSpeed = 0.09f;
        bulletSpeed = 500;
        firingSpeed = 0.60f;
        bulletAccuracy = 1;
        magazinSize = 92;
        health = 100;
    }

}
