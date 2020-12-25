using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int level;
    void Start()
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
