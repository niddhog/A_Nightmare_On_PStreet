using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In order to shake, call the Shake() function in this class
//Params:
//shaketime: how long will it shake
//shakepower = how strong is the camera movement
//shakespeed = how fast does the camera move

public class CameraController : MonoBehaviour
{
    private bool enableShake;
    private bool locker;

    private float time;
    private float power;
    private float speed;
    private float timeCount;
    private Vector3 originPosition;
    

    private void Awake()
    {
        enableShake = false;
        locker = false;
        timeCount = 0;
    }


    private void Update()
    {
        if (enableShake)
        {
            timeCount += Time.deltaTime;
            if(transform.position.x < originPosition.x + power && !locker)
            {
                Camera.main.transform.position += new Vector3(speed, speed, 0) * Time.deltaTime;
            }
            else if(transform.position.x >= originPosition.x - power)
            {
                locker = true;
                Camera.main.transform.position -= new Vector3(speed, speed, 0) * Time.deltaTime;
            }
            else
            {
                locker = false;
            }

            if(timeCount >= time)
            {
                Camera.main.transform.position = originPosition;
                enableShake = false;
            }
        }
    }


    public void Shake(float shakeTime, float shakePower, float shakeSpeed)
    {
        if (!enableShake)
        {
            timeCount = 0;
            originPosition = transform.position;
            time = shakeTime;
            power = shakePower;
            speed = shakeSpeed;
            enableShake = true;
        }
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
