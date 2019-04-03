using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public playerMove player;
    public enemyMove[] Enemies = new enemyMove[3];
    public Vector3 goal;

    enum phases
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4
    }
    phases currentPhase;
    bool playerPhase1Done = false;
    bool enemiesMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        currentPhase = phases.Phase1;
   
    }

    IEnumerator WaitForPosition()
    {
        
        print("waiting til he reaches location");
        yield return new WaitWhile(() => goal.magnitude > 0.2f);
        player.animator.SetBool("Idling", true);
        player.animator.SetBool("Running", false);
        playerPhase1Done = true;

        print("he made it");
    }


    // Update is called once per frame
    void Update()
    {
        switch(currentPhase)
        {
            case phases.Phase1:
                
                HandlePlayerPhase1();
                if (playerPhase1Done)
                {
                    
                    HandleEnemyPhase1();
                }
                break;
            case phases.Phase2:
                print("phase2");
                break;
        }
        
        
       

    }

    void HandlePlayerPhase1()
    {
        
        if (Input.GetKeyDown("1"))
        {
            player.animator.SetBool("Idling", false);
            player.animator.SetBool("Running", true);
            StartCoroutine(WaitForPosition());           
            player.playerMoveLoc1();
        }
        
        goal = player.transform.position - player.loc1.transform.position;
        



    }

    void HandleEnemyPhase1()
    {
        
        enemyMove E1 = Enemies[0];
        enemyMove E2 = Enemies[1];
        enemyMove E3 = Enemies[2];
        if(!enemiesMoving)
        {
            E1.enemyMoveLoc();
            E2.enemyMoveLoc();
            E3.enemyMoveLoc();
            enemiesMoving = true;
        }
        

        if (E1.isMoveFinished() && E2.isMoveFinished() && E3.isMoveFinished())
        {
            currentPhase = phases.Phase2;
        }
    }
    



}
