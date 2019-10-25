using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour {

	private PlayerController player;
    private BreakingWoodController _breakingWood;
	private BouncySpringController _bouncySpring;

    private void Start() {
		player = gameObject.GetComponentInParent<PlayerController>();
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (null == player) {
			Start();
		}
        if (other.CompareTag("Ground") || other.CompareTag("Box") || other.CompareTag("Breakable") || 
			other.CompareTag("Car") || other.CompareTag("BreakingWood") || other.CompareTag("BouncySpring"))
        {
			if (other.CompareTag("Car")) {
				player.grounded = true;
				player.inCar = true;
			}
            if (other.CompareTag("BreakingWood")) {
				player.grounded = true;
				_breakingWood = other.GetComponent<BreakingWoodController>();
                _breakingWood.StartAnim();
            }
			if(other.CompareTag("BouncySpring") && player.Rb2D.velocity.y < 0) {
				_bouncySpring = other.gameObject.GetComponent<BouncySpringController>();
				_bouncySpring.Bounce = true;
				player.grounded = false;
			}
        }
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag("Ground") || other.CompareTag("Box") || other.CompareTag("Breakable") || other.CompareTag("Car")) {
			if(other.name == "PlatformEffector" && player.vertical_movement < 0f) {		// exception for ladders
				other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			}
			else
				player.grounded = true;
			if (other.CompareTag("Car"))
				player.inCar = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Ground") || other.CompareTag("Box") || other.CompareTag("Breakable") || other.CompareTag("Car") || other.CompareTag("BreakingWood")) {
			player.grounded = false;
			if (other.CompareTag("Car"))
				player.inCar = false;
		}
	}
}
