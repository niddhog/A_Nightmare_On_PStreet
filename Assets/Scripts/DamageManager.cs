using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public GameObject damagePrefab;

    public void SpawnDamage(GameObject target)
    {
        Vector3 damageVector = target.transform.position;
        damageVector.z = -10f;
        damageVector.x += 10f;
        GameObject damage = Instantiate(damagePrefab, damageVector, Quaternion.identity);
        damage.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
    }
}
