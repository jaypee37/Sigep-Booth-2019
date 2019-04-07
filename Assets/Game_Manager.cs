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
    public EnemyAttackManager enemyManager;
    bool changeOfDirection = false;
    int currentEnemyIndex;
    int sequenceIndex = 0;
    string[] buttons = { "Green", "Red", "Yellow", "Blue" };
    string[] sequence;
    bool sequenceFinished = false;
    bool timeRanOut = false;
    bool waitingForSequence = false;


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
    IEnumerator WaitForSequence()
    {
        yield return new WaitForSeconds(20);
        timeRanOut = true;
        waitingForSequence = false;

    }


    private void LateUpdate()
    {
        cam.UpdateCam(dontTurnCamera);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey)
        {
            print("who");
        }
        
        switch (currentPhase)
        {
            case phases.Phase1:
                curEnemySetSize = 3;
                if(!playerPhaseDone && !enemyMovingPhase)
                {
                    HandlePlayerPhase();
                }
                
                if (playerPhaseDone && !enemyMovingPhase)
                {
                    HandleEnemyPhase(currentPhase);
                    player.playerRot = player.transform.rotation;
                }
                if (enemyMovingPhase )
                {
                    HandleAttackMode();
                }
                break;
            case phases.Phase2:
                curEnemySetSize = 3;
                if (!playerPhaseDone && !enemyMovingPhase)
                {
                    HandlePlayerPhase();
                }

                if (playerPhaseDone && !enemyMovingPhase)
                {
                    HandleEnemyPhase(currentPhase);
                    player.playerRot = player.transform.rotation;
                }
                if (enemyMovingPhase)
                {
                    HandleAttackMode();
                }
                break;
            case phases.Phase3:
                curEnemySetSize = 3;
                if (!playerPhaseDone && !enemyMovingPhase)
                {
                    HandlePlayerPhase();
                }

                if (playerPhaseDone && !enemyMovingPhase)
                {
                    HandleEnemyPhase(currentPhase);
                    player.playerRot = player.transform.rotation;
                }
                if (enemyMovingPhase)
                {
                    HandleAttackMode();
                }
                break;
            case phases.Phase4:
                curEnemySetSize = 4;
                if (!playerPhaseDone && !enemyMovingPhase)
                {
                    HandlePlayerPhase();
                }

                if (playerPhaseDone && !enemyMovingPhase)
                {
                    HandleEnemyPhase(currentPhase);
                    player.playerRot = player.transform.rotation;
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
    void HandleDeLaCruzPhase()
    {
        
        


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
        else
        {
            playerPhaseDone = true;
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
                        GameObject t = new GameObject();
                        t.transform.position = turn;

                        curEnemySet[i].SetTurnVector(turn,t);
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
                        GameObject t = new GameObject();
                        t.transform.position = turn;
                        curEnemySet[i].SetTurnVector(turn,t);
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
                        GameObject t = new GameObject();
                        t.transform.position = turn;
                        curEnemySet[i].SetTurnVector(turn,t);
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
                        GameObject t = new GameObject();
                        t.transform.position = turn;
                        curEnemySet[i].SetTurnVector(turn,t);
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
            print("call init");
            enemyMovingPhase = true;
            playerPhaseDone = false;
            enemyManager.InitCurrentEnemySet(curEnemySet,curEnemySetSize);
            CreateButtonSequence();
            

        }
        
    }
    public void HandleLockOn()
    {
        float direction = Input.GetAxis("Horizontal");
        if(direction != 0 && playerLockingOn && !changeOfDirection)
        {
            changeOfDirection = true;
            if(direction < 0)
            {
                //print("move left");
                if(currentEnemyIndex - 1 >= 0 && !curEnemySet[currentEnemyIndex-1].dead  )
                {
                    curEnemySet[currentEnemyIndex].LockOn(false);
                    currentEnemyIndex -= 1;
                    curEnemy = curEnemySet[currentEnemyIndex];
                    curEnemySet[currentEnemyIndex].LockOn(true);
                }

            }
            else
            {
               // print("move right");
                if (currentEnemyIndex + 1 >= 0 && !curEnemySet[currentEnemyIndex + 1].dead)
                {
                    curEnemySet[currentEnemyIndex].LockOn(false);
                    currentEnemyIndex += 1;
                    curEnemy = curEnemySet[currentEnemyIndex];
                    curEnemySet[currentEnemyIndex].LockOn(true);
                }
            }
            
        }
        else if(direction == 0 && changeOfDirection)
        {
            changeOfDirection = false;
        }
        else
        {
            if (!playerLockingOn)
            {
                currentEnemyIndex = (int)(Random.Range(0, curEnemySetSize));

                while (curEnemySet[currentEnemyIndex].isLockedOn() || curEnemySet[currentEnemyIndex].dead)
                {
                    currentEnemyIndex = (int)(Random.Range(0, curEnemySetSize));
                }

                curEnemy = curEnemySet[currentEnemyIndex];
                curEnemySet[currentEnemyIndex].LockOn(true);
                playerLockingOn = true;
            }
        }
        



    }

    public void CreateButtonSequence()
    {
        
        sequence = new string[6];
        for (int i = 0; i < 6; i++)
        {
            sequence[i] = buttons[(int)Random.Range(0, 4)];
        }
        for (int i = 0; i < 6; i++)
        {
            print(sequence[i]);
        }

        StartCoroutine(WaitForSequence());
    }

    public void HandleSequencePhase()
    {
        
        if(!timeRanOut)
        {
            if (!sequenceFinished)
            {
                for (int i = 0; i < 4; i++)
                {

                    if (Input.GetButton(buttons[i]) )
                    {
                        print("pressed");
                        sequenceIndex++;
                        if (sequenceIndex == 6)
                        {
                            sequenceFinished = true;
                        }
                        return;
                    }
                }
            }

            if (sequenceFinished)
            {
                StopAllCoroutines();
                print("completed Sequence");
                CreateButtonSequence();
                sequenceFinished = false;
                sequenceIndex = 0;
                curEnemy.takeDamage();
            }
        }
        else

        {
            sequenceFinished = false;
            sequenceIndex = 0;
            print("you lost bitch");
            timeRanOut = false;
            CreateButtonSequence();
            
        }
        

    }

    public void HandleAttackMode()
    {
        HandleLockOn();
        HandleSequencePhase();
        
        

       /* if (Input.GetButtonDown("HectorAttack"))
        {
            
            if (Time.time > timestamp)
                {
                    player.Attack(curEnemy);
                    timestamp = Time.time + (2.1F * attackRate);
                    dontTurnCamera = true;                 
                }   
            
        }*/

        if(curEnemy.dead)
        {
            curEnemySetDeadCount++;           
            playerLockingOn = false;
        }


        

        if (curEnemySetDeadCount == curEnemySetSize)
        {
           // print("reseting for next phase");
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
        //print("sent enemy hit");
        curEnemy.takeDamage();
        //print("finish sent enemy hit");       
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
