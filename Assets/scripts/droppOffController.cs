using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droppOffController : MonoBehaviour
{

    [SerializeField]
    GameObject spawner;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "log"){
            spawner.GetComponent<SpanerController>().add(other.gameObject.GetComponent<LogData>().value);
            Destroy(other.gameObject);
        }
    }

}
