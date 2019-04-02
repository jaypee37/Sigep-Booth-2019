using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections.Generic;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkMoveStopRadius = 0.2f;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;

    Vector3 currentTarget;
    int currLoc;

    public GameObject loc1, loc2, loc3, loc4, loc5;

    public GameObject[] locations;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();

        currentTarget = thirdPersonCharacter.transform.position;
        locations = new GameObject[5] { loc1, loc2, loc3, loc4, loc5 };
        currLoc = 0;

    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // G for gamepad. TODO add to menu
        {
            currentTarget = locations[0].transform.position; // clear the click target
           
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

            thirdPersonCharacter.Move(movement, false, false);


        }
    }
}