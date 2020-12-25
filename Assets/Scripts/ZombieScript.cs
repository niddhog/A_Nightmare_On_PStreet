using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    private float health;
    private float speed;
    private float luck;


    void Start()
    {
        health = 100f;
        speed = 50f;
    }


    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }


    public float GetHealth()
    {
        return health;
    }


    public float GetSpeed()
    {
        return speed;
    }


    public void SetHealth(float h)
    {
        health = h;
    }


    public void SetSpeed(float s)
    {
        health = s;
    }
}
