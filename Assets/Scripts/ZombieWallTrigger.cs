using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWallTrigger : MonoBehaviour
{
	private ZombieController zombie;

	private void Start() {
		zombie = gameObject.GetComponentInParent<ZombieController>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (null == zombie) {
			Start();
		}
		if (other.CompareTag("Ground"))
			zombie.HitWall = true;
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag("Ground"))
			zombie.HitWall = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Ground"))
			zombie.HitWall = false;
	}
}
