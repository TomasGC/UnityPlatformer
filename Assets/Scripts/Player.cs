using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character {

  // Use this for initialization.
	void Start(){
    _speed = 50f;
    _jump = 200;
    _speedMax = 3f;
    _rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    _animator = gameObject.GetComponent<Animator>();
    _maxHP = 5;
    _currentHP = _maxHP;
    _wallet = GameObject.FindGameObjectWithTag("Wallet").GetComponent<Wallet>();
    _audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
  }
	
	// Update is called once per frame.
	void Update(){
    // Update the Animator parameters.
    _animator.SetBool("Grounded", _grounded);
    _animator.SetFloat("Velocity", Mathf.Abs(_rigidBody2D.velocity.x));

    SpeedLimitation();
    TurnOver();
    Jump();
    DealWithLife();
	}

  // Update every fixed framerate frame.
  void FixedUpdate(){
    Move();
  }

  // Make the player moves.
  void Move(){
    // Get the direction on the horizontal axis (right or left).
    float direction = Input.GetAxis("Horizontal");

    // Moving the player.
    _rigidBody2D.AddForce((Vector2.right * _speed) * direction);
  }

  // The limitation of the player's speed.
  void SpeedLimitation(){
    if(_rigidBody2D.velocity.x > _speedMax){
      _rigidBody2D.velocity = new Vector2(_speedMax, _rigidBody2D.velocity.y);
}
    if(_rigidBody2D.velocity.x < -_speedMax) {
      _rigidBody2D.velocity  = new Vector2(-_speedMax, _rigidBody2D.velocity.y);
    }
  }

  // To turn the player to the left or right depending the direction.
  void TurnOver(){
    // To turn the player to the left.
    if(Input.GetAxis("Horizontal") < -0.1f){
      transform.localScale = new Vector3(-1, 1, 1);
    }

    // To turn the player to the right.
    if(Input.GetAxis("Horizontal") > 0.1f){
      transform.localScale = new Vector3(1, 1, 1);
    }
  }

  // To allow the player to jump.
  void Jump(){
    // if(Input.GetKey("up") || Input.GetKey("z")) can also work
    // but we can configure the entries of Jump in the unity settings, it's easier.
    if(Input.GetButtonDown("Jump")){
      if(_grounded){
        PlaySound(_jumpSound);
        _rigidBody2D.AddForce(Vector2.up * _jump);
        _secondJump = true;
      } else{
        if(_secondJump){
          PlaySound(_jumpSound);
          _rigidBody2D.velocity.Set(_rigidBody2D.velocity.x, 0);
          _rigidBody2D.AddForce(Vector2.up * _jump);
          _secondJump = false;
        }
      }
    }
  }

  // Check if the player has to die (Restart the game).
  protected override void CheckDeath(){
    if((_currentHP <= 0) && (!_audioSource.isPlaying)){
      _audioSource.PlayOneShot(_dieSound);

      // In order to delay the scene reload.
      Invoke("Die", _dieSound.length);
    }
  }

  protected override void Die(){
    SceneManager.LoadScene(0);
  }

  // When the player get a coin, the wallet improves and the coin disappear.
  void OnTriggerEnter2D(Collider2D collider2D){
    if(collider2D.CompareTag("Coin")){
      PlaySound(_coinSound);
      Destroy(collider2D.gameObject);
      _wallet.IncreaseNbCoins(Random.Range(1, 10));
    }

    if(collider2D.CompareTag("Heart")){
      PlaySound(_heartSound);
      Destroy(collider2D.gameObject);
      Heal(Random.Range(1, 5));
    }
  }

  // To set if the player is grounded or not.
  public void SetGrounded(bool grounded){
    _grounded = grounded;
  }

  // Attributes.
  float _speed;
  float _jump;
  float _speedMax;
  bool _grounded;
  bool _secondJump;
  Rigidbody2D _rigidBody2D;
  Wallet _wallet;

  public AudioClip _jumpSound;
  public AudioClip _coinSound;
  public AudioClip _heartSound;

}
