using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

  // Use this for initialization.
	void Start(){
    _speed = 50f;
    _jump = 200;
    _speedMax = 3f;
    _playerMaxHP = 5;
    _rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    _animator = gameObject.GetComponent<Animator>();
    _playerHP = _playerMaxHP;
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
    EaseVelocity();
    Move();
  }

  // Fake friction to ease the X speed of our player.
  void EaseVelocity(){
    Vector3 easeVelocity = _rigidBody2D.velocity;
    easeVelocity.x *= 0.75f;

    if (_grounded)
    {
      _rigidBody2D.velocity = easeVelocity;
    }

  }

  // Make the player moves.
  void Move(){
    // Get the direction on the horizontal axis (right or left).
    float direction = Input.GetAxis("Horizontal");

    // Moving the player.
    _rigidBody2D.AddForce((Vector2.right * _speed) * direction);
  }

  // The limitation of the player's speed.
  void SpeedLimitation()
  {
    if(_rigidBody2D.velocity.x > _speedMax){
      _rigidBody2D.velocity = new Vector2(_speedMax, _rigidBody2D.velocity.y);
}
    if(_rigidBody2D.velocity.x < -_speedMax) {
      _rigidBody2D.velocity  = new Vector2(-_speedMax, _rigidBody2D.velocity.y);
    }
  }

  // To turn the player to the left or right depending the direction.
  void TurnOver(){
    /*
     * To try out again.
    if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f){
      Vector3 scale = transform.localScale;
      scale.x *= -1;
      transform.localScale = scale;
    }
    */
    // To turn the player to the left.
    if (Input.GetAxis("Horizontal") < -0.1f)
    {
      transform.localScale = new Vector3(-1, 1, 1);
    }

    // To turn the player to the right.
    if (Input.GetAxis("Horizontal") > 0.1f)
    {
      transform.localScale = new Vector3(1, 1, 1);
    }
  }

  // To allow the player to jump.
  void Jump(){
    if (Input.GetButtonDown("Jump"))
    {
      if (_grounded)
      {
        _rigidBody2D.AddForce(Vector2.up * _jump);
        _secondJump = true;
      } else
      {
        if (_secondJump)
        {
          _rigidBody2D.velocity.Set(_rigidBody2D.velocity.x, 0);
          _rigidBody2D.AddForce(Vector2.up * _jump);
          _secondJump = false;
        }
      }
    }
  }

  // Take care of the player's HP.
  void DealWithLife(){
    if(_playerHP > _playerMaxHP){
      _playerHP = _playerMaxHP;
    }

    if(_playerHP <= 0){
      Die();
    }
  }
  // For now, when you die, you automatically restart.
  void Die(){
    SceneManager.LoadScene(0);
  }

  // To set if the player is grounded or not.
  public void SetGrounded(bool grounded){
    _grounded = grounded;
  }

  // To get the current HPs of the player.
  public int GetHP(){
    return _playerHP;
  }

  // To set the current HPs to a certain value.
  public void SetHP(int playerHP){
    _playerHP = playerHP;
  }

  // To decrease the current HPs by 1 heart.
  public void DecreaseHP(){
    --_playerHP;
  }


  // Attributes.
  private float _speed;
  private float _jump;
  private float _speedMax;
  private bool _grounded;
  private bool _secondJump;
  private Rigidbody2D _rigidBody2D;
  private Animator _animator;
  private int _playerHP;
  private int _playerMaxHP;

}
