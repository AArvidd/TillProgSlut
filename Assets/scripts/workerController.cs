using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class workerController : MonoBehaviour
{
    GameObject hand;

    GameObject target;
    bool holding = false;
    bool dropped  = false;

    NavMeshAgent agent;

    float timer = 3;

    int strength = 10;
    
    string[] tags = {"droppOff", "log", "tree"};

    int targetTag = 0; 

    // Start is called before the first frame update
    void Start()
    {
        hand = transform.GetChild(0).gameObject;
        agent = GetComponent<NavMeshAgent>();

    }


    // Update is called once per frame
    void Update()
    {

        if(target != null && timer <= 0 && dropped){
            target = null;
            dropped = false;
        }
        
        if(target == null){
            for (int i = 1; i < tags.Length; i++){
                if(setTarget(i))
                    break;
            }
        }

        if(targetTag == 1 && target?.transform.parent != null){
            target = null;
        }
        
        if(holding){
            setTarget(0);
        }
        
        bool arg;

        {
            Vector2 targetV2 = new Vector2(target.transform.position.x, target.transform.position.z);
            Vector2 workerV2 = new Vector2(transform.position.x, transform.position.z);
            arg = Vector2.Distance(workerV2, targetV2) < 2;
        } 

        if(arg){
            switch(targetTag){
                case 0 :
                    droppOff();
                    break;
                case 1 :
                    pickUpp();
                    break;
                case 2 :
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
        holding = true;
        target.transform.parent = hand.transform;
        target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        target.transform.localPosition = Vector3.zero;
        target.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private void droppOff(){
        holding = false;
        Transform held = transform.GetChild(0).transform.GetChild(0);
        held.parent = null;
        held.GetComponent<Rigidbody>(). constraints = RigidbodyConstraints.None;
        timer = 1;
        dropped = true;
        targetTag = -1;
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

            if(tag == 1 && targets[i].transform.parent != null){
                continue;
            }

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
        
        if(shortestI == -1){
            return false;
        }

        target = targets[shortestI];
        targetTag = tag;
        return target != null;
    }

}
