using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
   float _baseSpeed = 10.0f;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
   GameObject playerCamera;
   float cameraRotation;
   private Vector3 moveDirection = Vector3.zero;
   private int jumps = 0;

   private float vibration = 0;

   CharacterController characterController;

   void Start()
   {
       characterController = GetComponent<CharacterController>();
       playerCamera = GameObject.Find("Main Camera");
       cameraRotation = 0.0f;
       Cursor.lockState = CursorLockMode.Locked;
   }

   void Update()
   {
       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");

       float mouse_dX = Input.GetAxis("Mouse X");
       float mouse_dY = Input.GetAxis("Mouse Y")*-1;

       if(Input.GetKey("left shift") && characterController.isGrounded){
           mouse_dY += Mathf.Sin(vibration)/20;
           _baseSpeed = 15.0f;
           vibration += 0.1f;
       } else{
           vibration = 0.0f;
           _baseSpeed = 10.0f;
       }

        if (characterController.isGrounded) {
            jumps = 0;
            moveDirection = new Vector3(x, 0, z);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= _baseSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumps < 2){
            if(Input.GetKey("left shift") && characterController.isGrounded){
                jumpSpeed = 12.0f;
            } else{
                jumpSpeed = 8.0f;
            }
            moveDirection.y = jumpSpeed;
            jumps ++;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);


       cameraRotation += mouse_dY;
       Mathf.Clamp(cameraRotation, -75.0f, 75.0f);
       
       transform.Rotate(Vector3.up, mouse_dX);


       playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
   }

    void LateUpdate()

    {
    RaycastHit hit;
    //Debug.DrawRay(playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), Color.magenta);
    if(Physics.Raycast(playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
    {
        //Debug.Log(hit.collider.name);
    }
    }
}