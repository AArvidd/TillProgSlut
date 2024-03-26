using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.AI;

public class workerController : MonoBehaviour
{

    GameObject target;

    NavMeshAgent agent;

    float timer = 3;

    int strength = 10;


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
        if(Vector3.Distance(target.transform.position, transform.position) < 2 && timer <= 0){
            attack();
            timer = 1;
        }
        timer -= Time.deltaTime;

        agent.destination = target.transform.position;
    }

    private void attack(){
        treeCotroller script = target.GetComponent<treeCotroller>();
        script.takeDamage(strength);
    }
    

    private void setTarget(){
        GameObject[] trees = GameObject.FindGameObjectsWithTag("tree");
        float shortest = 9999999999999999999;
        int shortestI = -1;
        for (int i = 0; i < trees.Length; i++){
            Vector3 curent = trees[i].transform.position;

            NavMeshPath path = new NavMeshPath();
            if (!NavMesh.CalculatePath(transform.position, curent, NavMesh.AllAreas, path)){
                continue;
            }

            float testLenth = 0;
            Vector3 point0 = path.corners[0];
            Vector3 point1;

            for(int j = 1; j < path.corners.Length; j++){
                point1 = path.corners[j];
                testLenth += Vector3.Distance(point0, point1);
                point0 = point1;
            }

            if(testLenth < shortest){
                shortest = testLenth;
                shortestI = i;
            }

        }
        target = trees[shortestI];
    }

}
