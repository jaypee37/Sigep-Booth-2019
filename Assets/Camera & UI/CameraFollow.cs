using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;
    public GameObject dummyCam;
    public Transform lerp1;
    bool donLerping = false;
    float distance;
    public Transform lookat;

	// Use this for initialization
	void Start () {
        //player = GameObject.FindGameObjectWithTag("Player");
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        
    }

    public void UpdateCam(bool flag)
    {
        if (!flag)
        {

            transform.position = dummyCam.transform.position;
            transform.rotation = dummyCam.transform.rotation;
        }
    }

    public void lerp()
    {
        if(!donLerping)
        {

            transform.position =  Vector3.Lerp(transform.position, lerp1.position,0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, lerp1.rotation, 0.05f);
            //donLerping = true;
        }
    }
}
