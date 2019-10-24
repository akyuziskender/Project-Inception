using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour {
	public Collider2D attackTrigger;
	private Rigidbody2D rb2d;
	private ZombieController zombieControllerScript;

	private void Awake() {
		rb2d = gameObject.GetComponent<Rigidbody2D>();
		zombieControllerScript = rb2d.GetComponent<ZombieController>();
		attackTrigger.enabled = false;
	}

	private void FixedUpdate() {
		if (!zombieControllerScript.attacked)
			attackTrigger.enabled = false;
	}

	void EnableHitbox() {
		attackTrigger.enabled = true;
	}

	void DisallowHitbox() {
		attackTrigger.enabled = false;
	}
}
