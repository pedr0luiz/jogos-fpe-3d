using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
   float _baseSpeed = 10.0f;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
   GameObject playerCamera;
   GameManager gm;
   float cameraRotation;
   private Vector3 moveDirection = Vector3.zero;
   private int jumps = 0;
   private float vibration_time = 0.0f;

   private float vibration = 0;
   public Text hint;
   public Text inventario;

   public GameObject bombDefuseHint;

   CharacterController characterController;

   void Start()
   {
        if (gm == null) {
            gm = GameManager.GetInstance();
        }
       characterController = GetComponent<CharacterController>();
       playerCamera = GameObject.Find("Main Camera");
       cameraRotation = 0.0f;
       Cursor.lockState = CursorLockMode.Locked;
   }

   void Update()
   {
       if (gm.game_paused){
           Cursor.lockState = CursorLockMode.None;
           return;
       }

       Cursor.lockState = CursorLockMode.Locked;

       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");

       float mouse_dX = Input.GetAxis("Mouse X");
       float mouse_dY = Input.GetAxis("Mouse Y")*-1;

       vibration_time += Time.deltaTime;

       if(Input.GetKey("left shift") && characterController.isGrounded){
           _baseSpeed = 15.0f;
           if(vibration_time > 0.005f){
            mouse_dY += Mathf.Sin(vibration)/20;
            vibration += 0.1f;  
            vibration_time = 0.0f;
           }
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
    if(Physics.Raycast(playerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, 4.0f))
    {
        if(hit.collider.name == "Pergaminho" || (hit.collider.name == "TicketBarrier" && gm.has_read_hint)){
            hint.text = "Pressione F para pegar " + hit.collider.name;
        }
        if(Input.GetKeyDown(KeyCode.F)){
            switch(hit.collider.name){
                case "TicketBarrier":
                    if(gm.has_read_hint){
                        Destroy(hit.collider.gameObject);
                        gm.got_knife();
                        inventario.text = "Faca: ✔";
                        inventario.color = Color.green;
                    }
                    break;
                case "Pergaminho":
                    gm.set_bomb_hint(true);
                    inventario.text = "Faca: x";
                    bombDefuseHint.SetActive(true);
                    break;
            }
        }
    } else{
            hint.text = "";
        }
    }
}