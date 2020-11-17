using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazinUI : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite M00;
    public Sprite M01;
    public Sprite M02;
    public Sprite M03;
    public Sprite M04;
    public Sprite M05;
    public Sprite M06;

    private void Update()
    {
        if(PlayerStats.magazinSize <= 19)
        {
            spriteRenderer.sprite = M00;
        }
        else if (PlayerStats.magazinSize <= 34)
        {
            spriteRenderer.sprite = M01;
        }
        else if (PlayerStats.magazinSize <= 44)
        {
            spriteRenderer.sprite = M02;
        }
        else if (PlayerStats.magazinSize <= 56)
        {
            spriteRenderer.sprite = M03;
        }
        else if (PlayerStats.magazinSize <= 67)
        {
            spriteRenderer.sprite = M04;
        }
        else if (PlayerStats.magazinSize <= 77)
        {
            spriteRenderer.sprite = M05;
        }
        else if (PlayerStats.magazinSize <= 92)
        {
            spriteRenderer.sprite = M06;
        }
    }
}


