using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Input.GetJoystickNames();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Square"))
        {
            print("whooo");

        }
        if (Input.GetButtonDown("Horizontal"))
        {
            print("whooo");
        }
        if (Input.GetButtonDown("Vertical"))
        {
            print("whooo");
        }
        if (Input.GetButtonDown("Square"))
        {
            print("whooo");
        }
        if (Input.GetButtonDown("X"))
        {
            print("whooo");
        }
        if (Input.GetButtonDown("Circle"))
        {
            print("whooo");
        }
        if (Input.GetButtonDown("Triangle"))
        {
            print("whooo");
        }
        if (Input.GetButtonDown("Green"))
        {
            print("whooo");
        }
        if (Input.GetButtonDown("Red"))
        {
            print("whooo");
        }
        if (Input.GetButtonDown("Yellow"))
        {
            print("whooo");
        }
    }
}