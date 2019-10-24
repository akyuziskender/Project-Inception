using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStoneController : MonoBehaviour 
{
	private Animator _animator;

	private bool _firstWoodBroken;
	private bool _secondWoodBroken;

	public bool FirstWoodBroken {
		get {
			return _firstWoodBroken;
		}

		set {
			_firstWoodBroken = value;
		}
	}

	public bool SecondWoodBroken {
		get {
			return _secondWoodBroken;
		}
		
		set {
			_secondWoodBroken = value;
		}
	}
    // Start is called before the first frame update
    void Start() {
		_animator = gameObject.GetComponent<Animator>();
		_firstWoodBroken = _secondWoodBroken = false;
	}

    // Update is called once per frame
    void Update() {
		_animator.SetBool("FirstWoodBroken", _firstWoodBroken);
		_animator.SetBool("SecondWoodBroken", _secondWoodBroken);
	}
}
