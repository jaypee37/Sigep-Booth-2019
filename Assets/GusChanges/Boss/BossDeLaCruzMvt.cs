using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossDeLaCruzMvt : MonoBehaviour
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
    bool stunned;
    Material originalMat;
    public Material newMat;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        curLoc = locations[locIndex];
        animator = GetComponent<Animator>();
        originalMat = this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material;
    }

   
    IEnumerator WaitForMove()
    {

        yield return new WaitForSeconds(1.5f);
        finishedMove = true;

    }
    IEnumerator WaitForStun()
    {

        yield return new WaitForSeconds(1.0f);
        agent.speed = 0;
        targetTime = 1 + Time.deltaTime;
        animator.SetBool("Run", false);
        this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = newMat;
        stunned = true;

    }


    public float targetTime = 1.0f;
   
    // Update is called once per frame
    void Update()
    {
        goal = transform.position - curLoc.position;
        if (stunned == true)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0.0f)
            {
                stunned = false;
                //this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.SetColor("_Color", originalColor);
                this.gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = originalMat;
                agent.speed = 2;
                animator.SetBool("Run", true);
            }
        }
    }

    public void Stunned() {

        StartCoroutine(WaitForStun());
        //set speed to 0
        //Play fall animation
    }

    public bool Move()
    {
        

        if (!moving && allowedToMove)
        {
            StartCoroutine(WaitForMove());
            animator.SetBool("Run", true);
            agent.destination = curLoc.position;
            agent.speed = 2;
            moving = true;
        }
        return true;



    }
}
