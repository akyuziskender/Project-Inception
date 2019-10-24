using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadTrigger : MonoBehaviour
{
	private PlayerController _playerScript;

	// Start is called before the first frame update
	void Start()
    {
        _playerScript = FindObjectOfType<PlayerController>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.name == "Stone") {
			StartCoroutine(_playerScript.ChangeColor(0.85f));
			_playerScript.currHealth = 0;
		}
	}
}
