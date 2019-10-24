using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
	private PlayerController playerScript;
	public int damage;

	private void Start() {
		playerScript = FindObjectOfType<PlayerController>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Player") {
			Vector3 mushroom_to_player = other.transform.position - gameObject.transform.position;
			StartCoroutine(playerScript.Knockback(0.02f, 100, mushroom_to_player));   // when the player takes damage, apply a knockback
			StartCoroutine(playerScript.ChangeColor(0.3f));     // when the player takes damage, change its sprite's color to red for a short time
			playerScript.currHealth -= damage;
			playerScript.freezed = true;
		}
	}
}
