using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

	private Animator animator;

	public bool is_broke;
	public float spawnChance;

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();

		is_broke = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		animator.SetBool("Break", is_broke);
	}

	void DisableCollider() {
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
	}

	void DestroyBox() {
		Destroy(gameObject);
		if (Random.value <= spawnChance)
			Debug.Log("AN APPLE");
	}
}
