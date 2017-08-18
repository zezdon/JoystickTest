using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    //how player afects about players movements
    public float drag = 0.5f;
    public float terminalRotationSpeed = 25.0f;
    public VirtualJoystick moveJoystick;

    //Boost variables
    public float boostSpeed = 5.0f;
    public float boostCooldown = 2.0f;
    private float lastBoost;

    //Actually do the movement
    private Rigidbody controller;
    private Transform camTransform;

    private void Start()
    {
        lastBoost = Time.time - boostCooldown;

        controller = GetComponent<Rigidbody>();
        controller.maxAngularVelocity = terminalRotationSpeed;
        controller.drag = drag;

        camTransform = Camera.main.transform;
    }

    private void Update()
    {
        //Are you pressing any key
        Vector3 dir = Vector3.zero;
        //x axis = up and down
        dir.x = Input.GetAxis("Horizontal");
        //z axis = forward and backward
        dir.z = Input.GetAxis("Vertical");

        //dont faster than diagonal axis
        if (dir.magnitude > 1)
            dir.Normalize();
        //If you have some movements on joystick
        if (moveJoystick.InputDirection != Vector3.zero)
        {
            dir = moveJoystick.InputDirection;
        }

        //Rotate our direction vector with camera
        Vector3 rotatedDir = camTransform.TransformDirection(dir);
        rotatedDir = new Vector3(rotatedDir.x, 0, rotatedDir.z);
        rotatedDir = rotatedDir.normalized * dir.magnitude;

        controller.AddForce(rotatedDir * moveSpeed);
    }

    public void Boost()
    {
        if (Time.time - lastBoost > boostCooldown)
        {


            controller.AddForce(controller.velocity.normalized * boostSpeed, ForceMode.VelocityChange);
        }
    }
}

