using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	
	[SerializeField] private float moveSpeed;
	public Transform movePoint;
	Vector2 movement;
	[SerializeField] private Animator animator;
	public LayerMask whatStopsMovement;

	void Start() {
		movePoint.parent = null;
	}

  void Update() {
		transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f) {
			if (Mathf.Abs(movement.x) == 1f) {
				if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(movement.x, 0f, 0f), 0.2f, whatStopsMovement)) {
					movePoint.position += new Vector3(movement.x, 0f, 0f);
				}
			}
			if (Mathf.Abs(movement.y) == 1f) {
				if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, movement.y, 0f), 0.2f, whatStopsMovement)) {
					movePoint.position += new Vector3(0f, movement.y, 0f);
				}
			}
		}
	    
	  animator.SetFloat("Horizontal", movement.x);
	  animator.SetFloat("Vertical", movement.y);
	    
	  if (movement != Vector2.zero) {
	    animator.SetFloat("LastX", movement.x);
	    animator.SetFloat("LastY", movement.y);
  	}
	    
	  animator.SetFloat("Speed", movement.sqrMagnitude);
  }
    
}
