using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;
    public GameObject dummyCam;

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
}
