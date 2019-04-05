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
   
    }

    


    // Update is called once per frame
    void Update()
    {
        switch(currentPhase)
        {
            case phases.Phase1:
                curEnemySetSize = 3;
                HandlePlayerPhase();
                if (playerPhaseDone = player.isMoveFinished())
                {
                    HandleEnemyPhase(currentPhase);
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
                    player.transform.Rotate(Vector3.right * Time.deltaTime);
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
                    HandleEnemyPhase(currentPhase);
                }
                if (enemyMovingPhase)
                {
                    HandleAttackMode();
                }
                break;
        }
        
        
       

    }

    void HandlePlayerPhase()
    {
        
        if (Input.GetKeyDown("1"))
        {         
            player.playerMoveLoc(playerLocIndex);
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
                if(e != null)
                {
                    e.enemyMoveLoc();
                }
                
            }
            enemiesMoving = true;
        }

        bool flag = true;
        foreach (enemyMove e in curEnemySet)
        {
            if(e != null)
            {
                flag = flag && e.isMoveFinished();
            }
            
        }
        if (flag == true)
        {
            enemyMovingPhase = true;
        }
        
    }

    public void HandleAttackMode()
    {

        if (Input.GetKeyDown("2"))
        {
            for (int i = 0; i < curEnemySetSize; i++)
            {
                if (curEnemySet[i] != null && curEnemySet[i].isLockedOn())
                {
                    curEnemySet[i].Die();
                    curEnemySet[i] = null;

                    curEnemySetDeadCount++;
                    playerLockingOn = false;
                }
            }

            if(curEnemySetDeadCount == curEnemySetSize)
            {
                print("reseting for next phase");
                resetForNextPhase();
            }
            
        }

        if (Input.GetKeyDown("space"))
        {
            if(!playerLockingOn)
            {
                int i = (int)(Random.Range(0, curEnemySetSize));
                print(i);
                while (curEnemySet[i] == null || curEnemySet[i].isLockedOn())
                {
                    i = (int)(Random.Range(0, curEnemySetSize));
                }
                print(i);
                curEnemySet[i].LockOn();
                playerLockingOn = true;
            }
            
        }
    }


    public void resetForNextPhase()
    {
        enemiesMoving = false;
        playerPhaseDone = false;
        enemyMovingPhase = false;
        player.setMoveFinished(false);
        playerLocIndex += 1;
        currentPhase += 1;
        curEnemySetDeadCount = 0;

    }


}
