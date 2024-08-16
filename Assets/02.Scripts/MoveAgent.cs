using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int nextIdx;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        //NavMeshAgent�� ��ǥ ������ ������ �� ������ �������� ����Ҷ� ������ ������
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;//�̰� �ϸ� ����, ���������� ������

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
        if (agent.isPathStale)//(isPathStale)������ ��� ������϶� �������� �ٲ��� ����
            return;

        agent.destination = wayPoints[nextIdx].position;
        agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)//�ӵ��� �����̶� �ְ�, �������� ���� �������� ��
        {
            nextIdx++;
            nextIdx %= wayPoints.Count;
            MoveWayPoint();
        }
    }
}
