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


    // Start is called before the first frame update
    void Start()
    {
        currentPhase = phases.Phase1;
        Input.ResetInputAxes();
        running = false;
        StartCoroutine(WaitForAwake());
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
    IEnumerator WaitForAwake()
    {
        yield return new WaitForSeconds(3);
        running = true;
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

    IEnumerator WaitForFade()

    {
        appeared = true;
         //ayy lmao
        staff.Fade(1, sequence);
        //Debug.Log("Fading");
        yield return new WaitForSeconds(2);
    }

    IEnumerator WaitForLerp()
    {
        staff.Fade(2, null);
        yield return new WaitForSeconds(4);
        //yield return new WaitWhile(() => goal2.magnitude > 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
//        if (staff.fadeInFinished == true) Debug.Log("Test!!!");

        if (running)
        {
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
                    //Debug.Log("Done");
                    
                    break;
                case phases.Phase2:
                    HandleAttackMode();
                    break;
                case phases.Phase3:
                    HandleAttackMode();
                    break;
                default:
                    
                    SceneHandler.instance.SetFadedAndCanvas(true, winScreen);
                    SceneHandler.instance.ChangeScene(SceneHandler.Scene.Win);
                    break;
            }
        }
    }

    void HandleDeLaCruzPhase()
    {
        bossMovingPhase = true;
        playerPhaseDone = false;
        //bossManager.InitCurrentBossSet(curBossSet, curBossSetSize);
        staff.notesFade = false;
        staff.fadeInFinished = false;
        DeLaCruzMoved = DeLaCruz.Move();

       
        //bossSet = GameObject.FindGameObjectWithTag("BossSet1");
        //curBossSet = new bossMove[1];
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

    }

    public void HandleSequencePhase()
    {

        if (!timeRanOut)
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
        /*else

        {
            sequenceFinished = false;
            finishedCurrentAttack = true;
            sequenceIndex = 0;
            print("you lost bitch");
            timeRanOut = false;
            //curBoss.Attack();
            sequenceReady = false;
            staff.notesFade = false;
            //StopCoroutine(WaitForSequence(5));
        }*/

    }


    //Sometimes fadeInFinished doesn't get turned to true CANNOT FOR THE LIFE OF ME FIGURE OUT WHY
    //MAYBE FIXED?
    public void HandleAttackMode()
    {
        //Debug.Log("1");
        if (!bossHit)
        {
            //Debug.Log("1");
            if (!sequenceReady)
            {
                //Debug.Log("1");
                print("button sequecne");
                CreateButtonSequence();
                sequenceReady = true;
                timer_Started = false;
            }
            //Debug.Log(staff.fadeInFinished);
            if (staff.fadeInFinished)
            {
                Debug.Log(!staff.notesFade);
                if (!staff.notesFade && finishedCurrentAttack)
                {
                    //Debug.Log("3");
                    if (currentPhase == phases.Phase1 && !instructionShown)
                    {
                        StartCoroutine(WaitForInstructionScreen());
                    }
                    
                    staff.FadeNotes(1, sequence);
                    //Debug.Log("2");
                    finishedCurrentAttack = false;
                }
                if (staff.notesFade && sequenceReady)
                {
                    //Debug.Log("3");
                    HandleSequencePhase();
                    if (!timer_Started)
                    {
                        //print("in here");
                        //StartCoroutine(WaitForSequence(5));
                    }

                }


            }
            if (appeared == false) {
                TestFader.fade(1, sequence);
                appeared = true;
                //Debug.Log("Nice!");
            }

            /*if (timer_Started)
            {
                Debug.Log("WTF!!");
                //curBossSetDeadCount++;
                playerLockingOn = false;
                staff.FadeOutNotes();
                print("BossDead");
                if (curBossSetDeadCount == curBossSetSize)
                {
                    player.resetRotation((int)currentPhase);
                    bossHit = true;
                    //cam.doneLerping = false;

                }
                if (!bossHit)
                {

                    staff.notesFade = false;
                    staff.FadeNotes(1, sequence);
                    sequenceReady = false;
                }
            }*/

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
            //cam.StopAllCoroutines();
            //cam.doneLerping = true;
    }


}
