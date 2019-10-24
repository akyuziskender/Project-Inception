using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour {
    private PlayerController player;

	void OnTriggerEnter2D(Collider2D other) {
		if(!other.isTrigger) {
			if(other.CompareTag("Enemy")) {
				other.gameObject.GetComponent<ZombieController>().health--;
				player = FindObjectOfType<PlayerController>();
				Vector3 player_to_zombie = other.transform.position - player.transform.position;
				StartCoroutine(other.gameObject.GetComponent<ZombieController>().Knockback(0.02f, 100, player_to_zombie));   // when the player takes damage, apply a knockback
				StartCoroutine(other.gameObject.GetComponent<ZombieController>().ChangeColor());     // when the player takes damage, change its sprite's color to red for a short time
			}
			else if(other.CompareTag("Breakable")) {
				other.gameObject.GetComponent<BoxController>().is_broke = true;
			}
			else if(other.name == "FirstWoodTrigger") {
				other.gameObject.GetComponent<FS_FirstWoodTrigger>().Break();
			}
			else if (other.name == "SecondWoodTrigger") {
				other.gameObject.GetComponent<FS_SecondWoodTrigger>().Break();
			}
		}
	}
}
