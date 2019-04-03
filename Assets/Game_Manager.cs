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


    // Start is called before the first frame update
    void Start()
    {
        currentPhase = phases.Phase1;
    }

    IEnumerator WaitForPosition()
    {
        
        print("waiting til he reaches location");
        yield return new WaitWhile(() => goal.magnitude > 2);
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
        }
        
        
       

    }

    void HandlePlayerPhase1()
    {
        
        if (Input.GetKeyDown("1"))
        {       
            StartCoroutine(WaitForPosition());
            player.playerMoveLoc1();
        }
        goal = player.transform.position - player.loc1.transform.position;
    }

    void HandleEnemyPhase1()
    {
        Enemies[0].enemyMoveLoc();
        Enemies[1].enemyMoveLoc();
        Enemies[2].enemyMoveLoc();
    }
    



}
