using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGroundChecker : MonoBehaviour {

	private ZombieController zombie;

	private void Start() {
		zombie = gameObject.GetComponentInParent<ZombieController>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (null == zombie) {
			Start();
		}
		if (other.CompareTag("Ground"))
			zombie.grounded = true;
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag("Ground"))
				zombie.grounded = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Ground"))
			zombie.grounded = false;
	}
}
