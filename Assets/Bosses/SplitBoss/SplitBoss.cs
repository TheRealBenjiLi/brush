using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitBoss : Entity {

	public int splitsRemaining;
	public float diameter;
	public GameObject bulletPrefab;

	private int t;
	private int lastAction;
	private bool grounded;
	private int splitFactor;

	// Use this for initialization
	void Start () {
		health = 1;
		damage = 1;
		this.rb = GetComponent<Rigidbody2D>();
		t = 0;
		lastAction = t;
		grounded = false;
		splitFactor = 2;
	}

	// Update is called once per frame
	void Update () {
		Action();
		IsKilled();
		t++;
	}

	private void Action () {
		if (t > lastAction + 150 && grounded) {
			// Randomly jump or attack
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
				float[] xPosVals = {-0.5f * diameter, 0.5f * diameter};
				float[] yPosVals = {0.5f * diameter, 0.5f * diameter};
				float[] xForceVals = {-100.0f, 100.0f};
				float[] yForceVals = {100.0f, 100.0f};
				for (int i = 0; i < xPosVals.Length; i++) {
					Vector3 posOffset = new Vector3(xPosVals[i], yPosVals[i], 1);
					Vector2 force = new Vector2(xForceVals[i], yForceVals[i]);
					GameObject bullet = Instantiate(bulletPrefab, transform.position + posOffset, transform.rotation);
					bullet.GetComponent<Rigidbody2D>().AddForce(force);
				}
				lastAction = t;
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
		}
	}
}
