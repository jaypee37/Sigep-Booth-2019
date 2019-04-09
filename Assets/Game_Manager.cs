using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public bool playerAttacking;
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
    bool DeLaCruzMoved = false;
    public DeLaCruzMvt DeLaCruz;
    bool cameraSetForNextPhase = false;
    bool enemiesDead = false;
    public NoteStaff staff;
    bool sequenceReady = false;
    string[] notesForStaff;
    bool running = false;
    public Canvas winScreen;
    public Canvas instructionScreen;
    bool instructionShown = false;

    


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
        Input.ResetInputAxes();
        running = false;
        StartCoroutine(WaitForAwake());
        
   
    }
    IEnumerator WaitForSequence()
    {
        yield return new WaitForSeconds(5);
        timeRanOut = true;
        waitingForSequence = false;
        StopAllCoroutines();

    }
    IEnumerator WaitForAwake()
    {
        yield return new WaitForSeconds(3);
        running = true;

    }
    IEnumerator WaitForEnemyDeath()
    {
        yield return new WaitWhile(() => player.isAttacking);
        curEnemy.takeDamage();

    }
    IEnumerator WaitForInstructionScreen()
    {
        instructionShown = true;
        yield return new WaitWhile(() => player.isAttacking);
        curEnemy.takeDamage();

    }



    private void LateUpdate()
    {
        cam.UpdateCam(dontTurnCamera);
    }

    // Update is called once per frame
    void Update()
    {
        if(running)
        {
            switch (currentPhase)
            {
                case phases.Phase1:
                    curEnemySetSize = 1;

                    if (!playerPhaseDone && !enemyMovingPhase)
                    {
                        HandlePlayerPhase();
                    }
                    if (!DeLaCruzMoved && playerPhaseDone)
                    {
                        HandleDeLaCruzPhase();
                    }

                    if (DeLaCruzMoved && playerPhaseDone && !enemyMovingPhase)
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
                    curEnemySetSize = 2;

                    if (!playerPhaseDone && !enemyMovingPhase)
                    {
                        HandlePlayerPhase();
                    }
                    if (!DeLaCruzMoved && playerPhaseDone)
                    {
                        HandleDeLaCruzPhase();
                    }

                    if (DeLaCruzMoved && playerPhaseDone && !enemyMovingPhase)
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
                    if (!DeLaCruzMoved && playerPhaseDone)
                    {
                        HandleDeLaCruzPhase();
                    }

                    if (DeLaCruzMoved && playerPhaseDone && !enemyMovingPhase)
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
                    if (!DeLaCruzMoved && playerPhaseDone)
                    {
                        HandleDeLaCruzPhase();
                    }

                    if (DeLaCruzMoved && playerPhaseDone && !enemyMovingPhase)
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
                    SceneHandler.instance.SetFadedAndCanvas(true,winScreen);
                    SceneHandler.instance.ChangeScene(SceneHandler.Scene.Win);
                    break;
            }
        }
     
        

        
       

    }


    void HandlePlayerPhase()
    {

        if(!player.isMoveFinished())
        {
           
            player.playerMoveLoc(playerLocIndex);
            
        }

        if(player.isMoveFinished() && player.turnFinished)
        {
            playerPhaseDone = true;

        }

    }


    void HandleDeLaCruzPhase()
    {

        DeLaCruzMoved = DeLaCruz.Move();

    }

    #region HandleEnemyPhase
    void HandleEnemyPhase(phases phase)
    {
   
        if(!enemiesMoving)
        {
            switch (phase)
            {
                case phases.Phase1:
                    enemySet = GameObject.FindGameObjectWithTag("EnemySet1");
                    curEnemySet = new enemyMove[1];
                    for (int i = 0; i < 1; i++)
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
                    curEnemySet = new enemyMove[2];
                    for (int i = 0; i < 2; i++)
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
            staff.notesFade = false;
            staff.fadeInFinished = false;
            //CreateButtonSequence();
            
           
        }
        
    }
    #endregion
    public void HandleLockOn()
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

    public void CreateButtonSequence()
    {
        
        sequence = new string[4];
        for (int i = 0; i < 4; i++)
        {
            sequence[i] = buttons[(int)Random.Range(0, 4)];
        }
        for (int i = 0; i < 4; i++)
        {
            print(sequence[i]);
        }
        //Display Buttons Functions

        StartCoroutine(WaitForSequence());
    }

    public void HandleSequencePhase()
    {
        
        if(!timeRanOut)
        {
            if (!sequenceFinished)
            {
                string curColor;

                if (Input.GetButtonDown("Green"))
                {
                    print("green");
                    curColor = "Green";
                }
                else if (Input.GetButtonDown("Red"))
                {
                    print("red");
                    curColor = "Red";
                }
                else if (Input.GetButtonDown("Yellow"))
                {
                    print("yellow");
                    curColor = "Yellow";
                }
                else if (Input.GetButtonDown("Blue"))
                {
                    print("blue");
                    curColor = "Blue";
                }
                else
                {
                    curColor = "none";
                }
                

                if (curColor == sequence[sequenceIndex])
                {
                    staff.GrayOutNote(sequenceIndex);
                    sequenceIndex++;
                    if (sequenceIndex == 4)
                    {
                        sequenceFinished = true;
                    }
                    return;
                }
                
            }

            if (sequenceFinished)
            {
                StopAllCoroutines();
                print("completed Sequence");
                CreateButtonSequence();
                sequenceFinished = false;
                sequenceIndex = 0;
                StopAllCoroutines();
                staff.FadeOutNotes();
                player.Attack();
                StartCoroutine(WaitForEnemyDeath());
            }
        }
        else

        {
            sequenceFinished = false;
            sequenceIndex = 0;
            print("you lost bitch");
            timeRanOut = false;
            //CreateButtonSequence();
            curEnemy.Attack();
            sequenceReady = false;
            staff.notesFade = false;
           
        }
        

    }
    void CameraLerp(bool  flag)
    {
        dontTurnCamera = true;
        cam.lerp(flag,sequence);
        
        
    }

    public void HandleAttackMode()
    {
        if(!enemiesDead)
        {
            HandleLockOn();
            if (!sequenceReady && curEnemy.finishedAttacking)
            {
                print("button sequecne");
                CreateButtonSequence();
                sequenceReady = true;
            }
            
            if (staff.fadeInFinished)
            {
              
                if(!staff.notesFade && curEnemy.finishedAttacking)
                {
                    if(currentPhase == phases.Phase1 && !instructionShown)
                    {
                        StartCoroutine(WaitForInstructionScreen());
                    }
                    staff.FadeNotes(1, sequence);
                }
                if (staff.notesFade && sequenceReady)
                {
                    HandleSequencePhase();
                }

                
            }
            CameraLerp(true);




            if (curEnemy.dead)
            {
               
                curEnemySetDeadCount++;
                playerLockingOn = false;
                staff.FadeOutNotes();
                print("enemyDead");
                if (curEnemySetDeadCount == curEnemySetSize)
                {
                    player.resetRotation((int)currentPhase);
                    enemiesDead = true;
                    cam.doneLerping = false;

                }
                if (!enemiesDead)
                {
                    CreateButtonSequence();
                    staff.notesFade = false;
                    staff.FadeNotes(1, sequence);
                }
                
                
                

            }

        }
        
        if(enemiesDead)
        {
            resetForNextPhase();
        }
        

    }

    public void enemyHit()
    {
        //print("sent enemy hit");
        curEnemy.takeDamage();
        //print("finish sent enemy hit");       
    }

    void resetVariables()
    {
        enemiesMoving = false;
        playerPhaseDone = false;
        enemyMovingPhase = false;
        player.setMoveFinished(false);
        playerLocIndex += 1;
        if ((int)currentPhase < 4)
        {
            currentPhase += 1;
        }

        curEnemySetDeadCount = 0;
        dontTurnCamera = false;
        DeLaCruzMoved = false;
        enemiesDead = false;
        sequenceReady = false;
        staff.fadeInFinished = false;
    }
    public void resetForNextPhase()
    {
        if (cam.doneLerping)
        {
            resetVariables();
            cam.StopAllCoroutines();
            cam.doneLerping = true;
        }
        else
        {
           CameraLerp(false);

        }
        
       
    }


}
