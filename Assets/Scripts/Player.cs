using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  
  public Player(){
    _speed = 50f;
    _jump = 150f;
    _speedMax = 3f;
  }

  // Use this for initialization.
	void Start () {
    _rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    _animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame.
	void Update() {
    _animator.SetBool("Grounded", _grounded);
    _animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
    /*
    // To turn the player to the left or right depending the direction.
    if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f){
      Vector3 scale = transform.localScale;
      scale.x *= -1;
      transform.localScale = scale;
    }
    */
    // To turn the player to the left.
    if(Input.GetAxis("Horizontal") < -0.1f){
      transform.localScale = new Vector3(-1, 1, 1);
    }

    // To turn the player to the right.
    if(Input.GetAxis("Horizontal") > 0.1f){
      transform.localScale = new Vector3(1, 1, 1);
    }

    // To allow the player to jump.
    if(Input.GetButtonDown("Jump") && _grounded){
      _rigidBody2D.AddForce(Vector2.up * _jump);
    }
	}

  // Update every fixed framerate frame.
  void FixedUpdate() {
    // Get the direction on the horizontal axis (right or left).
    float direction = Input.GetAxis("Horizontal");

    // Moving the player.
    _rigidBody2D.AddForce((Vector2.right * _speed) * direction);

    // Speed limitation.
    if(_rigidBody2D.velocity.x > _speedMax){
      _rigidBody2D.velocity = new Vector2(_speedMax, _rigidBody2D.velocity.y);
    }
    if(_rigidBody2D.velocity.x < -_speedMax) {
      _rigidBody2D.velocity  = new Vector2(-_speedMax, _rigidBody2D.velocity.y);
    }

  }

  public void SetGrounded(bool grounded){
    _grounded = grounded;
  }

  // Public attributes.
  public float _speed;
  public float _jump;

  // Private attributes.
  private float _speedMax;
  private bool _grounded;
  private Rigidbody2D _rigidBody2D;
  private Animator _animator;
}
