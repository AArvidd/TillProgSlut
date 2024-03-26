using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionController : MonoBehaviour
{
    GameObject cameraObjekt;

    GameObject lookTarget;

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
            3f
        );

        lookTarget = hit.collider != null ? hit.collider.gameObject : null;

        //treeCotroller script = lookTarget.transform.parent.GetComponent<treeCotroller>; todo

    }
    void OnFire(){
        
        lookTarget?.SendMessage(
            "OnInteract",
            SendMessageOptions.DontRequireReceiver);
    }
}
