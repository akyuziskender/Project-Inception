using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	public Collider2D attackTrigger;

	private Animator animator;

	// Use this for initialization
	void Awake () {
		animator = gameObject.GetComponent<Animator>();
		attackTrigger.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Attack state
		if (Input.GetButtonDown("Fire1")) {   // if the "X" key is pushed, play the attack animation
			animator.SetBool("Attacked", true);
		}
	}

	void EnableHitbox() {
		attackTrigger.enabled = true;
	}

	void UnableHitbox() {
		animator.SetBool("Attacked", false);
		attackTrigger.enabled = false;
	}
}
