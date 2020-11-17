using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    


    public void Start()
    {
        
    }



    public IEnumerator ShakeCamera()
    {
        if(PlayerStats.firingSpeed > 0.8f)
        {
            Camera.main.transform.position += new Vector3(0.5f, 0.5f, 0);
            yield return new WaitForSeconds(0.05f);
            Camera.main.transform.position -= new Vector3(0.5f, 0.5f, 0);
        }
        else
        {
            Camera.main.transform.position += new Vector3(0.5f, 0.5f, 0);
            yield return new WaitForSeconds(0.07f);
            Camera.main.transform.position -= new Vector3(0.5f, 0.5f, 0);
        }
    }
}
