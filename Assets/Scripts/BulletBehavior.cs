using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    Vector3 moveDir;
    
    void Start()
    {
        Physics2D.IgnoreCollision(transform.GetComponent<BoxCollider2D>(), GameObject.Find("Barbwire").GetComponent<PolygonCollider2D>());
        moveDir = FaceMouse.GetDirection(GameObject.Find("BulletTarget").GetComponent<Transform>().position, transform.position).normalized;
        moveDir.z = 0f;
        moveDir.y += Random.Range(-(1.0f - PlayerStats.bulletAccuracy), 1.0f - PlayerStats.bulletAccuracy);

        float angle = (Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg); //Atan2 returns Radiants, we need to convert it to Degree
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, PlayerStats.rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle);
        Destroy(gameObject, 5f);
    }

    
    void Update()
    {
        transform.position += moveDir * PlayerStats.bulletSpeed * Time.deltaTime;
    }
}
