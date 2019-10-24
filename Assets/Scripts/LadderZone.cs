using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderZone : MonoBehaviour {

	private PlayerController player;
	private Transform child;
	private float time;

	private void Start() {
		player = FindObjectOfType<PlayerController>();
		child = gameObject.transform.GetChild(0);
		time = 0f;
	}

	private void FixedUpdate() {
		if(!child.GetComponent<BoxCollider2D>().enabled) {
			time += Time.deltaTime;
			if (time > 0.75f) {
				child.GetComponent<BoxCollider2D>().enabled = true;
				time = 0f;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "LadderTrigger") {
			player.onLadder = true;
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (other.name == "LadderTrigger") {
			player.onLadder = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.name == "LadderTrigger") {
			player.onLadder = false;
		}
	}
}
