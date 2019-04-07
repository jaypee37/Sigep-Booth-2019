using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackManager : MonoBehaviour
{
    enemyMove[] curEnemySet;
    int setSize;
    bool attacking;
    bool sentAttackRequest;
    bool enemyFinished;
    enemyMove curAttackingEnemy;
    bool waitingForAttack = false;
    bool allDead = false;
    bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        sentAttackRequest = false;
        enemyFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activated)
        {
            if (CountDead() == setSize)
            {
                activated = false;
            }

            if (waitingForAttack && !allDead && activated)
            {
                if (enemyFinished)
                {
                    waitingForAttack = false;
                    ChooseAttackingEnemy();
                }
            }
            
        }
        
    }

    public void InitCurrentEnemySet(enemyMove[] enemySet, int size)
    {
        print("init");
        curEnemySet = enemySet;
        setSize = size;
        ChooseAttackingEnemy();
        allDead = false;
        activated = true;
    }

    public enemyMove ChooseAttackingEnemy()
    {
        //print("choosingcharacter");
        int i = Random.Range(0, setSize);
        //print(i);
        while (curEnemySet[i].dead)
        {
            i = (int)(Random.Range(0, setSize));
        }
        curAttackingEnemy = curEnemySet[i];
        curEnemySet[i].Attack();
        sentAttackRequest = true;
        enemyFinished = false;
        waitingForAttack = true;
        return null;
    }

    public int CountDead()
    {
        int count = 0;
        foreach( enemyMove e in curEnemySet)
        {
            if(e.dead)
            {
                count += 1;
            }
        }
        return count;
    }

    public void enemyFinishedAttacking()
    {
        enemyFinished = true;
    }
}
