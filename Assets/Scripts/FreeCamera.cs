using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{

    public VirtualJoystick cameraJoystick;
    //the player
    public Transform lookAt;

    private float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float sensivityX = 3.0f;
    private float sensivityY = 1.0f;

    private void Update()
    {
        currentX += cameraJoystick.InputDirection.x * sensivityX;
        currentY += cameraJoystick.InputDirection.z * sensivityY;
    }
    //Make sure that player move first and than camera moves
    private void LateUpdate()
    {
        //distance ten meter behind the wall itself
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        //transform.position = camera
        transform.position = lookAt.position + rotation * dir;
        transform.LookAt(lookAt);
    }
}
