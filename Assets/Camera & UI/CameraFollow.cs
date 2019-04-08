using System.Collections;
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

    // Use this for initialization
    void Start()
    {

        //player = GameObject.FindGameObjectWithTag("Player");
        prevLoc = new GameObject();
        lerpLoc = locations[0];
    }

    // Update is called once per frame
    void LateUpdate()
    {

    }
    IEnumerator WaitForFade()

    {
        yield return new WaitWhile(() => goal.magnitude > 0.2f);
        print("done lerping");
        n.Fade(1);
    }

    IEnumerator WaitForLerp()

    {
        n.Fade(2);
        lerpBackward = true;
        yield return new WaitForSeconds(3);
        doneLerping = true;
        locIndex++;
        print(locIndex);
        lerpLoc = locations[locIndex];
        lerpBackward = false;
        lerpForward = false;
    }


    public void UpdateCam(bool flag)
    {
        goal = transform.position - lerpLoc.position;
        if (!flag)
        {

            transform.position = dummyCam.transform.position;
            transform.rotation = dummyCam.transform.rotation;
        }

    }

    public void lerp(bool dir)
    {

        if (dir)
        {
            if (!lerpForward)
            {
                StartCoroutine(WaitForFade());
                prevLoc.transform.position = transform.position;
                prevLoc.transform.rotation = transform.rotation;
                lerpForward = true;
            }

            transform.position = Vector3.Lerp(transform.position, lerpLoc.position, 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, lerpLoc.rotation, 0.05f);
            //print((transform.position - lerpLoc.position).magnitude < 0.2f);

        }


        else if (!dir)
        {
            if (!lerpBackward)
            {
                StartCoroutine(WaitForLerp());

            }
            transform.position = Vector3.Lerp(transform.position, prevLoc.transform.position, 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, prevLoc.transform.rotation, 0.05f);

        }


    }
}

