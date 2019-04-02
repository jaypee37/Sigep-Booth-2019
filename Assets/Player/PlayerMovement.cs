using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections.Generic;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = 0.2f;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;
    public GameObject loc1;
    public GameObject loc2;
    Vector3 curLoc;
    bool moving = false;

    bool isInDirectMode = false;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
        curLoc = loc1.transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G)) // G for gamepad. TODO add to menu
        {
            isInDirectMode = !isInDirectMode; // toggle mode
            currentClickTarget = transform.position; // clear the click target
        }

        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
    }

    private void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

        thirdPersonCharacter.Move(movement, false, false);
    }

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    currentClickTarget = loc1.transform.position;                
                    break;
                case Layer.Enemy:
                    print("Not moving to enemy");
                    break;
                default:
                    print("Unexpected layer found");
                    print(cameraRaycaster.currentLayerHit);
                    return;
            }
        }

        var playerToClickPoint = currentClickTarget - transform.position;
        //print(playerToClickPoint);
        if (playerToClickPoint.magnitude >= walkMoveStopRadius)
        {   
            if(moving == false)
            {
                moving = true;
            }
            thirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            print("else statement");
            if (moving == true)
            {
                moving = false;
                curLoc = loc2.transform.position;
            }
            thirdPersonCharacter.Move(Vector3.zero, false, false);
        }
    }
}

