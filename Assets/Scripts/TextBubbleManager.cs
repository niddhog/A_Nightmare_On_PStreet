using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBubbleManager : MonoBehaviour
{
    public GameObject fireBubblePrefab;

    public void DisplayBubble(GameObject bubble, Vector3 position, float time)
    {
        GameObject textBubble = Instantiate(bubble, position, Quaternion.identity);
        textBubble.name = "bubble";
        textBubble.transform.SetParent(GameObject.Find("PrefabSink").GetComponent<Transform>(), false);
        StartCoroutine(KillTimer(textBubble, time));
    }


    public void RemoveBubble(GameObject bubble)
    {
        Debug.Log("Gib");
        bubble.GetComponent<Animator>().SetBool("Disappears", true);
    }


    private IEnumerator KillTimer(GameObject bubble, float time)
    {
        yield return new WaitForSeconds(time);
        RemoveBubble(bubble);
    }
}
