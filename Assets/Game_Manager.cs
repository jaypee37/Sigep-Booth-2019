using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public playerMove player;
    public enemyMove[] curEnemySet;
    public bool enemiesMoving = false;
    public int playerLocIndex = 0;
    public GameObject enemySet;
    public int curEnemySetSize = 0;
    public int curEnemySetDeadCount = 0;
    bool playerLockingOn = false;
    public float attackRate = 1F;
    public float timestamp = 0F;
    enemyMove curEnemy;
    Quaternion curPlayerRotation = Quaternion.Euler(0, 0, 0);
    bool dontTurnCamera;
    Vector3 curCamPos;
    public CameraFollow cam;
    public bool attacking;
    public armMove AttackAnimBehavior;

    enum phases
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4
    }
    phases currentPhase;
    bool playerPhaseDone = false;
    bool enemyMovingPhase = false;



    // Start is called before the first frame update
    void Start()
    {
        currentPhase = phases.Phase1;
        curEnemySet = new enemyMove[4];
        dontTurnCamera = false;
        attacking = false;
        
   
    }


    private void LateUpdate()
    {
        cam.UpdateCam(dontTurnCamera);
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (currentPhase)
        {
            case phases.Phase1:
                curEnemySetSize = 3;
                HandlePlayerPhase();
                if (playerPhaseDone = player.isMoveFinished())
                {
                    HandleEnemyPhase(currentPhase);
                    player.playerRot = player.transform.rotation;
                }
                if (enemyMovingPhase)
                {
                    HandleAttackMode();
                }
                break;
            case phases.Phase2:
                curEnemySetSize = 3;
                HandlePlayerPhase();
                if (playerPhaseDone = player.isMoveFinished())
                {
                    player.playerRot = player.transform.rotation;
                    HandleEnemyPhase(currentPhase);
                }
                if (enemyMovingPhase)
                {
                    HandleAttackMode();
                }
                break;
            case phases.Phase3:
                curEnemySetSize = 3;
                HandlePlayerPhase();
                if (playerPhaseDone = player.isMoveFinished())
                {
                    player.playerRot = player.transform.rotation;
                    HandleEnemyPhase(currentPhase);
                }
                if (enemyMovingPhase)
                {
                    HandleAttackMode();
                }
                break;
            case phases.Phase4:
                curEnemySetSize = 4;
                HandlePlayerPhase();
                if (playerPhaseDone = player.isMoveFinished())
                {
                    player.playerRot = player.transform.rotation;
                    HandleEnemyPhase(currentPhase);
                }
                if (enemyMovingPhase)
                {
                    HandleAttackMode();
                }
                break;
            default:
                print("end of game");
                break;
        }

        
       

    }

    void HandlePlayerPhase()
    {
        if(!player.isMoveFinished())
        {
            if (Input.GetKeyDown("1"))
            {
                player.playerMoveLoc(playerLocIndex);
            }
        }
        
        

    }

    void HandleEnemyPhase(phases phase)
    {
   
        if(!enemiesMoving)
        {
            switch (phase)
            {
                case phases.Phase1:
                    enemySet = GameObject.FindGameObjectWithTag("EnemySet1");
                    curEnemySet = new enemyMove[3];
                    for (int i = 0; i < 3; i++)
                    {
                        curEnemySet[i] = enemySet.transform.GetChild(i).GetComponent<enemyMove>();
                        Vector3 turn = new Vector3();
                        turn.Set(curEnemySet[i].loc.position.x, curEnemySet[i].loc.position.y, curEnemySet[i].loc.position.z - 2);

                        curEnemySet[i].SetTurnVector(turn);
                    }
                    break;
                case phases.Phase2:
                    enemySet = GameObject.FindGameObjectWithTag("EnemySet2");
                    curEnemySet = new enemyMove[3];
                    for (int i = 0; i < 3; i++)
                    {
                        curEnemySet[i] = enemySet.transform.GetChild(i).GetComponent<enemyMove>();
                        Vector3 turn = new Vector3();
                        turn.Set(curEnemySet[i].loc.position.x - 2, curEnemySet[i].loc.position.y, curEnemySet[i].loc.position.z);

                        curEnemySet[i].SetTurnVector(turn);
                    }
                    break;
                case phases.Phase3:
                    enemySet = GameObject.FindGameObjectWithTag("EnemySet3");
                    curEnemySet = new enemyMove[3];
                    for (int i = 0; i < 3; i++)
                    {
                        curEnemySet[i] = enemySet.transform.GetChild(i).GetComponent<enemyMove>();
                        Vector3 turn = new Vector3();
                        turn.Set(curEnemySet[i].loc.position.x, curEnemySet[i].loc.position.y, curEnemySet[i].loc.position.z);

                        curEnemySet[i].SetTurnVector(turn);
                    }
                    break;
                case phases.Phase4:
                    enemySet = GameObject.FindGameObjectWithTag("EnemySet4");
                    curEnemySet = new enemyMove[4];
                    for (int i = 0; i < 4; i++)
                    {
                        curEnemySet[i] = enemySet.transform.GetChild(i).GetComponent<enemyMove>();
                        Vector3 turn = new Vector3();
                        turn.Set(curEnemySet[i].loc.position.x, curEnemySet[i].loc.position.y, curEnemySet[i].loc.position.z);

                        curEnemySet[i].SetTurnVector(turn);
                    }
                    break;

            }

            foreach (enemyMove e in curEnemySet)
            {
                if(e.moving == false)
                {
                    e.enemyMoveLoc();
                }
                
            }
            enemiesMoving = true;
        }

        bool flag = true;

        foreach (enemyMove e in curEnemySet)
        {
            flag = flag && e.isMoveFinished();                      
        }

        if (flag == true)
        {
            enemyMovingPhase = true;
        }
        
    }

    public void HandleAttackMode()
    {

        

        
        if(!playerLockingOn)
        {
            int i = (int)(Random.Range(0, curEnemySetSize));

            while (curEnemySet[i].isLockedOn())
            {
                i = (int)(Random.Range(0, curEnemySetSize));
            }

            curEnemy = curEnemySet[i];
            curEnemySet[i].LockOn();
            playerLockingOn = true;
        }

        if (Input.GetButtonDown("HectorAttack"))
        {
            
            if (Time.time > timestamp)
                {
                    player.Attack(curEnemy);
                    timestamp = Time.time + (3.1F * attackRate);
                    dontTurnCamera = true;                 
                }   
            
        }


        for (int i = 0; i < curEnemySetSize; i++)
        {

            if(curEnemySet[i].dead == true && !curEnemySet[i].sentDeadReciept)
            {
                curEnemySetDeadCount++;
                curEnemySet[i].setRecieptSent(true);
                playerLockingOn = false;
            }           

        }

        if (curEnemySetDeadCount == curEnemySetSize)
        {
            print("reseting for next phase");
            player.resetRotation((int)currentPhase);
            resetForNextPhase();

        }

        if (player.animator.GetCurrentAnimatorStateInfo(0).IsName("attack")) 
        {
            attacking = true;
        }
        
        if(attacking && !player.animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            attacking = false;
        }

    }

    public void enemyHit()
    {

        print("sent enemy hit");
        curEnemy.takeDamage();
        print("finish sent enemy hit");
        
    }
    public void resetForNextPhase()
    {
        enemiesMoving = false;
        playerPhaseDone = false;
        enemyMovingPhase = false;
        player.setMoveFinished(false);
        playerLocIndex += 1;
        if((int)currentPhase < 4)
        {
            currentPhase += 1;
        }
        
        curEnemySetDeadCount = 0;
        dontTurnCamera = false;

    }


}
