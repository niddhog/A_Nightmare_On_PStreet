﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressionManager : MonoBehaviour
{
    public GameObject arrowPrefab;
    private LevelManager levelManager;
    private EnvironmentManager environmentManager;

    void Start()
    {
        levelManager = GameObject.Find("GameHandler").GetComponent<LevelManager>();
        environmentManager = GameObject.Find("GameHandler").GetComponent<EnvironmentManager>();
        StartCoroutine(SetupArrows());
        StartCoroutine(WarmUpGame());
    }


    private IEnumerator SetupArrows()
    {
        float x = 79.8f;
        for (int i = 0; i<43; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, new Vector3(x, 342f, -2), Quaternion.identity);
            arrow.name = "arrow";
            arrow.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
            yield return new WaitForSeconds(0.1f);
            x += 20f;
        } 
    }


    private IEnumerator WarmUpGame()
    {
        environmentManager.SetFog(0.25f);
        yield return new WaitForSeconds(0.1f);
        levelManager.StartGameFlow();
    }
}
