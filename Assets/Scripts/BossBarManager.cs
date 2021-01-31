using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBarManager : MonoBehaviour
{
    private Health health;
    private RectTransform barMaskRectTransform;
    private RawImage barRawImage;
    private float barMaskwidth;

    private void Awake()
    {
        barMaskRectTransform = GameObject.Find("barMask").GetComponent<RectTransform>();
        barRawImage = GameObject.Find("bar").GetComponent<RawImage>();
        health = new Health();
        barMaskwidth = barMaskRectTransform.sizeDelta.x;
    }

    private void Update()
    {
        health.Update();
        Rect uvRect = barRawImage.uvRect;
        uvRect.x -= 1f * Time.deltaTime;
        barRawImage.uvRect = uvRect;

        Vector2 barMaskSizeDelta = barMaskRectTransform.sizeDelta;
        barMaskSizeDelta.x = health.GetHealthNormalized() * barMaskwidth;
        barMaskRectTransform.sizeDelta = barMaskSizeDelta;
    }

    public void SetRegen(float value)
    {
        health.SetRegen(value);
    }

    public void Damage(float value)
    {
        health.ReduceHealth(value);
    }

    public void SetHealthMax(int value)
    {
        health.SetMaxHealth(value);
    }


    public float GetHealth()
    {
        return health.GetHealth();
    }

    public void FillUpHealth(int maxHealt, int Regen)
    {
        health.SetMaxHealth(maxHealt);
        health.SetRegen(Regen);
        health.FillUpBar();
    }
}

public class Health
{
    //public const int HEALTH_MAX = 100;
    public int health_max = 100;

    private bool fillUpBar;

    private float healthAmount;
    private float healthRegenAmount;

    public Health()
    {
        healthAmount = 1;
        healthRegenAmount = 30f;
        fillUpBar = false;
    }

    public void Update()
    {
        if (fillUpBar)
        {
            healthAmount += healthRegenAmount * Time.deltaTime;
            healthAmount = Mathf.Clamp(healthAmount, 0f, health_max);
            if (healthAmount == health_max)
            {
                healthRegenAmount = 0;
                fillUpBar = false;
            }
        }
    }

    public void ReduceHealth(float amount)
    {
        healthAmount -= amount;
        if(healthAmount < 0)
        {
            healthAmount = 0;
        }
    }


    public float GetHealthNormalized()
    {
        return healthAmount / health_max;
    }


    public void SetRegen(float value)
    {
        healthRegenAmount = value;
    }

    public void SetMaxHealth(int value)
    {
        health_max = value;
    }


    public float GetHealth()
    {
        return healthAmount;
    }

    public void FillUpBar()
    {
        fillUpBar = true;
    }
}
