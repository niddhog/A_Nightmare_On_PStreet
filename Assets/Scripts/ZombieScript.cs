using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    private BloodManager blood;

    private int health;
    private float speed;
    private float luck;


    void Start()
    {
        blood = GameObject.Find("GameHandler").GetComponent<BloodManager>();
        health = 10;
        speed = 50f;
    }


    void Update()
    {
        if(health <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("Dead", true);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        blood.SpawnBlood(collision.gameObject);
        Destroy(collision.gameObject);
        health -= PlayerStats.bulletPower;
    }


    public float GetHealth()
    {
        return health;
    }


    public float GetSpeed()
    {
        return speed;
    }


    public void SetHealth(int h)
    {
        health = h;
    }


    public void SetSpeed(float s)
    {
        speed = s;
    }
}
