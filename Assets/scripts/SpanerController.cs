using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpanerController : MonoBehaviour
{

    [SerializeField]
    GameObject worker;

    int ekon;

    public void add(int value){
        ekon += value;

        int wirth = worker.GetComponent<workerController>().getValue();

        if (wirth <= ekon){
            ekon -= value;
            Instantiate(worker, transform.position, Quaternion.identity);
        }

    }

}
