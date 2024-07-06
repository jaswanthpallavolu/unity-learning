using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMovement : MonoBehaviour
{
    CharacterController controller;

    public float speed = 2f;
    public float gravity = -9.81f;
    public float jumpHeight = 4f;
    public float fallSpeed = 2f;
    Vector3 velocity;
    public Transform playerbody;
    Animator animator;

    float currentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = playerbody.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        bool isGrounded = controller.isGrounded;
        if(isGrounded && velocity.y < 0){
            // Debug.Log("jump 1");
            velocity.y = -2f;
            // playerbody.Translate(0f,-2f,0f);
        }
        if(isGrounded){
            animator.SetBool("jump",false);
        }
        
        Move();
        Jump(isGrounded);
    }

    void Move(){
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        animator.SetFloat("moveZ",z);
        animator.SetFloat("moveX",x);

        if(z==1 && Input.GetKey(KeyCode.LeftShift)){
            animator.SetBool("sprint",true);
            animator.SetBool("idle",false);
            currentSpeed = speed * 3f;
        }else{
            currentSpeed = speed;
            animator.SetBool("sprint",false);
        }

        if(x != 0 || z!=0 || animator.GetBool("sprint")){
            animator.SetBool("idle",false);
        }
        else{
            animator.SetBool("idle",true);
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void Jump(bool isGrounded){
        if(Input.GetButtonDown("Jump") && isGrounded){
            animator.SetBool("jump",true);
            // Debug.Log(animator.SetT("jump"));
            // Finding Velocity When Only Given Height [v = sqrt(2gh)]
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            // Invoke("StopJump",5f);
        }
        // y = 1/2 * g * t^2 [free fall motion]
        velocity.y += gravity * fallSpeed * Time.deltaTime ;
        controller.Move(velocity * Time.deltaTime);
        
        
    }
}
