using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Character {

  enum Loot { Coin, Heart };


	// Use this for initialization
	void Start(){
    _maxHP = 60;
    _currentHP = _maxHP;
    _activationRange = 8;
    _shootInterval = 0.8f;
    _bulletSpeed = 7f;
    _bulletTimer = 0;
    _activated = false;
    _lookingRight = false;
    _audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
    _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().transform;
  }
	
	// Update is called once per frame
	void Update(){
    _animator.SetBool("Activated", _activated);
    _animator.SetBool("LookingRight", _lookingRight);

    RangeCheck();
    LookTheTarget();

    DealWithLife();
	}

  // Activate the shooter.
  void Awake(){
    _animator = gameObject.GetComponent<Animator>();    
  }

  // Check the distance between the shooter and the target.
  // If the target is in the range, the shooter is activated.
  void RangeCheck(){
    _distance = Vector3.Distance(transform.position, _target.transform.position);
   // _activated = (_distance > _activationRange) ? false : true;
    if(_distance < _activationRange){
      _activated = true;
    }
    if(_distance > _activationRange){
      _activated = false;
    }
  }

  // Look on the direction of the target.
  void LookTheTarget(){
    if(_target.transform.position.x > transform.position.x){
      _lookingRight = false;
    }
    if(_target.transform.position.x < transform.position.x){
      _lookingRight = true;
    }
  }

  // Attack the side where the target is.
  void AttackTheRightSide(Vector2 direction, Transform shootPoint){
    PlaySound(_attackSound);
    GameObject bulletClone;
    bulletClone = Instantiate(_bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
    bulletClone.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;

    _bulletTimer = 0;
  }

  protected override void CheckDeath(){
    if(_currentHP <= 0){
      AudioSource.PlayClipAtPoint(_dieSound, transform.position);
      Die();
    }
  }
  // When the shooter dies, he drop a coin and is destroyed.
  protected override void Die(){
    Drop(Loot.Coin);
    Drop(Loot.Heart);

    Destroy(gameObject);
  }

  // Drop an item in a random place behind the shooter.
  void Drop(Loot typeOfDrop){
    float offset = Random.Range(0, 0.5f);
    offset *= (!_lookingRight) ? -1 : 1;
    Vector3 dropPosition = transform.position;
    dropPosition.x += offset;
    switch(typeOfDrop){
      case Loot.Coin:
        Instantiate(_coin, dropPosition, Quaternion.identity);
        break;

      case Loot.Heart:
        // To randomise the drop of heart.
        if(Random.Range(0f, 1f) < 0.5f){
          Instantiate(_heart, dropPosition, Quaternion.identity);
        }
        break;
    }
    
  }

  // Attack the target.
  public void Attack(bool attackRightSide){
    // Increase _bulletTime by 1 each seconds.
    _bulletTimer += Time.deltaTime;

    // While the bullet still is "alive" we continue the attack.
    if(_bulletTimer >= _shootInterval){
      Vector2 direction = _target.transform.position - transform.position;

      // Offset, like this the shooter shoots on the player's body.
      direction.y -= _target.GetComponent<SpriteRenderer>().bounds.size.y; 
      direction.Normalize();
 
      if(attackRightSide){
        AttackTheRightSide(direction, _shootPointRight);
      } else{
        AttackTheRightSide(direction, _shootPointLeft);
      }
    }
  }

  // Attributes
  float _distance;
  float _activationRange;
  float _shootInterval;
  float _bulletSpeed;
  float _bulletTimer;
  bool _activated;
  bool _lookingRight;
  Transform _target;

  public GameObject _bullet;
  public Transform _shootPointRight;
  public Transform _shootPointLeft;
  public GameObject _coin;
  public AudioClip _attackSound;
  public GameObject _heart;

}
