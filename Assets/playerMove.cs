using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class playerMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform loc1;
    public Animator animator;
    public ThirdPersonCharacter character;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        agent.updateRotation = false;
        animator.SetBool("Idling", true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerMoveLoc1()
    {
        
        agent.destination = loc1.position;
        agent.updateRotation = false;
        
 
    }
}
