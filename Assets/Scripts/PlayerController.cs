using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
   float _baseSpeed = 10.0f;
   float _gravidade = 9.8f;
   GameObject playerCamera;
   float cameraRotation;

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
       
       //Verificando se é preciso aplicar a gravidade
       float y = 0;
       if(!characterController.isGrounded){
           y = -_gravidade;
       }
       
       Vector3 direction = new Vector3(x, y, z);

       cameraRotation += mouse_dY;
       Mathf.Clamp(cameraRotation, -75.0f, 75.0f);
       
       characterController.Move(direction * _baseSpeed * Time.deltaTime);
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