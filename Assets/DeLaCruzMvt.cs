using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeLaCruzMvt : MonoBehaviour
{
    bool moving = false;
    bool finishedMove = false;
    NavMeshAgent agent;
    public Transform[] locations = new Transform[4];
    int locIndex = 0;
    Transform curLoc;
    Vector3 goal;
    Animator animator;
    bool allowedToMove = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        curLoc = locations[locIndex];
        animator = GetComponent<Animator>();


    }

    IEnumerator WaitForPosition()
    {
       
        yield return new WaitWhile(() => goal.magnitude > .2f);          
        locIndex++;
        allowedToMove = false;
        moving = false;
        yield return new WaitForSeconds(2);
        finishedMove = false;
        transform.rotation = curLoc.rotation;
        animator.SetBool("Run", false);
        if ( locIndex < 3)
        {
            curLoc = locations[locIndex];
        }
        allowedToMove = true;
        

    }
    IEnumerator WaitForMove()
    {

        yield return new WaitForSeconds(.5f);
        finishedMove = true;

    }

    // Update is called once per frame
    void Update()
    {
        goal = transform.position - curLoc.position;
    }

    public bool Move()
    {
        if(locIndex < 3)
        {

            if (!moving && allowedToMove)
            {
                StartCoroutine(WaitForMove());
                animator.SetBool("Run", true);
                agent.destination = curLoc.position;
                StartCoroutine(WaitForPosition());
                moving = true;
            }
            return finishedMove;
        }
        else
        {
            return true;
        }
        

       
    }



}
