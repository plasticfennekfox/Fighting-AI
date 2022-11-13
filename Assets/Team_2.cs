using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class Team_2 : MonoBehaviour
{
    private NavMeshAgent AI_Agent;
    private GameObject[] targets;
    //
    private GameObject currentTarget = null;

    IEnumerator GetClosestTarget()
    {
        float tmpDist = float.MaxValue;
        //GameObject currentTarget = null;
        for (int i = 0; i < targets.Length; i++)
        {
            if (AI_Agent.SetDestination(targets[i].transform.position))
            {
                //ждем пока вычислится путь до цели
                while (AI_Agent.pathPending)
                {
                    yield return null;
                }
                Debug.Log(AI_Agent.pathStatus.ToString());
                // проверяем, можно ли дойти до цели
                if (AI_Agent.pathStatus != NavMeshPathStatus.PathInvalid)
                {
                    float pathDistance = 0;
                    //вычисляем длину пути
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
                    Debug.Log("невозможно дойти до " + targets[i].name);
                }

            }

        }
        if (currentTarget != null)
        {
            AI_Agent.SetDestination(currentTarget.transform.position);
            //... дальше ваша логика движения к цели

        }

    }

    void Start()
    {
        AI_Agent = gameObject.GetComponent<NavMeshAgent>();
        targets = GameObject.FindGameObjectsWithTag("Team_1");
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
                targets = GameObject.FindGameObjectsWithTag("Team_1");
                StartCoroutine(GetClosestTarget());
            }
        }
        //targets = GameObject.FindGameObjectsWithTag("Team_1");
        if (targets.Length == 0)
        {
            //transform.gameObject.tag = "Team_4";
            targets = GameObject.FindGameObjectsWithTag("Team_3");
            StartCoroutine(GetClosestTarget());
        }

    }
}