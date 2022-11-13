using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TakeDamage : MonoBehaviour
{
    public int HP = 100;

    GameObject currentTarget;
    private GameObject[] targets;
    private NavMeshAgent AI_Agent;
    public int damage;
    private static GameObject enemy;

    private void Start()
    {
        //AI_Agent = gameObject.GetComponent<NavMeshAgent>();
       
    }

    private void Update()
    {
        targets = GameObject.FindGameObjectsWithTag("Team_2");
              

        if (HP <= 0)
        {
            transform.gameObject.tag = "Team_3";
            gameObject.SetActive(false);
            StopAllCoroutines();
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        int i = targets.Length;

        for (i = 0; i < targets.Length; i++)
        {
            //if (targets[i] == this.gameObject) continue;
            currentTarget = targets[i];
        }

        if (collider.gameObject.tag== "Team_2")
        
        {
            StartCoroutine(ToDamage(currentTarget));
        }
    }

    private IEnumerator ToDamage(GameObject currentTarget)
    {
        while (HP > 0)
        {
            //gameObject.GetComponent<Enemy>().TakeDamage(damage);
            GameObject.FindWithTag("Team_2");
            HP -= damage;
            yield return new WaitForSeconds(0.5f);
        }
    }



    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Team_2")
            StopAllCoroutines(); 
    }

}
