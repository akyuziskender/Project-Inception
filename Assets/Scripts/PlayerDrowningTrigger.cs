using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrowningTrigger : MonoBehaviour
{
	private PlayerController _playerScript;

	// Start is called before the first frame update
	void Start() {
		_playerScript = FindObjectOfType<PlayerController>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Water")) {
			_playerScript.InWater = true;
			_playerScript.UpdateMovementValues();
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag("Water")) {
			_playerScript.InWater = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Water")) {
			_playerScript.InWater = false;
			_playerScript.TimeInWater = 0f;
			_playerScript.RestoreMovementValues();
		}
	}
}
