using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Damage : MonoBehaviour
{
    private GameObject[] targets;
    private NavMeshAgent AI_Agent;

    [SerializedField] public int amount = -10;
    public Action<int> OnDamage;

    private void OnTriggerEnter(Collider collider)
    {
        transform.gameObject.tag = "Team_2";

        StartCoroutine(ToDamage(collider));
    }

    private IEnumerator ToDamage(Collider collider)
    {
        while (gameObject.activeSelf==true)
        {
            collider.gameObject.GetComponent<Health>().Change(amount);
            OnDamage?.Invoke(amount);
            //if (gameObject.activeSelf == true) transform.gameObject.tag = "Team_1";
            yield return new WaitForSeconds(0.5f);
            
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Team_2")
            StopCoroutine(ToDamage(collider));//
        targets = GameObject.FindGameObjectsWithTag("Team_1");

    }


}
