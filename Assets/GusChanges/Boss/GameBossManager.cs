using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameBossManager : MonoBehaviour
{

    public playerMove player;
    //public bossMove[] curBossSet;
    public bool bossMoving = false;
    public int playerLocIndex = 0;
    public GameObject bossSet;
    public int curBossSetSize = 0;
    public int curBossSetDeadCount = 0;
    bool playerLockingOn = false;
    public float attackRate = 1F;
    public float timestamp = 0F;
    Quaternion curPlayerRotation = Quaternion.Euler(0, 0, 0);
    Vector3 curCamPos;
    public bool playerAttacking;
    public armMove AttackAnimBehavior;
    bool changeOfDirection = false;
    int currentBossIndex;
    int sequenceIndex = 0;
    string[] buttons = { "Green", "Red", "Yellow", "Blue" };
    string[] sequence;
    bool sequenceFinished = false;
    bool timeRanOut = false;
    bool waitingForSequence = false;
    bool DeLaCruzMoved = false;
    public BossDeLaCruzMvt DeLaCruz;
    bool bossHit = false;
    public NoteStaff staff;
    bool sequenceReady = false;
    string[] notesForStaff;
    bool running = false;
    public Canvas winScreen;
    public Canvas instructionScreen;
    bool instructionShown = false;
    bool timer_Started = false;
    public TextMeshProUGUI numberText;
    Vector3 goal;
    bool appeared = false;
    public TestFader TestFader;
    bool finishedCurrentAttack = true;
    bool startStaffFaded = false;
    bool gameEnded = false;
    int maxSequenceNumber;


    //Phases are now what form of attack he is in
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
    bool bossMovingPhase = false;

    void SetDifficulty(SceneHandler.Difficulty difficulty)
    {
        switch (difficulty)
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

    // Start is called before the first frame update
    void Start()
    {
        SetDifficulty(SceneHandler.instance.difficulty);
        currentPhase = phases.Phase1;
        Input.ResetInputAxes();
        running = true;
        
       

    }

    IEnumerator WaitForSequence(int waitAmount)
    {
        staff.FadeTimerNumbersInandOut();
        timer_Started = true;
        //Debug.Log("test");

        for (int i = 1; i <= waitAmount; i++)
        {
            yield return new WaitForSeconds(i);
            print(i);
        }

        timer_Started = false;
        timeRanOut = true;
        waitingForSequence = false;
        //StopAllCoroutines();
    }
    
    //Actually only damages him
    IEnumerator WaitForBossDeath()
    {
        yield return new WaitWhile(() => player.isAttacking);
        //curBoss.takeDamage();

    }
    IEnumerator WaitForInstructionScreen()
    {
        instructionShown = true;
        yield return new WaitWhile(() => player.isAttacking);
        //curBoss.takeDamage();
    }

    // Update is called once per frame
    void Update()
    {
//        if (staff.fadeInFinished == true) Debug.Log("Test!!!");

        if (running)
        {
            
            if (!startStaffFaded)
            {
                staff.FadeStaff(1, null);
                startStaffFaded = true;
                sequenceFinished = false;
            }
            switch (currentPhase)
            {
                case phases.Phase1:
                    curBossSetSize = 1;

                    if (!DeLaCruzMoved)
                    {
                        HandleDeLaCruzPhase();
                        player.playerRot = player.transform.rotation;
                    }
                    HandleAttackMode();
                    break;
                case phases.Phase2:
                    HandleAttackMode();
                    break;
                case phases.Phase3:
                    HandleAttackMode();
                    break;
                default:      
                    if(!gameEnded)
                    {
                        gameEnded = true;
                        SceneHandler.instance.SetFadedAndCanvas(true, winScreen);
                        SceneHandler.instance.ChangeScene(SceneHandler.Scene.Win);
                    }
                   
                    break;
            }
        }
    }

    void HandleDeLaCruzPhase()
    {
        bossMovingPhase = true;
        playerPhaseDone = false;
        staff.notesFade = false;
        staff.fadeInFinished = false;
        DeLaCruzMoved = DeLaCruz.Move();
        DeLaCruzMoved = true;

    }

    public void CreateButtonSequence()
    {

        sequence = new string[maxSequenceNumber];
        for (int i = 0; i < maxSequenceNumber; i++)
        {
            sequence[i] = buttons[(int)Random.Range(0, 4)];
        }
        for (int i = 0; i < maxSequenceNumber; i++)
        {
            print(sequence[i]);
        }
        //Display Buttons Functions

    }

    public void HandleSequencePhase()
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


                if (curColor == sequence[sequenceIndex] || Input.GetButtonDown("Submit"))
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
                finishedCurrentAttack = true;
                sequenceIndex = 0;
                StopAllCoroutines();
                staff.FadeOutNotes();
                bossHit = true;
                player.Attack();
            
                //StartCoroutine(WaitForBossDeath()); REPLACE WITH STUN PROBABLY CHANGE COROUTINE CODE WITH STUN
            }


    }

    public void HandleAttackMode()
    {
        Debug.Log("1");
        if (!bossHit)
        {
            //Debug.Log("1");
            if (!sequenceReady)
            {
                print("button sequecne");
                CreateButtonSequence();
                sequenceReady = true;
                timer_Started = false;
            }
           
            if (!staff.notesFade && finishedCurrentAttack)
            {
                Debug.Log("3");
                if (currentPhase == phases.Phase1 && !instructionShown)
                {
                    StartCoroutine(WaitForInstructionScreen());
                }
                    
                staff.FadeNotes(1, sequence);
                staff.notesFade = true;
                Debug.Log("2");
                finishedCurrentAttack = false;
            }
            print("here");
            print(staff.notesFade);
            print(sequenceReady);
            if (staff.notesFade && sequenceReady)
            {
                Debug.Log("4");
                HandleSequencePhase();               

            }
        }

        if (bossHit)
        {
            Debug.Log("Boss was hit");
            DeLaCruz.Stunned();
            resetForNextPhase();
        }

        //Debug.Log("Done technically");
    }

    void resetVariables()
    {
        Debug.Log("RESETTING");
        bossMoving = false;
        appeared = false;
        finishedCurrentAttack = true;
        playerPhaseDone = false;
        bossMovingPhase = false;
        player.setMoveFinished(false);
        playerLocIndex += 1;
        if ((int)currentPhase < 4)
        {
            currentPhase += 1;
        }

        curBossSetDeadCount = 0;
        //dontTurnCamera = false;
        DeLaCruzMoved = false;
        bossHit = false;
        sequenceReady = false;
        //staff.fadeInFinished = false;
        timer_Started = false;
        staff.notesFade = false;
    }
    public void resetForNextPhase()
    {
            resetVariables();

    }


}
