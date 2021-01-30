using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveDamageNumber : MonoBehaviour
{
    bool fading;
    float xOffset;
    float xModifier;

    float yOffset;
    float yModifier;

    float timer;

    void Start()
    {
        fading = false;
        xOffset = 70;
        xModifier = 120;
        yOffset = 14;
        yModifier = 4;
        timer = 1.2f;
    }


    void Update()
    {
        Destroy(gameObject,timer);
        if (!fading)
        {
            transform.GetComponent<TextMeshProUGUI>().CrossFadeColor(new Color32(255, 93, 93, 0), timer, false, true);
            fading = true;
        }
        transform.position -= new Vector3(xOffset * Time.deltaTime, -(yOffset * Time.deltaTime), 0);

        if(xOffset > 0)
        {
            xOffset -= xModifier * Time.deltaTime;
            xModifier += 100 * Time.deltaTime;
        }

        if(xOffset <= 0)
        {
            yOffset = 0;
        }
        else if(yOffset > 0)
        {
            yOffset -= yModifier * Time.deltaTime;
        }
    }

    public void SetDamageText(float value)
    {
        transform.GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
}
