using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour
{
    public static GameObject bloodPrefab;

    public static void SpawnBlood(GameObject target)
    {
        GameObject blood = Instantiate(bloodPrefab, target.GetComponent<Transform>().position, Quaternion.identity);
        blood.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
    }
}
