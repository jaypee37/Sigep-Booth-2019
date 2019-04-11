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
    bool timer_Started = false;
    public TextMeshProUGUI numberText;
    bool setMovingEnemies = false;
    bool ActiveButtons = false;
    Quaternion prevPlayerRotation;
    bool lerpCalled;
    public ActivateBoss activateBoss;
    int maxSequenceNumber;

    


    enum phases
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Phase5
    }

    phases currentPhase;
    bool playerPhaseDone = false;
    bool enemyMovingPhase = false;


    // Start is called before the first frame update
    void Start()
    {
        SetDifficulty(SceneHandler.instance.difficulty);
        instructionScreen.gameObject.SetActive(false);
        currentPhase = phases.Phase1;
        curEnemySet = new enemyMove[4];
        dontTurnCamera = false;
        Input.ResetInputAxes();
        running = false;
        StartCoroutine(WaitForAwake());        
   
    }
    IEnumerator WaitForSequence()
    {
        
        staff.FadeTimerNumbersInandOut();
        timer_Started = true;
        yield return new WaitWhile(() => staff.fading);       
        timer_Started = false;
        timeRanOut = true;
        waitingForSequence = false;
      
        //StopAllCoroutines();

    }
    IEnumerator WaitForAwake()
    {
        yield return new WaitForSeconds(3);
        running = true;

    }
    IEnumerator WaitForPLayerAttack()
    {
        yield return new WaitWhile(() => player.isAttacking );
        StartCoroutine(WaitForEnemyDeath());
        sequenceReady = false;
        player.transform.rotation = prevPlayerRotation;
        

    }
    IEnumerator WaitForEnemyAttack()
    {
        yield return new WaitUntil(() => curEnemy.finishedAttacking);
        sequenceReady = false;
        timer_Started = false;
        timeRanOut = false;
        

    }
    IEnumerator WaitForEnemyDeath()
    {
        curEnemy.takeDamage();
        yield return new WaitWhile(() => player.isAttacking);
        

    }
    IEnumerator WaitForInstructionScreen()
    {
        instructionShown = true;
        instructionScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        Destroy(instructionScreen.gameObject);
        //curEnemy.takeDamage();

    }

    void SetDifficulty(SceneHandler.Difficulty difficulty)
    {
        switch(difficulty)
        {
            case SceneHandler.Difficulty.Easy:
                maxSequenceNumber = 2;
                break;
            case SceneHandler.Difficulty.Medium:
                maxSequenceNumber = 4;
                break;
            case SceneHandler.Difficulty.Hard:
                maxSequenceNumber = 6;
                break;
        }
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
                    activateBoss.Activate();                    
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
            if(!setMovingEnemies)
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

                            curEnemySet[i].SetTurnVector(turn, t);
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
                            curEnemySet[i].SetTurnVector(turn, t);
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
                            curEnemySet[i].SetTurnVector(turn, t);
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
                            curEnemySet[i].SetTurnVector(turn, t);
                        }
                        break;
                        

                }
                setMovingEnemies = true;
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
        
        sequence = new string[maxSequenceNumber];
        for (int i = 0; i < 4; i++)
        {
            sequence[i] = buttons[(int)Random.Range(0, 4)];
        }
        for (int i = 0; i < 4; i++)
        {
            print(sequence[i]);
        }
        //Display Buttons Functions

        
    }

    public void HandleSequencePhase()
    {
        
        if(!timeRanOut)
        {
            if (!sequenceFinished && ActiveButtons)
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
                

                if (curColor == sequence[sequenceIndex] || Input.GetButtonDown("Submit") )
                {
                    staff.GrayOutNote(sequenceIndex);
                    sequenceIndex++;
                    if (sequenceIndex == maxSequenceNumber)
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
                sequenceFinished = false;
                //sequenceReady = false;
                sequenceIndex = 0;
                
                staff.FadeOutNotes();
                prevPlayerRotation = player.transform.rotation;
                player.transform.LookAt(curEnemy.transform);
                player.Attack();                
                
                StartCoroutine(WaitForPLayerAttack());
                staff.StopTimer();
                ActiveButtons = false;
                curEnemySetDeadCount++;
                if(curEnemySetDeadCount == curEnemySetSize)
                {
                    if(instructionScreen != null)
                    {
                        Destroy(instructionScreen.gameObject);
                    }
                    staff.FadeStaff(2, null);
                }

            }
        }
        else

        {
            sequenceFinished = false;
            sequenceIndex = 0;
            print("you lost bitch");
            timeRanOut = false;
            curEnemy.Attack();

            staff.notesFade = false;            
            StopCoroutine(WaitForSequence());
            StartCoroutine(WaitForEnemyAttack());
            staff.StopTimer();
            ActiveButtons = false;

        }
        

    }
    void CameraLerp(bool  flag)
    {
        dontTurnCamera = true;
        if(!lerpCalled)
        {
            lerpCalled = true;
            cam.lerpIn(flag, sequence);
        }
        
        
        
    }

    public void HandleAttackMode()
    {
        if(!enemiesDead)
        {
            HandleLockOn();
            if (!sequenceReady)
            {
                print("button sequecne");
                CreateButtonSequence();
                sequenceReady = true;
                timer_Started = false;
                curEnemy.finishedAttacking = true;
            }
            
            if (staff.fadeInFinished)
            {
              
                if(!staff.notesFade && curEnemy.finishedAttacking)
                {
                    print("1");
                    if(currentPhase == phases.Phase1 && !instructionShown)
                    {
                        StartCoroutine(WaitForInstructionScreen());
                    }
                    staff.FadeNotes(1, sequence);
                }
                if (staff.notesFade && sequenceReady)
                {
                    
                    if (!timer_Started && curEnemy.finishedAttacking && !player.isAttacking)
                    {
                        print("in here");
                        StartCoroutine(WaitForSequence());
                        ActiveButtons = true;
                    }
                    HandleSequencePhase();

                }

                
            }
            CameraLerp(true);




            if (curEnemy.dead && curEnemy.finishedAttacking && !player.isAttacking)
            {
               
                
                playerLockingOn = false;
                staff.FadeOutNotes();
                print("enemyDead");
                if (curEnemySetDeadCount == curEnemySetSize)
                {
                    if(currentPhase != phases.Phase4)
                    {
                        staff.FadeStaff(2, null);
                    }
                    
                    player.resetRotation((int)currentPhase);
                    enemiesDead = true;
                    cam.doneLerping = false;

                }
                if (!enemiesDead)
                {
                    print("killed one enemy");
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
        timer_Started = false;
        setMovingEnemies = false;
        lerpCalled = false;
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
           cam.lerpOut();

        }
        
       
    }


}
