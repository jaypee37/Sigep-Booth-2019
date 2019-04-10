﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;
    public GameObject dummyCam;
    public Transform lerp1;
    Transform lerpLoc;
    public bool doneLerping = false;
    float distance;
    public Transform lookat;
    Vector3 goal;
    bool lerpForward = false;
    bool lerpBackward = false;
    GameObject prevLoc;
    public Transform[] locations;
    int locIndex = 0;
    public NoteStaff n;
    bool lerpingBitttch = false;
    Vector3 goal2;
    public bool donelerpingIn = false;
    float cameraTimerElapsed;
    float camerafadeTime = 4.0f;
    public bool fadeMaterialsInView = false;
    bool lerping = false;
    bool lerpCalled;
    public MaterialFaderManager _MaterialFadeManager;

    // Use this for initialization
    void Start()
    {
        prevLoc = new GameObject();
        lerpLoc = locations[0];
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }
    IEnumerator WaitForFade(string[] notes)

    {
        yield return new WaitWhile(() => goal.magnitude > 0.2f);
        n.FadeStaff(1,notes);
        donelerpingIn = true;
        cameraTimerElapsed = 0;
    }

    IEnumerator WaitForLerp()

    {
        n.FadeStaff(2,null);
        lerpBackward = true;
        yield return new  WaitWhile(() => goal2.magnitude > 0.2f);
        _MaterialFadeManager.FadeInMaterials();
        doneLerping = true;
        
    }


    public void UpdateCam(bool flag)
    {
        //goal = transform.position - lerpLoc.position;
        //goal2 = prevLoc.transform.position - transform.position;
        if (!flag)
        {
            transform.position = dummyCam.transform.position;
            transform.rotation = dummyCam.transform.rotation;
        }

    }

    public void Update()
    {
        if(lerping)
        {
            if(lerpForward)
            {
                cameraTimerElapsed += Time.deltaTime;
                if (cameraTimerElapsed > camerafadeTime)
                {
                    print("done lerping in");
                    lerping = false;
                    cameraTimerElapsed = 0;
                    n.FadeStaff(1,null);
                    donelerpingIn = true;
                }
                float percentage = (cameraTimerElapsed / camerafadeTime);
               

                if (percentage > .06 && percentage < .1 && !fadeMaterialsInView)
                {
                    _MaterialFadeManager.FadeOutMaterials();
                }

                transform.position = Vector3.Lerp(transform.position, lerpLoc.position, percentage);
                transform.rotation = Quaternion.Lerp(transform.rotation, lerpLoc.rotation, percentage);

            }

            else
            {
                cameraTimerElapsed += Time.deltaTime;
                if (cameraTimerElapsed > camerafadeTime)
                {
                    doneLerping = true;
                    lerping = false;
                    _MaterialFadeManager.FadeInMaterials();
                    cameraTimerElapsed = 0;
                    locIndex++;
                    print(locIndex);
                    if (locIndex < 4)
                    {
                        lerpLoc = locations[locIndex];
                    }

                    lerpBackward = false;
                    lerpForward = false;
                }
                float percentage = (cameraTimerElapsed / camerafadeTime);

                transform.position = Vector3.Lerp(transform.position, prevLoc.transform.position, 0.05f);
                transform.rotation = Quaternion.Lerp(transform.rotation, prevLoc.transform.rotation, 0.05f);

            }
        }
    }

    public void lerpIn(bool dir, string[] notes)
    {
        prevLoc.transform.position = transform.position;
        lerping = true;
        lerpForward = dir;
            
    }
    public void lerpOut()
    {
        n.FadeStaff(2, null);
        lerping = true;
        lerpForward = false;
        
    }
}

