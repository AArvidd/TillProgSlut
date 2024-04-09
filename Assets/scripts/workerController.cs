using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.AI;

public class workerController : MonoBehaviour
{
    Vector3 hand;

    GameObject target;

    NavMeshAgent agent;

    float timer = 3;

    int strength = 10;
    
    string[] tags = {"log", "tree"};

    int targetTag = 0; 

    // Start is called before the first frame update
    void Start()
    {
        hand = transform.GetChild(0).position;
        agent = GetComponent<NavMeshAgent>();

    }


    // Update is called once per frame
    void Update()
    {
        if(target == null){
            for (int i = 0; i < tags.Length; i++){
                if(setTarget(i))
                    break;
            }
        }



        if(Vector3.Distance(target.transform.position, transform.position) < 2){

            switch(targetTag){
                case 0 :
                    pickUpp();
                break;
                case 1 :
                    if (timer <= 0){
                        attack();
                        timer = 1;
                    }
                    break;
            }

        }
        timer -= Time.deltaTime;

        agent.destination = target.transform.position;
    }

    private void pickUpp(){
        target.transform.position = hand;
        
    }

    private void attack(){
        treeCotroller script = target.GetComponent<treeCotroller>();
        script.takeDamage(strength);
    }
    

    private bool setTarget(int tag){
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tags[tag]);
        if(targets.Length == 0)
            return false;
        float shortest = 9999999999999999999;
        int shortestI = -1;
        for (int i = 0; i < targets.Length; i++){
            Vector3 curent = targets[i].transform.position;

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
        target = targets[shortestI];
        targetTag = tag;
        return target != null;
    }

}
