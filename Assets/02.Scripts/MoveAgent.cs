using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int nextIdx;

    NavMeshAgent agent;

    readonly float patrolSpeed = 1.5f;
    readonly float traceSpeed = 4.0f;

    bool _patrolling;
    public bool patrolling
    {
        //저장된 값을 가져오는건 get, 값을 저장하는건 set
        get { return _patrolling; }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrolSpeed;
                MoveWayPoint();
            }
        }
    }

    Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            TraceTarget(_traceTarget);
        }
    }

    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
            return;

        agent.destination = pos;
        agent.isStopped = false;
        agent.speed = patrolSpeed;
    }

    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent는 목표 지점에 도착할 때 서서히 느려지고 출발할때 서서히 빨라짐
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;//이걸 하면 가속, 감속현상을 없애줌

    var group = GameObject.Find("WayPoints");
        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);
        }
        MoveWayPoint();
    }

    void MoveWayPoint()
    {
        if (agent.isPathStale)//(isPathStale)목적지 경로 계산중일땐 목적지를 바꾸지 않음
            return;

        agent.destination = wayPoints[nextIdx].position;
        agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)//속도가 조금이라도 있고, 목적지에 거의 도착했을 때
        {
            nextIdx++;
            nextIdx %= wayPoints.Count;
            MoveWayPoint();
        }
    }
}
