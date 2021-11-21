using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	public float moveSpeed = 5f;
	Vector2 movement;
	
	public Rigidbody2D rigidBody;
	public Animator animator;

    void Update() {
	    movement.x = Input.GetAxisRaw("Horizontal");
	    movement.y = Input.GetAxisRaw("Vertical");
	    
	    animator.SetFloat("Horizontal", movement.x);
	    animator.SetFloat("Vertical", movement.y);
	    
	    if (movement != Vector2.zero) {
		    animator.SetFloat("LastX", movement.x);
		    animator.SetFloat("LastY", movement.y);
    	}
	    
	    animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    
	void FixedUpdate() {
		rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
	}
    
}
