using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Team_1 : MonoBehaviour
{
    private NavMeshAgent AI_Agent;
    private GameObject[] targets;
    GameObject currentTarget = null;


    IEnumerator GetClosestTarget()
    {
        float tmpDist = float.MaxValue;
        GameObject currentTarget = null;
        //for (int i = 0; i < targets.Length; i++)
        for (int i = 0; i < 3; i++)
            {
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
       // targets = GameObject.FindGameObjectsWithTag("Team_2");
        targets = GameObject.FindObjectsOfType<GameObject>();
        StartCoroutine(GetClosestTarget());
    }

    void FixedUpdate()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            float Dist_Player = Vector3.Distance(currentTarget.transform.position, gameObject.transform.position);
            if (Dist_Player < 3)
            {
                currentTarget.SetActive(false);
                //targets = GameObject.FindGameObjectsWithTag("Team_2");
                StartCoroutine(GetClosestTarget());
            }
        }
        //targets = GameObject.FindGameObjectsWithTag("Team_2");
        if (targets.Length == 0)
        {
            //transform.gameObject.tag = "Team_3";
            targets = GameObject.FindGameObjectsWithTag("Team_3");
            StartCoroutine(GetClosestTarget());

        }

    }
}

