using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGuyScript : Player {

	public bool lifesteal;

	// Use this for initialization
	public void Start () {
		characterid = 1;
		playerUpgrades = new List<Upgrade>();

		health = 2;
		maxHealth = 2;
		damage = 1;
		this.rb = GetComponent<Rigidbody2D>();

		speed = 7.0f;
		jumpMultiplier = 20.0f;
		t = 0;
		prevJumpStart = t;
		maxJumpsRemaining = 1;
		jumpsRemaining = maxJumpsRemaining;
		faceDirection = true;

		isInAir = true;
		isMovingHorizontal = false;
		isAttacking = false;
		lastMoving = t;
		defaultHeadPosition = head.transform.localPosition;

		lifesteal = false;

		ApplyUpgrades();
	}

	// Update is called once per frame
	public void Update () {
		Move();
		Attack();
		Animate();
		IsKilled();
		t++;
	}

	// Attack hitbox appears in front of player direction when Fire1 is pushed.
	// If player aims up or down beforehand, the appropriate hitbox will appear.
	protected void Attack () {
		if (Input.GetButtonDown("Fire1")) {
			GameObject hitbox;
			if (Input.GetAxis("Vertical") < 0.0f) {
				hitbox = Instantiate(hitboxPrefab, transform.position +
					Vector3.down * 1.5f + Vector3.left * 0.24f, transform.rotation);
			} else if (Input.GetAxis("Vertical") > 0.0f) {
				hitbox = Instantiate(hitboxPrefab, transform.position +
					Vector3.up * 1.7f + Vector3.left * 0.24f, transform.rotation);
			} else if (faceDirection) {
				hitbox = Instantiate(hitboxPrefab, transform.position +
					Vector3.right * 0.9f, transform.rotation);
			} else {
				hitbox = Instantiate(hitboxPrefab, transform.position +
					Vector3.left * 1.4f, transform.rotation);
			}
			hitbox.GetComponent<DestroyOnContact>().creator = this;
			hitbox.GetComponent<DestroyOnContact>().damage = damage;
			Physics2D.IgnoreCollision(hitbox.GetComponent<Collider2D>(),
				GetComponent<Collider2D>());
		}
	}

	public void OnHitSuccess () {
		if (lifesteal) {
			health += damage;
			if (health > maxHealth) {
				health = maxHealth;
			}
		}
	}
}
