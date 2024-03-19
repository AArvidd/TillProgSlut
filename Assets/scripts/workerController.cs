using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.AI;

public class workerController : MonoBehaviour
{

    GameObject target;

    NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
            setTarget();
        agent.destination = target.transform.position;
        attack();
    }

    private void attack(){
        //todo make atach tree

    }
    

    private void setTarget(){
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        float shortest = 9999999999999999999;
        int shortestI = -1;
        for (int i = 0; i < trees.Length; i++){
            Vector3 curent = trees[i].transform.position;
            agent.destination = curent;
            float testLenth = agent.remainingDistance;
            if(testLenth < shortest){
                shortest = testLenth;
                shortestI = i;
            }
        }
        target = trees[shortestI];
    }

}
