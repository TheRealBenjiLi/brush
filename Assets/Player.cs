using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	public GameObject hitboxPrefab;

	protected float speed;
	protected float jumpMultiplier;
	protected int t;
	protected int prevJumpStart;               // Used to calculate jump arcs
	protected int maxJumpsRemaining;           // Maximum jumps for the player
	protected int jumpsRemaining;              // Counter to track jumps remaining
	protected bool faceDirection;              // Left is false, right is true
	protected Vector3 attackOffsetHorizontal;  // Offset is for facing right
	protected Vector3 attackOffsetVertical;    // Offset is for facing up

	// Handles player movement
	protected void Move () {
		float direction = Input.GetAxis("Horizontal");
		float x = direction * speed;
		UpdateDirection(direction);
		float y = JumpCalculate();
		rb.velocity = new Vector2(x, y);
	}

	// Handles the direction the player is facing
	protected void UpdateDirection (float direction) {
		if (direction > 0) {
			faceDirection = true;
		} else if (direction < 0) {
			faceDirection = false;
		}
	}

	// A helper function to help calculate jump height
	// TODO: does the jump feel good enough?
	protected float JumpCalculate () {
		float y = rb.velocity.y;
		if (Input.GetButtonDown("Jump") && jumpsRemaining > 0) {
			prevJumpStart = t;
			jumpsRemaining--;
		}	else if (Input.GetButton("Jump")) {
			float jumpNormal = 1.0f - 0.1f * (t - prevJumpStart);
			if (jumpNormal > 0) {
				y = jumpNormal * jumpMultiplier;
			}
		} else if (Input.GetButtonUp("Jump")) {
			if (y > 0) {
				y = 0;
			}
		}
		return y;
	}

	// Current purpose is to reset jumps when contacting the top of a
	// "Ground" object or "Platform" object. If connecting with a platform object
	// from anywhere aside from the top, ignore collisions
	// TODO: the current raycast solution has some holes
	public void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Ground" &&
			Physics2D.Raycast(transform.position, Vector2.down,
			Mathf.Infinity, 9).distance <= 1.0f) {
			jumpsRemaining = maxJumpsRemaining;
		}
	}
}
