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
    private int seconds, minutes;
   private float vibration = 0;
   private float timer = 0.0f;
   public Text hint;
   public Text inventario, inv_mult, inv_radio, txt_timer;

    public GameObject bombDefuseHint;

    private AudioSource _audiosource;
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
        _audiosource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (gm.game_paused){
            Cursor.lockState = CursorLockMode.None;
            return;
        }

        if(timer >= 59.0f){
            minutes ++;
            timer = 0.0f;
            if(minutes >= 4){
                Cursor.lockState = CursorLockMode.None;
                gm.EndGame();
            }
        }
        timer += Time.deltaTime;
        
        txt_timer.text = "0" + minutes.ToString() + ":" + (timer < 10.0f ? "0" : "") + timer.ToString("F0");

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
        if(hit.collider.name == "Pergaminho" || (hit.collider.name == "Faca" && gm.has_read_hint) || (hit.collider.name == "Multimetro" && gm.has_read_hint) || (hit.collider.name == "Radio" && gm.has_read_hint)){
            hint.text = "Pressione F para pegar " + hit.collider.name;
        }
        if(hit.collider.name == "Bomb" && gm.can_defuse()){
            hint.text = "Pressione F para defusar a bomba";
        }
        if(Input.GetKeyDown(KeyCode.F)){
            switch(hit.collider.name){
                case "Faca":
                    if(gm.has_read_hint){
                        _audiosource.Play();
                        Destroy(hit.collider.gameObject);
                        gm.got_knife();
                        inventario.text = "Faca: ✔";
                        inventario.color = Color.green;
                    }
                    break;
                case "Multimetro":
                    if(gm.has_read_hint){
                        _audiosource.Play();
                        Destroy(hit.collider.gameObject);
                        gm.got_multimetro();
                        inv_mult.text = "Multimetro: ✔";
                        inv_mult.color = Color.green;
                    }
                    break;
                case "Radio":
                    if(gm.has_read_hint){
                        _audiosource.Play();
                        Destroy(hit.collider.gameObject);
                        gm.got_radio();
                        inv_radio.text = "Radio: ✔";
                        inv_radio.color = Color.green;
                    }
                    break;
                case "Pergaminho":
                    _audiosource.Play();
                    gm.set_bomb_hint(true);
                    inventario.text = "Faca: x";
                    inv_mult.text = "Multimetro: x";
                    inv_radio.text = "Radio: x";
                    bombDefuseHint.SetActive(true);
                    break;
                case "Bomb":
                    _audiosource.Play();
                    gm.player_won = true;
                    Cursor.lockState = CursorLockMode.None;
                    gm.EndGame();
                    break;
            }
        }
    } else{
            hint.text = "";
        }
    }
}