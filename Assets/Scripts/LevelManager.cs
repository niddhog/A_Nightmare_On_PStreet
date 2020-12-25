using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton
public class LevelManager : MonoBehaviour
{
    [SerializeField] private int level;
    private static LevelManager _instance;


    private void Awake()
    {
        level = 1;
    }


    public int GetLevel()
    {
        return level;
    }


    public void SetLevel(int l)
    {
        level = l;
    }
}
