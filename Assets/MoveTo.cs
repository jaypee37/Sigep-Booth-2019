using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
    public Transform goal;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();       
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown("space"))
        {
            agent.destination = goal.position; 
        }
        
    }
}
