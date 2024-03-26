using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movementController : MonoBehaviour
{

    Vector2 inputVector = Vector2.zero;

    CharacterController charecter;

    [SerializeField]
    float spead = 5;

    [SerializeField]
    float jumpForse = 10;

    [SerializeField]
    float gravityMultiplyer = 4;

    float velosetyY = 0;

    bool jumpPrest = false;

    void Awake(){
        charecter = GetComponent<CharacterController>();
    }
    
    void Update()
    {

        Vector3 movement = transform.right * inputVector.x + 
            transform.forward * inputVector.y;

        movement *= spead;


        if(charecter.isGrounded){
            velosetyY = -1;
            if(jumpPrest){
                velosetyY = jumpForse;
            }
        }

        velosetyY += Physics.gravity.y * gravityMultiplyer * Time.deltaTime;

        movement.y = velosetyY;

        charecter.Move(movement * Time.deltaTime );
        jumpPrest = false;
    }

    void OnMove(InputValue value) => inputVector = value.Get<Vector2>();
    void OnJump(InputValue value) => jumpPrest = true;
}
