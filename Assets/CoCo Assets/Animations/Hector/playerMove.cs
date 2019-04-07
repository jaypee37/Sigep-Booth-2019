using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class playerMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public ThirdPersonCharacter character;
    public bool playerMoveFinished = false;
    public Vector3 goal1;
    public Vector3 goal2;
    public Vector3 goal3;
    public Vector3 goal4;
    public GameObject projectile;
    public Transform[] locations = new Transform[4];
    private Transform curLoc;
    private bool moving = false;
    private bool turning = false;
    private Vector3 turnR;
    NavMeshAgent armAgent;
    public Transform armLoc;
    public bool attacking = false;
    public Quaternion playerRot;
    enemyMove enemyAttackingMe;
    bool gettingAttacked;
    int health = 100;
    float startAttackTime;
    bool tookDamage = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        agent.updateRotation = false;
        animator.SetBool("Idling", true);
        curLoc = locations[0];
        goal1 = transform.position - locations[0].position;
        goal2 = transform.position - locations[1].position;
        goal3 = transform.position - locations[2].position;
        goal4 = transform.position - locations[3].position;
        armAgent = GameObject.FindGameObjectWithTag("throw arm").GetComponent<NavMeshAgent>();
        attacking = false;

    }

    
    IEnumerator WaitForPosition(int i)
    {

        Vector3 turn = new Vector3();

        print("waiting til he reaches location");
        if (i == 0)
        {
            turn = locations[0].position;
            yield return new WaitWhile(() => goal1.magnitude > 0.2f);
            agent.destination = turn;
        }

        else if (i == 1)
        {
            turn.Set(locations[1].position.x + 2.5f, locations[1].position.y, locations[1].position.z);
            yield return new WaitWhile(() => goal2.magnitude > 0.2f);
            agent.destination = turn;
        }
        else if (i == 2)
        {
            turn.Set(locations[2].position.x, locations[2].position.y, locations[2].position.z + 2.5f);
            yield return new WaitWhile(() => goal3.magnitude > 0.2f);
            agent.destination = turn;
        }

        else if (i == 3)
        {
            turn.Set(locations[3].position.x - 2.5f, locations[3].position.y, locations[3].position.z);
            yield return new WaitWhile(() => goal4.magnitude > 0.2f);
            agent.destination = turn;
        }

        
        

        animator.SetBool("Idling", true);
        animator.SetBool("Running", false);
        playerMoveFinished = true;
        moving = false;      

    }


    // Update is called once per frame
    void Update()
    {
        
        goal1 = transform.position - locations[0].position;
        goal2 = transform.position - locations[1].position;
        goal3 = transform.position - locations[2].position;
        goal4 = transform.position - locations[3].position;

        if(gettingAttacked)
        {
            if(enemyAttackingMe.dead == false && Time.time - startAttackTime > 3.11 && !tookDamage)
            {
                
                takeDamage();
                tookDamage = true;
                //print("damage taken");
                //print(health);
            }
        }
        
        
        

    }

    public void playerMoveLoc(int index)
    {
        
        animator.SetBool("Idling", false);
        animator.SetBool("Running", true);
        StartCoroutine(WaitForPosition(index));
        curLoc = locations[index];
        agent.destination = curLoc.position;
        agent.updateRotation = true;


    }

    public bool isMoveFinished()
    {
        return playerMoveFinished;
    }

    public void setMoveFinished(bool flag)
    {
        playerMoveFinished = flag;
       
    }
    bool animatorIsAttacking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
                animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public void Attack(enemyMove enemy)
    {

        animator.SetTrigger("Attack");
        transform.LookAt(enemy.transform);

    }
    public void resetRotation(int i)
    {
        if(i < 3)
        {
            transform.LookAt(locations[i+1]);
        }
        
    }

    public void GettingAttacked(enemyMove enemyAttackingMe)
    {
        startAttackTime = Time.time;
        this.enemyAttackingMe = enemyAttackingMe;
        gettingAttacked = true;
    }
    public void DoneGettingAttacked()
    {
       // print("player recieved done attacking");
        gettingAttacked = false;
        tookDamage = false;
    }
    public void takeDamage()
    {
        health -= 10;
    }

}
