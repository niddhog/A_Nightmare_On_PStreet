using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    Vector2 mousePosition;
    Vector3 moveDir;
    
    void Start()
    {
        mousePosition = Vector2.zero;
        moveDir = FaceMouse.GetDirection(GameObject.Find("BulletTarget").GetComponent<Transform>().position, transform.position).normalized;
        moveDir.z = 0f;
        if(Random.Range(0,2) == 0)
        {
            moveDir.y += Random.Range(0.0f, 1.0f - PlayerStats.bulletAccuracy);
        }
        else
        {
            moveDir.y -= Random.Range(0.0f, 1.0f - PlayerStats.bulletAccuracy);
        }

        float angle = (Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg);
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
