using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    public Transform lookAt;
    //Smooth camera
    private Vector3 desiredPosition;

    private Vector3 offset;

    //Swipe
    private Vector2 touchPosition;
    private float swipeResistance = 200.0f;

    //for smooth camera
    private float smoothSpeed = 7.5f;
    //distance beetwen itself and player
    private float distance = 5.0f;
    private float yOffset = 3.5f;

    private void Start()
    {
        //Camera back on the boll
        offset = new Vector3(0, yOffset, -1f * distance);

    }

    private void Update()
    {
        //Keyboard input to switch camera
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            SlideCamera(true);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            SlideCamera(false);

        //Looking for touch information
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            touchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            float swipeForce = touchPosition.x - Input.mousePosition.x;
            //touchPosition.x is bigger than mousePosition.x
            if (Mathf.Abs(swipeForce) > swipeResistance)
            {
                //We had a swipe
                if (swipeForce < 0)
                    //Swipe left
                    SlideCamera(true);
                else
                    //Swipe right
                    SlideCamera(false);

            }
        }

    }
    //Fix the chunky fx
    private void FixedUpdate()
    {
        //transform.position = lookAt.position + offset;
        //This will give some smoothing effect beetwen camera switch
        desiredPosition = lookAt.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        //Camera will look at the player
        //transform.LookAt (lookAt);
        transform.LookAt(lookAt.position + Vector3.up);

    }

    public void SlideCamera(bool left)
    {
        if (left)
            offset = Quaternion.Euler(0, 90, 0) * offset;
        else
            offset = Quaternion.Euler(0, -90, 0) * offset;
    }
}
