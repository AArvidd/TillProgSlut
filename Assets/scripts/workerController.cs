using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        print(agent.remainingDistance);
    }

    private void setTarget(){
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        for (int i = 0; i < trees.Length; i++){
            //TODO afindes the closest tree
        }
    }

}
