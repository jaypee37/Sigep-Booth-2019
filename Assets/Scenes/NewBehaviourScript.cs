using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(Input.GetAxis("Strum Down"));
        if(Input.GetButtonDown("Green"))
        {
            print(Input.GetAxis("Strum Down"));
        }
    }
}
