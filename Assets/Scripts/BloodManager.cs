using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour
{
    public GameObject bloodPrefab;

    public void SpawnBlood(GameObject target)
    {
        Debug.Log(bloodPrefab);
        GameObject blood = Instantiate(bloodPrefab, target.transform.position, Quaternion.identity);
        blood.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
    }
}
