using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{

    private bool staggered;
    private BloodManager blood;
    private DamageManager damage;
    private Animator animator;
    private bool wait;
    private bool attackInMotion;
    private float health;
    private float speed;
    private float attackPower;
    private float attackSpeed;
    private AudioManager audioManager;
    private LevelManager levelManager;
    private NumberManager numberManager;
    private bool dead;
    private float Stagerresi;


    void Start()
    {
        Stagerresi = 0;
        health = 28f;
        attackSpeed = 1; //Range [1,2]
        speed = Random.Range(20f, 50f);
        attackPower = Random.Range(1, 2);

        levelManager = GameObject.Find("GameHandler").GetComponent<LevelManager>();
        audioManager = GameObject.Find("GameHandler").GetComponent<AudioManager>();
        wait = false;
        attackInMotion = false;
        animator = gameObject.GetComponent<Animator>();
        blood = GameObject.Find("GameHandler").GetComponent<BloodManager>();
        damage = GameObject.Find("GameHandler").GetComponent<DamageManager>();
        numberManager = GameObject.Find("GameHandler").GetComponent<NumberManager>();
        dead = false;
        staggered = false;
    }


    void Update()
    {
        if (PlayerStats.GAMEOVER)
        {

        }
        else
        {
            if (dead)
            {

            }
            else if (health <= 0)
            {
                levelManager.ReduceEnemy();
                gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                animator.SetBool("Dead", true);
                dead = true;
            }

            if (animator.GetBool("hasSpawned") && !(animator.GetBool("Dead")) && !(animator.GetBool("Attack")) && !wait)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Barbwire")
        {
            animator.SetBool("Attack", true);
        }
        else if (collision.gameObject.name == "z" && wait == false)
        {
            if(collision.gameObject.transform.position.x >= transform.position.x)
            {
                wait = true;
            }
        }
        else if (collision.gameObject.name == "Bullet")
        {
            if (!staggered)
            {
                StartCoroutine(Staggering());
                staggered = true;
            }
            audioManager.hit01.Play();
            blood.SpawnBlood(collision.gameObject);
            Destroy(collision.gameObject);
            float incomingDamage = PlayerStats.GetBulletPower();
            incomingDamage = Mathf.Round(incomingDamage);
            numberManager.SpawnDamageNumber(transform.position, incomingDamage);
            health -= incomingDamage;
        }   
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "z")
        {
            if (collision.gameObject.transform.position.x >= transform.position.x)
            {
                wait = false;
            }
        }   
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PlayerStats.GAMEOVER)
        {
            
        }
        else
        {
            if (collision.gameObject.name == "Barbwire")
            {
                if (!attackInMotion)
                {
                    StartCoroutine(Attack());
                    attackInMotion = true;
                }
            }
            else if (collision.gameObject.name == "z")
            {
                if (collision.gameObject.transform.position.x >= transform.position.x && collision.GetComponent<Animator>().GetBool("Dead"))
                {
                    wait = false;
                }
            }
        }
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


    private IEnumerator Attack()
    {
        StartCoroutine(PlayerStats.ShakeHealth());
        GameObject.Find("Barbwire").GetComponent<Animator>().SetBool("damageWire", true);
        yield return new WaitForSeconds(0.01f);
        GameObject.Find("Barbwire").GetComponent<Animator>().SetBool("damageWire", false);
        audioManager.wireDamage.Play();
        damage.SpawnDamage(gameObject);
        PlayerStats.AdjustHealth(-attackPower);
        yield return new WaitForSeconds(2 - attackSpeed);
        attackInMotion = false;
    }

    private IEnumerator Staggering()
    {
        float staggerDelta = PlayerStats.staggerPower - Stagerresi;
        speed /= Mathf.Clamp(staggerDelta, 1f, 100);
        yield return new WaitForSeconds(Mathf.Clamp(PlayerStats.staggerDuration - Stagerresi, 0, 100));
        speed *= Mathf.Clamp(staggerDelta, 1f, 100); ;
        staggered = false;
    }
}
