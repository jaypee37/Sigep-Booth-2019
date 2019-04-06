using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMove : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform loc;
    private bool moveFinished = false;
    Vector3 goal;
    Vector3 turnV;
    ParticleSystem pSystem;
    public bool LockedOn;
    public int health = 100;
    public bool dead;
    public bool moving;
    public bool sentDeadReciept;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = true;
        pSystem = transform.GetComponentInChildren<ParticleSystem>();
        LockedOn = false;
        agent.updatePosition = false;
        agent.updateRotation = false;
        dead = false;
        moving = false;
        sentDeadReciept = false;
        
    }
    IEnumerator WaitForPosition()
    {
        print("enemy is moving");
        yield return new WaitWhile(() => goal.magnitude > 2);
        agent.destination = turnV;
        moveFinished = true;
        yield return new WaitForSeconds(2);
        agent.updatePosition = false;
        agent.updateRotation = false;
        print("enemy made it");
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveFinished)
        {
            goal = transform.position - loc.position;
        }

        if(health == 0)
        {
            transform.localScale = Vector3.zero;
            dead = true;
            Destroy(pSystem);
            Destroy(gameObject.GetComponent<CapsuleCollider>());

        }
        
      
        

    }

    public void enemyMoveLoc()
    {
        moving = true;
        agent.updatePosition = true;
        agent.updateRotation = true;
        StartCoroutine(WaitForPosition());
        agent.destination = loc.position;
    }
    public void setRecieptSent(bool flag)
    {
        sentDeadReciept = flag;
    }
    public bool isMoveFinished()
    {
        return moveFinished;
    }

    public void Die()
    {
        transform.localScale = Vector3.zero;
    }

    public void SetTurnVector(Vector3 turn)
    {
        turnV = turn;
    }
    public void LockOn()
    {
        pSystem.Play();
        LockedOn = true;
    }
    public bool isLockedOn()
    {
        return LockedOn;
    }

    public void takeDamage()
    {
        health -= 50;
    }
}   
