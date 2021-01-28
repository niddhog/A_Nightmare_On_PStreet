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
}

public class Health
{
    public const int HEALTH_MAX = 100;

    private float healthAmount;
    private float healthRegenAmount;

    public Health()
    {
        healthAmount = 0;
        healthRegenAmount = 25f;
    }

    public void Update()
    {
        healthAmount += healthRegenAmount * Time.deltaTime;
        healthAmount = Mathf.Clamp(healthAmount, 0f, HEALTH_MAX);
        if(healthAmount == HEALTH_MAX)
        {
            healthRegenAmount = 0;
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
        return healthAmount / HEALTH_MAX;
    }


    public void SetRegen(float value)
    {
        healthRegenAmount = value;
    }
}
