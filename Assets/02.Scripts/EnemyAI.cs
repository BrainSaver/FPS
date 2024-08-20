using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }

    public State state = State.PATROL;

    Transform playerTr;
    Transform enemyTR;

    public float attackDist = 5f;
    public float traceDist = 10f;

    public bool isDie = false;

    WaitForSeconds ws;

    MoveAgent moveAgent;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        if(player != null)
        {
            playerTr = player.GetComponent<Transform>();
        }
        enemyTR = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();

        ws = new WaitForSeconds(0.3f);
    }

    private void OnEnable()
    {
        //코르틴은 항상 이렇게 호출해야함!!
        StartCoroutine("CheckState");

    }

    IEnumerator CheckState()
    {
        while (!isDie)
        {
            if (state == State.DIE)
            {
                yield break;
            }
            float dist = Vector3.Distance(playerTr.position, enemyTR.position);

            if (dist <= attackDist)
            {
                state = State.ATTACK;
            }
            else if (dist <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PATROL;
            }
            yield return ws;
        }
    }

    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;
        }
        switch (state)
        {
            case State.PATROL:
                moveAgent.patrolling = true;
                break;
            case State.TRACE:
                moveAgent.traceTarget = playerTr.position;
                break;
            case State.ATTACK:
                moveAgent.Stop();
                //멈춘 후 공격
                break;
            case State.DIE:
                moveAgent.Stop();
                break;
        }
    }
}
