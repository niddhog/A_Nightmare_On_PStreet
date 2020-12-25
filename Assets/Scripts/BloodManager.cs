using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodManager : MonoBehaviour
{
    public GameObject bloodPrefab;

    public void SpawnBlood(GameObject target)
    {
        Vector3 bloodVector = target.transform.position;
        bloodVector.z = -10f;
        GameObject blood = Instantiate(bloodPrefab, bloodVector, Quaternion.identity);
        blood.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>());
    }
}
