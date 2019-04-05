using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;
    Vector3 cameraPos;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //cameraPos.Set(player.transform.position.x, player.transform.position.y + 2.0f, player.transform.position.z - 5.0f);
        //transform.position = cameraPos;
        //transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation, Time.deltaTime * 2.0f);
    }
}
