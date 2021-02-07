using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Instantiation_CAE: MonoBehaviour {

    //instantiate a new GameObject
    public static GameObject Instantiation(GameObject gObject, Vector3 spawnLocation, Quaternion quaternion, string name, String parents)
    {
        GameObject returnObject = Instantiate(gObject, spawnLocation, quaternion);
        returnObject.name = name;
        returnObject.transform.SetParent(GameObject.Find(parents).GetComponent<Transform>());
        return returnObject;
    }

    //Set Bool Values of Animators
    public static void SetAnimatorBool(GameObject gameObject, string boolName, bool boolValue)
    {
        try
        {
            gameObject.GetComponent<Animator>().SetBool(boolName, boolValue);
        }
        catch (NullReferenceException e)
        {
            print("Tried to set a Animator Bool value of a gameObject that does not exist! Did not change anything in the animator");
            print(e);
        }
    }
}
