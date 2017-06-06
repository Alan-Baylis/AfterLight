using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    
    public float jumpBackRange = 6;
    public Vector3 destination;
    Transform player;
    NavMeshAgent agent;
    [SerializeField]bool isRunning;

     void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {

        if (!isRunning)
        {
            agent.destination = player.position;
        }
        else
        {
            agent.SetDestination(destination);
        }
        if (isRunning && agent.remainingDistance < 5) isRunning = false;
        
       
    }

    public void JumpBack()
    {
        isRunning = true;
        Vector3 back = transform.TransformPoint(Vector3.back * jumpBackRange);
        agent.SetDestination(destination);
        destination = back;
    }
    //IEnumerator Wait()
    //{
    //    print(this + " is waiting");
    //    while(agent.remainingDistance > .5)
    //    {
    //        print(agent.remainingDistance);
    //        yield return null;
    //    }
    //    print(this + " later");
    //    isRunning = false;
    //    //destination = player.position;
    //}
}
