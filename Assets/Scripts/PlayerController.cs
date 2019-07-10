﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
	private Vector3 playerInput;

    public CharacterController player;

    public float playerSpeed;

    private Vector3 movePlayer;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float jumpForce;

    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    public bool isOnSlope = false;
    private Vector3 hitNormal; 
    public float slideVelocity;
    public float slopeForceDown;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Cálculos, lógica
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        player.transform.LookAt(player.transform.position + movePlayer);

        setGravity();

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);
    	
    	//Debug.Log(player.velocity.magnitude);
    }
    //llamadas a movimientos
    private void FixedUpdate() 
    {
    	
    }

    void camDirection()
    {
    	camForward = mainCamera.transform.forward;
    	camRight = mainCamera.transform.right;

    	camForward.y = 0;
    	camRight.y = 0;

    	camForward = camForward.normalized;
    	camRight = camRight.normalized;
    }

    void PlayerSkills()
    {
    	if(player.isGrounded && Input.GetButtonDown("Jump")) {
    		fallVelocity = jumpForce;
    		movePlayer.y = fallVelocity;
    	}
    }

    void setGravity()
    {
    	if(player.isGrounded) {
    		fallVelocity = -gravity * Time.deltaTime;
    		movePlayer.y = fallVelocity;
    	} else {
    		fallVelocity -= gravity * Time.deltaTime;
    		movePlayer.y = fallVelocity;
    	}

    	SlideDown();
    }

    // Para rampas
    void SlideDown()
    {
    	//comprueba si está o no en una rampa
    	isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;
    	
    	if (isOnSlope) {
    		movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
    		movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;

    		movePlayer.y += slopeForceDown;
    	}
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
    	hitNormal = hit.normal;
    }
}
