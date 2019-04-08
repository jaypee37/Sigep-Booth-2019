using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;
    public GameObject dummyCam;
    public Transform lerp1;
    public bool doneLerping = false;
    float distance;
    public Transform lookat;
    Vector3 goal;
    bool lerpForward = false;
    bool lerpBackward = false;
    GameObject prevLoc;

	// Use this for initialization
	void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");
        prevLoc = new GameObject();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        
    }
    IEnumerator WaitForLerp()

    {

        lerpBackward = true;
        yield return new WaitForSeconds(3);
        
        doneLerping = true;

        

    }
    

    public void UpdateCam(bool flag)
    {
        if (!flag)
        {

            transform.position = dummyCam.transform.position;
            transform.rotation = dummyCam.transform.rotation;
        }
        
        goal = transform.position - lerp1.position;
    }

    public void lerp(bool dir)
    {
       
        if (dir)
        {
            if(!lerpForward)
            {
                
                prevLoc.transform.position = transform.position;
                prevLoc.transform.rotation = transform.rotation;
                lerpForward = true;
            }
            
            transform.position = Vector3.Lerp(transform.position, lerp1.position, 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, lerp1.rotation, 0.05f);

        }
            
        
        else if (!dir )
        {
            print("false");
            if(!lerpBackward)
            {
                StartCoroutine(WaitForLerp());
                
            }
            transform.position = Vector3.Lerp(transform.position, prevLoc.transform.position, 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, prevLoc.transform.rotation, 0.05f);

        }
        
        
    }

    public bool GetLerpStatus()
    {
        return doneLerping;
    }
}
