using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    private BloodManager blood;

    private float health;
    private float speed;
    private float luck;


    void Start()
    {
        blood = GameObject.Find("GameHandler").GetComponent<BloodManager>();
        health = 100f;
        speed = 50f;
    }


    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        blood.SpawnBlood(collision.gameObject);
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
