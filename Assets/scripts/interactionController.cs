using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionController : MonoBehaviour
{
    GameObject cameraObjekt;

    GameObject lookTarget;

    treeCotroller script;

    int damage = 15;

    void Awake()
    {
        cameraObjekt = GetComponentInChildren<Camera>().gameObject;
    }

    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(
            cameraObjekt.transform.position,
            cameraObjekt.transform.forward,
            out hit,
            5f
        );

        lookTarget = hit.collider != null ? hit.collider.gameObject : null;

        script = lookTarget?.transform.parent?.GetComponent<treeCotroller>(); 

    }
    void OnFire(){
        
        script?.takeDamage(damage);

    }
}
