﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 

public class Movement : MonoBehaviour
{

    CharacterController controller;

    private float speed;
    public float crouchedSpeed = 6f;

    public float jumpHeight = 3f;
    public float groundDistance = 0.4f;
    public float gravity = -20f;

    public float glideBoost = 10000f;
    public float boostAmount = 3f;
    public float glideGravity = -4f;
    public Vector3 glideDirection = new Vector3(0, 0, 0);
 
    public Transform groundCheck;
    public LayerMask groundMask;
    public GameObject player;

    Vector3 velocity;
    bool isGrounded;
    bool isCrouched;
    bool isGliding;


    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }


    void Update()

    {
        if (isCrouched)
        {
          speed = crouchedSpeed;

        }

        else
        {
            speed = crouchedSpeed * 2;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)

        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (Input.GetButton("Crouch"))
        {
            controller.height = 1f;
            boostAmount = 3f;
            isCrouched = true;
        }

        else
        {
            controller.height = 2f;
            isCrouched = false;
        }
        if (Input.GetButton("Glide") && !isGrounded)
        {
            speed *= 2f;
            gravity = glideGravity;
            isGliding = true;
        }

        else
        {
            speed = crouchedSpeed * 2;
            gravity = -20f;
            isGliding = false;
        }

        if(isGliding && Input.GetButtonDown("Boost") && boostAmount > 0f)
        {
            controller.Move(move * glideBoost * Time.deltaTime);
            boostAmount--;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }     
    

}

 