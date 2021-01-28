using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject fogPrefab;
    private bool ongoing;
    private bool colorOngoing;

    private void Awake()
    {
        ongoing = false;
        colorOngoing = false;
    }

    public void SetFog(float intensity)
    {
        if(GameObject.Find("Fog") == null)
        {
            GameObject temp = Instantiate(fogPrefab, new Vector3(-50, -50, 60), Quaternion.identity);
            temp.name = "Fog";
            temp.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
        }
        GameObject fog = GameObject.Find("Fog");
        Color tmp = fog.GetComponent<SpriteRenderer>().color;
        tmp.a = intensity;
        fog.GetComponent<SpriteRenderer>().color = tmp;
    }


    public void ChangeFogColor(float time, float r, float g, float b)
    {
        if (GameObject.Find("Fog") == null)
        {
            GameObject temp = Instantiate(fogPrefab, new Vector3(-50, -50, 60), Quaternion.identity);
            temp.name = "Fog";
            temp.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
        }
        GameObject fog = GameObject.Find("Fog");
        if (!colorOngoing)
        {
            StartCoroutine(ChangeColor(time, r, g, b, fog));
            colorOngoing = true;
        }
    }


    public void ChangeFogIntensityOverTime(float time, float intensity)
    {
        if (GameObject.Find("Fog") == null)
        {
            GameObject temp = Instantiate(fogPrefab, new Vector3(-50, -50, 60), Quaternion.identity);
            temp.name = "Fog";
            temp.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
        }
        GameObject fog = GameObject.Find("Fog");
        if (!ongoing)
        {
            StartCoroutine(ChangeIntensity(time, intensity, fog));
            ongoing = true;
        }
    }


    private IEnumerator ChangeIntensity(float seconds, float intensity, GameObject fog)
    {
        bool decrease = true;
        float originalIntensity = fog.GetComponent<SpriteRenderer>().color.a;
        float looper = 0;
        if (originalIntensity < intensity)
        {
            decrease = false;
        }
        float step = (Mathf.Abs(originalIntensity - intensity))/(seconds*100);

        while (looper < seconds)
        {
            Color tmp = fog.GetComponent<SpriteRenderer>().color;
            tmp.a = step;
            tmp.r = 0;
            tmp.g = 0;
            tmp.b = 0;

            if (decrease)
            {
                fog.GetComponent<SpriteRenderer>().color -= tmp;
            }
            else
            {
                fog.GetComponent<SpriteRenderer>().color += tmp;
            }

            yield return new WaitForSeconds(0.01f);
            looper += 0.01f;
        }
        ongoing = false;
    }


    private IEnumerator ChangeColor(float seconds, float r, float g, float b, GameObject fog)
    {
        bool decreaseR = true;
        bool decreaseG = true;
        bool decreaseB = true;

        r = r / 255;
        g = g / 255;
        b = b / 255; 
        float originalR = fog.GetComponent<SpriteRenderer>().color.r;
        float originalG = fog.GetComponent<SpriteRenderer>().color.g;
        float originalB = fog.GetComponent<SpriteRenderer>().color.b;

        float looper = 0;

        if (originalR < r)
        {
            decreaseR = false;
        }
        if (originalG < g)
        {
            decreaseG = false;
        }
        if (originalB < b)
        {
            decreaseB = false;
        }

        float stepR = (Mathf.Abs(originalR - r)) / (seconds * 100);
        float stepG = (Mathf.Abs(originalG - g)) / (seconds * 100);
        float stepB = (Mathf.Abs(originalB - b)) / (seconds * 100);

        while (looper < seconds)
        {
            Color tmp = fog.GetComponent<SpriteRenderer>().color;
            tmp.a = 0;

            tmp.r = stepR;
            tmp.g = 0;
            tmp.b = 0;
            if (decreaseR)
            {               
                fog.GetComponent<SpriteRenderer>().color -= tmp;
            }
            else
            {
                fog.GetComponent<SpriteRenderer>().color += tmp;
            }

            tmp.r = 0;
            tmp.g = stepG;
            tmp.b = 0;
            if (decreaseG)
            {
                fog.GetComponent<SpriteRenderer>().color -= tmp;
            }
            else
            {
                fog.GetComponent<SpriteRenderer>().color += tmp;
            }

            tmp.r = 0;
            tmp.g = 0;
            tmp.b = stepB;
            if (decreaseB)
            {
                fog.GetComponent<SpriteRenderer>().color -= tmp;
            }
            else
            {
                fog.GetComponent<SpriteRenderer>().color += tmp;
            }

            yield return new WaitForSeconds(0.01f);
            looper += 0.01f;
            //Debug.Log(fog.GetComponent<SpriteRenderer>().color);
        }
        Color adjust = fog.GetComponent<SpriteRenderer>().color;
        adjust.r = r;
        adjust.g = g;
        adjust.b = b;
        fog.GetComponent<SpriteRenderer>().color = adjust;
        colorOngoing = false;
    }
}
