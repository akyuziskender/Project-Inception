using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FS_FirstWoodTrigger : MonoBehaviour {
	private Collider2D pol_collider;

	private void Start() {
		pol_collider = gameObject.GetComponent<PolygonCollider2D>();
	}

	public void Break() {
		this.transform.parent.GetComponent<FallingStoneController>().FirstWoodBroken = true;
		pol_collider.enabled = false;
	}
}
