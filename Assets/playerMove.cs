﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerMove : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform loc1;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        ;
    }

    public void playerMoveLoc1()
    {
        agent.destination = loc1.position;
    }
}
