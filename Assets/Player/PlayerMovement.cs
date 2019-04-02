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
<<<<<<< HEAD
    Vector3 currentClickTarget;
    public GameObject loc1;
    public GameObject loc2;
    Vector3 curLoc;
    bool moving = false;
=======
    Vector3 currentTarget;
    int currLoc;
>>>>>>> c803d13594c6c182120b67bda00d3dd50be4c0fe

    public GameObject loc1, loc2, loc3, loc4, loc5;

    public GameObject[] locations;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
<<<<<<< HEAD
        currentClickTarget = transform.position;
        curLoc = loc1.transform.position;
=======
        currentTarget = thirdPersonCharacter.transform.position;
        locations = new GameObject[5] { loc1, loc2, loc3, loc4, loc5};
        currLoc = 0;
>>>>>>> c803d13594c6c182120b67bda00d3dd50be4c0fe
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // G for gamepad. TODO add to menu
        {
            currentTarget = locations[currLoc].transform.position; // clear the click target
            if (currLoc < locations.Length - 1) currLoc++;
        }

        ProcessDirectMovement();
    }

    private void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = currentTarget - thirdPersonCharacter.transform.position;

        if (movement.magnitude >= walkMoveStopRadius)
        {
<<<<<<< HEAD
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
=======
            thirdPersonCharacter.Move(movement, false, false);
>>>>>>> c803d13594c6c182120b67bda00d3dd50be4c0fe
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