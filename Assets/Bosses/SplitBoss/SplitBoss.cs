using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBoss : Entity {

	public int splitsRemaining;
	public float diameter;
	public GameObject bulletPrefab;

	private int t;
	private int lastAction;
	private const int waitTime = 150;
	private bool grounded;
	private const int splitFactor = 2;

	// Use this for initialization
	void Start () {
		health = 1;
		damage = 1;
		this.rb = GetComponent<Rigidbody2D>();
		t = 0;
		lastAction = t;
		grounded = false;
	}

	// Update is called once per frame
	void Update () {
		Action();
		IsKilled();
		t++;
	}

	private void Action () {
		if (t > lastAction + waitTime && grounded) {
			// Randomly jumps or charges at the player
			if (Random.value > 0.5) {
				Vector2 velo = 500f * Vector2.up;
				if (Random.value > 0.5) {
					velo += 100f * Random.value * Vector2.right;
				} else {
					velo += 100f * Random.value * Vector2.left;
				}
				rb.AddForce(velo);
				grounded = false;
			} else {
				// Charge at the player
				GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
				Vector3 direction = player.transform.position - transform.position;
				rb.velocity = new Vector2(direction.x, direction.y);
				grounded = false;
			}
		} else if (grounded) {
			rb.velocity = new Vector2(0, 0);
		}
	}

	// Overriding default IsKilled function to include splitting
	private void IsKilled () {
		if (health <= 0) {
			if (splitsRemaining > 0) {
				for (int i = 0; i < splitFactor; i++) {
					// posOffset prevents the children to balance on top of each other
					Vector3 posOffset = new Vector3(Random.value, 0, 0);
					GameObject child = Instantiate(gameObject,
						transform.position + posOffset, transform.rotation);
					child.transform.localScale = new Vector3(diameter / 2,
						diameter / 2, 1);
					child.GetComponent<SplitBoss>().splitsRemaining = splitsRemaining - 1;
					child.GetComponent<SplitBoss>().diameter = diameter / 2;
				}
			}
			Destroy(gameObject);
		}
	}

	// Same logic used in Player script to calculate SplitBoss's jump resets
	public void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Player") {
			Entity script = col.gameObject.GetComponent<Entity>();
			script.TakeDamage(damage);
			return;
		}
		Vector2 colToBoss = gameObject.GetComponent<Renderer>().bounds.center -
			col.gameObject.GetComponent<Renderer>().bounds.center;
		Vector2 colScale = new Vector2(col.gameObject.transform.lossyScale.x,
			col.gameObject.transform.lossyScale.y);
		colToBoss = new Vector2(colToBoss.x / colScale.x, colToBoss.y / colScale.y);
		if (col.gameObject.tag == "Ground" &&
			colToBoss.y > Mathf.Abs(colToBoss.x)) {
			lastAction = t;
			grounded = true;
		} else {
			rb.velocity = new Vector2(0, 0);
		}
	}
}
