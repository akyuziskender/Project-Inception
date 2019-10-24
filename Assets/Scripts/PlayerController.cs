using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	// Running
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _maxSpeed;
	
	// Climb
    public float climbSpeed;
    private float climbVelocity;

    public float vertical_movement;
    public float horizontal_movement;
	private float freezeTime = 0.5f;
	private float _timeInWater = 0f;
	private float _drowningTimer = 0f;

	// Jump
	private Vector2 _jumpHeight = new Vector2(0f, 9.5f);

	// Constants
	private float gravityStore;
	private float speedStore;
	private float maxSpeedStore;
	private Vector2 jumpHeightStore;

	// State
	public int currHealth;  // current health
    private int maxHealth = 100;
	private int _secondsInWater;
	private int _secondsDrowningTime;

	public bool rightPushingZone;
    public bool leftPushingZone;      // 1 This four are related with pushing state
    public bool pushed;           // 2
    public bool grounded;
    public bool climbed;
    public bool onLadder;
	public bool inCar;
    public bool dead;
	public bool freezed;
	private bool _inWater;
	private bool _drowningCooldown;
	private bool gasLambOn;
    private bool activateLamb;

    private Rigidbody2D rb2d;
    private Animator animator;
	public GameObject smask;
	public Color DamageEffectColor;

	public Vector3 ladderPos;

	public bool InWater {
		get { return _inWater; }
		set { _inWater = value; }
	}

	public float TimeInWater {
		set { _timeInWater = value; }
	}

	public float MaxSpeed {
		get { return _maxSpeed; }
		set { _maxSpeed = value; }
	}

	public float MaxSpeedStore {
		get { return maxSpeedStore; }
	}

	public Rigidbody2D Rb2D {
		get { return rb2d; }
		set { rb2d = value; }
	}

	// Use this for initialization
	public void Start() {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        currHealth = maxHealth; // at the beginning, set the healt of te player to the max health
		
		// Storing constants
        gravityStore = rb2d.gravityScale;
		speedStore = _speed;
		maxSpeedStore = _maxSpeed;
		jumpHeightStore = _jumpHeight;

		ladderPos = Vector3.zero;
		
        gasLambOn = false;
        activateLamb = true;    // TODO: this line will be changed
        dead = false;
		freezed = false;
		InWater = false;
		_drowningCooldown = false;

	}

    // Update is called once per frame
    void Update() {
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Climbed", climbed);
        animator.SetBool("GasLambOn", gasLambOn);
		animator.SetBool("InCar", inCar);
        animator.SetBool("Pushed", pushed);
        animator.SetFloat("SpeedX", Mathf.Abs(rb2d.velocity.x));
        animator.SetFloat("SpeedY", vertical_movement);

		if (!dead)
			PlayerMovement();

        // Die state
        if (currHealth < 1)
            animator.SetBool("Died", true);

        if (currHealth > maxHealth)
            currHealth = maxHealth; // making sure that current health never gets bigger than max health
    }

    void FixedUpdate() {
        if (!dead) {
            Vector3 easeVelocity = rb2d.velocity;
            easeVelocity.x *= 0.70f;
            easeVelocity.y = rb2d.velocity.y;
            easeVelocity.z = 0.0f;

            bool attackState = animator.GetBool("Attacked");

            // Fake friction / easing the x speed of the player
            if (grounded) {
                rb2d.velocity = easeVelocity;
            }
			if (!freezed) {
				// Moving the player
				horizontal_movement = Input.GetAxis("Horizontal");    // Getting horizontal movement (-1,1)
				if (climbed)
					horizontal_movement = 0;
				if (!attackState)
					rb2d.AddForce((Vector2.right * _speed) * horizontal_movement);
				else
					rb2d.AddRelativeForce(-rb2d.velocity);

				// Limiting the speed of the player
				if (rb2d.velocity.x > _maxSpeed) {
					rb2d.velocity = new Vector2(_maxSpeed, rb2d.velocity.y);
				}
				if (rb2d.velocity.x < -_maxSpeed) {
					rb2d.velocity = new Vector2(-_maxSpeed, rb2d.velocity.y);
				}
			}
			else {
				freezeTime -= Time.deltaTime;
				if (freezeTime <= 0) {
					freezed = false;
					freezeTime = 0.5f;
				}
			}
		}
	}

    void PlayerMovement() {
        bool jump_movement = false;
        if (!pushed)
            jump_movement = Input.GetButtonDown("Jump");
        vertical_movement = Input.GetAxisRaw("Vertical");
        animator.enabled = true;

        // Lighting the gas lamb
        if (Input.GetButtonDown("Fire2")) {
			if (activateLamb && !gasLambOn) {
				gasLambOn = true;
				smask.SetActive(true);
			}
			else if (activateLamb && gasLambOn) {
				gasLambOn = false;
				smask.SetActive(false);
			}
		}

        // Flipping the sprite vertically with respect to the player's direction
        if (Input.GetAxis("Horizontal") < -0.1f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetAxis("Horizontal") > 0.1f) {
            transform.localScale = new Vector3(1, 1, 1);
        }

		Jump(jump_movement);	// Jump movement

        // Climb state
        if (onLadder) {
            if (vertical_movement > 0f) {
				rb2d.gravityScale = 0f; // setting gravity to 0
				float offset = transform.localScale.x * 0.05f;
				transform.position = new Vector3(ladderPos.x - offset, transform.position.y, transform.position.z);
                transform.Translate(0, Time.deltaTime, 0);
                climbVelocity = climbSpeed * vertical_movement;
                rb2d.velocity = new Vector2(0f, climbVelocity);
                climbed = true;
                grounded = false;
            }
			else {
                climbed = false;
            }
        }

		// Player is in the water
		if(InWater) {
			Drowning();
		}

        if (!onLadder) {
            climbed = false;    
        }

		if(!climbed && !InWater)
			rb2d.gravityScale = gravityStore;   // setting gravity to its previous value

		// Push state
		if (leftPushingZone) {
            if (Input.GetAxis("Horizontal") > 0)	{
                animator.enabled = true;
                pushed = true;
                _maxSpeed = 0;
            }
			else if(Input.GetAxis("Horizontal") == 0) {
                pushed = false;
            }
        } else if (rightPushingZone) {
            if (Input.GetAxis("Horizontal") < 0) {
                animator.enabled = true;
                pushed = true;
                _maxSpeed = 0;
            }
            else if (Input.GetAxis("Horizontal") == 0) {
                pushed = false;
            }
        }
		else {
            pushed = false;
        }

    }

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Ladder")) {
			ladderPos = other.transform.position;
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag("Ladder")) {
			ladderPos = other.transform.position;
		}
	}

	private void Jump(bool jump_movement) {
		if (jump_movement && grounded) {
			rb2d.AddForce(_jumpHeight, ForceMode2D.Impulse);
		}
	}

	// Drowning state when the player stays in the water
	private void Drowning() {
		_timeInWater += Time.deltaTime;
		_secondsInWater = (int)(_timeInWater % 60);
		if (_drowningCooldown) {
			_drowningTimer += Time.deltaTime;
			_secondsDrowningTime = (int)(_drowningTimer % 60);
		}
		if (_secondsDrowningTime >= 1)
			_drowningCooldown = false;
		if (_secondsInWater > 5 && !_drowningCooldown) {
			currHealth -= 20;
			StartCoroutine(ChangeColor(0.3f));
			_drowningCooldown = true;
			_drowningTimer = 0f;
		}
	}

	public void UpdateMovementValues() {
		_speed = _speed * 0.4f;
		_maxSpeed = _maxSpeed * 0.4f;
		_jumpHeight = _jumpHeight * 0.45f;
		rb2d.gravityScale = 0.6f;
	}

	public void RestoreMovementValues() {
		_speed = speedStore;
		_maxSpeed = maxSpeedStore;
		_jumpHeight = jumpHeightStore;
		rb2d.gravityScale = gravityStore;
	}

	// Knockback function
	public IEnumerator Knockback(float knockbackDuration, float knockbackPower, Vector3 knockbackDir) {
		float timer = 0;

		while (knockbackDuration > timer) {
			timer += Time.deltaTime;

			rb2d.AddForce(new Vector3(knockbackDir.x * 1000, knockbackDir.y * knockbackPower, transform.position.z));
		}
		
		yield return 0;
	}
	// the function that makes player's sprite red when he gets damage
	public IEnumerator ChangeColor(float duration) {
		GetComponent<SpriteRenderer>().color = DamageEffectColor;
		yield return new WaitForSeconds(duration);
		GetComponent<SpriteRenderer>().color = Color.white;
		yield return new WaitForSeconds(duration);
	}

	// When the player is dead, set his body type as Static in order to prevent him from moving
	void MakePlayerStatic() {
		rb2d.bodyType = RigidbodyType2D.Static;
		dead = true;
	}

	// TODO change this function when the "YOU DIED" scene is ready
	void Die() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);	// for now, we are restarting the game when we die
	}
}
