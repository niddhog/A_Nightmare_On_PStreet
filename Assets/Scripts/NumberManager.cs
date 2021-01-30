using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberManager : MonoBehaviour
{
    public GameObject damageNumberPrefab;
    private NumberManager numberManager;
    private void Awake()
    {
        numberManager = GameObject.Find("GameHandler").GetComponent<NumberManager>();
    }


    public void SpawnDamageNumber(Vector3 target, float incomingDamage)
    {
        target -= new Vector3(20, 0, 0);
        GameObject damageNumber = Instantiate(damageNumberPrefab, target, Quaternion.identity);
        damageNumber.name = "DNumber";
        damageNumber.transform.SetParent(GameObject.Find("DamageNumber").GetComponent<Transform>(), false);
        damageNumber.GetComponent<MoveDamageNumber>().SetDamageText(incomingDamage);
    }
}
