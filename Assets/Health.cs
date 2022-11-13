using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    private NavMeshAgent AI_Agent;

    [SerializedField] public int hitpoints = 100;
    public Action<int, int> OnChange;

    public void Change(int amount)
    {
        hitpoints += amount;
        OnChange?.Invoke(hitpoints, amount);

        if (hitpoints <= 0)
        {
            transform.gameObject.tag = "Team_3";
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<newenemy>().enabled = false;
            gameObject.SetActive(false);
            hitpoints = -1;
            StopAllCoroutines();
            
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.gameObject.tag = "Team_1";
    }
}
