using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class lookController : MonoBehaviour
{

    GameObject head;

    float xCameraRoatation = 0;

    [SerializeField]
    Vector2 sensitivity = Vector2.one;

    [SerializeField]
    float viewAngleLimet = 80;

    private void Awake() {
        head = GetComponentInChildren<Camera>().gameObject;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void OnLook(InputValue value){
        Vector2 lookVektor = value.Get<Vector2>();

        // Horisontal
        float degreesY = lookVektor.x * sensitivity.x;
        transform.Rotate(Vector3.up, degreesY);

        //Vertical
        float degreesX = -lookVektor.y * sensitivity.y;
        xCameraRoatation += degreesX;

        xCameraRoatation = Mathf.Clamp(xCameraRoatation, -viewAngleLimet, viewAngleLimet);

        head.transform.localEulerAngles = new(xCameraRoatation, 0, 0);
    }
}
