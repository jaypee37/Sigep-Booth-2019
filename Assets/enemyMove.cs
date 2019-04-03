using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyMove : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform loc;
    private bool moveFinished = false;
    Vector3 goal;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    IEnumerator WaitForPosition()
    {
        print("enemy is moving");
        yield return new WaitWhile(() => goal.magnitude > 2);
        moveFinished = true;
        print("enemy made it");
    }

    // Update is called once per frame
    void Update()
    {
        goal = transform.position - loc.position;
    }

    public void enemyMoveLoc()
    {
        StartCoroutine(WaitForPosition());
        agent.destination = loc.position;
    }

    public bool isMoveFinished()
    {
        return moveFinished;
    }
}
