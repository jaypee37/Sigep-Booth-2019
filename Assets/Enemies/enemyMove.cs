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
    Vector3 goal2;
    Vector3 turnV;
    ParticleSystem pSystem;
    public bool LockedOn;
    public int health = 100;
    public bool dead;
    public bool moving;
    public bool sentDeadReciept;
    Transform tnew;
    Animator animator;
    public Transform loc2;
    public bool enemyAttacking;
    public EnemyAttackManager attackManager;
    bool finishedAttacking = false;
    public playerMove player;
   
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
        animator = GetComponent<Animator>();
        enemyAttacking = false;
       
    }
    IEnumerator WaitForPosition()
    {
        print("enemy is moving");
        yield return new WaitWhile(() => goal.magnitude > 2);       
        agent.destination = loc2.position;
        yield return new WaitWhile(() => goal2.magnitude > 0.2f);
        animator.SetBool("Run", false);
        agent.updatePosition = false;
        agent.updateRotation = false;
        
        print("enemy made it");
        yield return new WaitForSeconds(3);
        enemyAttacking = true;
        moveFinished = true;

    }
    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        //transform.localScale = Vector3.zero;

    }
    IEnumerator WaitForAttack(int time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("Attack", false);
        finishedAttacking = true;
        FinishedAttack();
        print("i finished attacking");
        StopCoroutine(WaitForAttack(time));
     

    }

    // Update is called once per frame
    void Update()
    {
        if (!moveFinished)
        {
            goal = transform.position - loc.position;
            goal2 = transform.position - loc2.position;
        }

        if (health == 0 && !dead)
        {
            //transform.localScale = Vector3.zero;
            animator.SetBool("Attack", false);
            animator.SetBool("Run", false);
            attackManager.enemyFinishedAttacking();
            animator.SetTrigger("Death");
            dead = true;
            Destroy(pSystem);
            StartCoroutine(WaitForDeath());

        }
        if (moveFinished)
        {
            transform.position = loc2.position;
        }

    }
    public void enemyMoveLoc()
    {
        moving = true;
        agent.updatePosition = true;
        agent.updateRotation = true;
        animator.SetBool("Run",true);
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

    public void SetTurnVector(Vector3 turn,GameObject t)
    {
        turnV = turn;
        tnew = t.transform;
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

    public void Attack()
    {
        transform.LookAt(player.transform);
        animator.SetBool("Run", false);
        animator.SetBool("Attack",true);
        int time = Random.Range(6, 9);
        StartCoroutine(WaitForAttack(time));
    }

    public void FinishedAttack()
    {
        attackManager.enemyFinishedAttacking();
    }
}   
