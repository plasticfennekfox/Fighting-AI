using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class newenemy : MonoBehaviour
{
    private NavMeshAgent AI_Agent;
    private GameObject[] targets;
    GameObject currentTarget;
    GameObject[] Target1;
    IEnumerator GetClosestTarget()
    {
        float tmpDist = float.MaxValue;
        // GameObject currentTarget = null;
        for (int i = 0; i < (targets.Length); i++)
        {
            if (targets[i] == this.gameObject) continue;
            if (AI_Agent.SetDestination(targets[i].transform.position))
            {
                //���� ���� ���������� ���� �� ����
                while (AI_Agent.pathPending)
                {
                    yield return null;
                }
                Debug.Log(AI_Agent.pathStatus.ToString());
                // ���������, ����� �� ����� �� ����
                if (AI_Agent.pathStatus != NavMeshPathStatus.PathInvalid)
                {
                    float pathDistance = 0;
                    //��������� ����� ����
                    pathDistance += Vector3.Distance(transform.position, AI_Agent.path.corners[0]);
                    for (int j = 1; j < AI_Agent.path.corners.Length; j++)
                    {
                        pathDistance += Vector3.Distance(AI_Agent.path.corners[j - 1], AI_Agent.path.corners[j]);
                    }

                    if (tmpDist > pathDistance)
                    {
                        tmpDist = pathDistance;
                        currentTarget = targets[i];
                        AI_Agent.ResetPath();
                    }
                }
                else
                {
                    Debug.Log("���������� ����� �� " + targets[i].name);
                }
            }

        }
        if (currentTarget != null)
        {
            AI_Agent.SetDestination(currentTarget.transform.position);
            //... ������ ���� ������ �������� � ����

        }
    }

    void Start()
    {
        AI_Agent = gameObject.GetComponent<NavMeshAgent>();
        targets = GameObject.FindGameObjectsWithTag("Team_1");
        StartCoroutine(GetClosestTarget());

    }

    void Update()
    {
        for (int i = 0; i < targets.Length; i++)
        {

            transform.gameObject.tag = "Team_1";
            targets = GameObject.FindGameObjectsWithTag("Team_1");
            AI_Agent.Stop();

            for (i = 0; i < targets.Length; i++)
            {
                float Dist_Player = Vector3.Distance(currentTarget.transform.position, this.gameObject.transform.position);
                if (Dist_Player < 2f)     //�������� � �������
                {
                    StartCoroutine(GetClosestTarget());
                    //AI_Agent.Stop();
                    transform.gameObject.tag = "Team_2";
                    targets = GameObject.FindGameObjectsWithTag("Team_2");


                }

            }

            StartCoroutine(GetClosestTarget());

        }

    }
}
