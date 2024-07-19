using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour, Health.IHealthListener
{

    enum State
    {
        Idle,
        Walk, 
        Attack,
        Dying
    };



    public GameObject player;
    NavMeshAgent agent;
    Animator animator;

    State state;
    float timeForNextState = 2;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.Idle:
                float distance = (player.transform.position -
                    (transform.position + GetComponent<CapsuleCollider>().center)).magnitude;
                if (distance < 1.0f)
                {
                    Attack();
                }
                else
                {
                    timeForNextState -= Time.deltaTime;
                    if (timeForNextState < 0)
                    {
                        StarWalk();

                    }
                }
                break;
            case State.Walk:
                if (agent.remainingDistance < 1.0f || !agent.hasPath)
                {
                    StartIdle();
                }
                break;
            case State.Attack:
                timeForNextState -= Time.deltaTime;
                if (timeForNextState < 0)
                {
                    StartIdle();
                }
                break;
        }


        if (timeForNextState < 0)
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Walk:
                    break;

            }
        }
    }

    void Attack()
    {
        state = State.Attack;
        timeForNextState = 1.5f;
        animator.SetTrigger("Attack");
    }

    void StarWalk()
    {
        state = State.Walk;
        agent.destination = player.transform.position;
        agent.isStopped = false;
        animator.SetTrigger("Walk");
    }

    void StartIdle()
    {
        state = State.Idle;
        timeForNextState = Random.Range(1f, 2f);
        agent.isStopped = true;
        animator.SetTrigger("Idle");
    }

    public void Die()
    {
        Debug.Log("Die");
    }

}
