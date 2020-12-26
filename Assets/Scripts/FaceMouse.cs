using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FaceMouse : MonoBehaviour
{

    public static Vector2 mousePosition;
    public static Vector2 direction;

    PlayerControls controls;
    private bool pause;


    private void Awake()
    {
        controls = new PlayerControls();
        pause = false;
    }


    void Start()
    {
        mousePosition = Vector2.zero;
        direction = Vector2.zero;
    }


    void Update()
    {
        if (!pause)
        {
            mousePosition = controls.Basic.MousePosition.ReadValue<Vector2>();
            direction = GetDirection(Camera.main.ScreenToWorldPoint(mousePosition), transform.position);
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, PlayerStats.rotationSpeed * Time.deltaTime);
        }
    }


    public static Vector3 GetDirection(Vector3 start, Vector3 end)
    {
        return start - end;
    }


    public void Pause()
    {
        pause = true;
    }


    public void UnPause()
    {
        pause = false;
    }



    #region Enable/Disable
    private void OnEnable()
    {
        controls.Enable();
    }


    private void OnDisable()
    {
        controls.Disable();
    }
    #endregion
}
