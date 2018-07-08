using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public GameObject player;

	// Update is called once per frame
	void Update () {
		float playerX = player.transform.position.x;
		float playerY = player.transform.position.y;
		transform.position = new Vector3(
			Mathf.Floor((playerX + 7.0f) / 14.0f) * 14.0f,
			Mathf.Floor((playerY + 4.0f) / 8.0f) * 8.0f, -10.0f);
	}
}
