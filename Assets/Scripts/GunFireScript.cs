using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFireScript : MonoBehaviour
{
    Vector2 mousePosition;
    Vector3 moveDir;

    void Start()
    {
        moveDir = FaceMouse.GetDirection(GameObject.Find("BulletTarget").GetComponent<Transform>().position, transform.position).normalized;
        float angle = (Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, PlayerStats.rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, angle);
        Destroy(gameObject, 1.25f);

    }
}
