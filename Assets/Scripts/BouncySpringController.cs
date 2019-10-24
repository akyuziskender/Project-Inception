using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySpringController : MonoBehaviour
{

	private bool _bounce = false;
	private Vector2 _bounceHeight = new Vector2(0f, 15f);
	private PlayerController player;

	public bool Bounce {
		get { return _bounce; }
		set { _bounce = value; }
	}

	void Start() {
		player = FindObjectOfType<PlayerController>();
	}

	void Update() {
		if(Bounce) {
			player.Rb2D.velocity = new Vector2(player.Rb2D.velocity.x, 0f);
			player.Rb2D.AddForce(_bounceHeight, ForceMode2D.Impulse);	// bounce effect
			Bounce = false;
		}
	}
}
