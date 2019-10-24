using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMask : MonoBehaviour
{

	[Range(0.05f, 0.1f)]
	public float flickTime;

	[Range(0.01f, 0.05f)]
	public float addSize;

	public Transform Target;

	private float timer = 0;
	private bool bigger = true;


	// Update is called once per frame
	void Update()
    {
		timer += Time.deltaTime;

		if(timer > flickTime) {
			if (bigger) {
				transform.localScale = new Vector3(transform.localScale.x + addSize, transform.localScale.y + addSize, transform.localScale.z);
			}
			else {
				transform.localScale = new Vector3(transform.localScale.x - addSize, transform.localScale.y - addSize, transform.localScale.z);
			}
			timer = 0;
			bigger = !bigger;
		}
		float offset = Target.transform.localScale.x;
		transform.position = new Vector3(Target.position.x + (offset * 0.88f), Target.position.y + 0.7f, Target.position.z);
    }
}
