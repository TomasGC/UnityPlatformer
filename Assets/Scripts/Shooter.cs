using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	// Use this for initialization
	void Start(){
    _shooterMaxHP = 60;
    _shooterHP = _shooterMaxHP;
    _activationRange = 8;
    _shootInterval = 0.8f;
    _bulletSpeed = 7f;
    _bulletTimer = 0;
    _activated = false;
    _lookingRight = false;
    _audioSource = GetComponent<AudioSource>();
  }
	
	// Update is called once per frame
	void Update(){
    _animator.SetBool("Activated", _activated);
    _animator.SetBool("LookingRight", _lookingRight);

    RangeCheck();
    LookTheTarget();

    CheckDeath();
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
    _audioSource.clip = _attackSound;
    _audioSource.Play();
    GameObject bulletClone;
    bulletClone = Instantiate(_bullet, shootPoint.transform.position, shootPoint.transform.rotation) as GameObject;
    bulletClone.GetComponent<Rigidbody2D>().velocity = direction * _bulletSpeed;

    _bulletTimer = 0;
  }

  // Check if the shooter has to die (be destoyed).
  void CheckDeath(){
    if(_shooterHP <= 0){
      _audioSource.clip = _dieSound;
      _audioSource.Play();

      //renderer.enabled = false;
      // To make the mob drops behind him.
      float offset = (_lookingRight) ? 0.5f : -0.5f;
      Vector3 dropPosition = transform.position;
      dropPosition.x += offset;
      Instantiate(_coin, dropPosition, Quaternion.identity);
      Destroy(gameObject);
    }
  }

  // Attack the target.
  public void Attack(bool attackRightSide){
    // Increase _bulletTime by 1 each seconds.
    _bulletTimer += Time.deltaTime;

    // While the bullet still is "alive" we continue the attack.
    if(_bulletTimer >= _shootInterval){
      Vector2 direction = _target.transform.position - transform.position;
      direction.Normalize();
 
      if(attackRightSide){
        AttackTheRightSide(direction, _shootPointRight);
      } else{
        AttackTheRightSide(direction, _shootPointLeft);
      }
    }
  }

  // To decrease the current HPs of the shooter by hpLost.
  public void Damages(int hpLost){
    if(_audioSource){
      _audioSource.clip = _hurtSound;
      _audioSource.Play();
    }
    _shooterHP -= hpLost;
    gameObject.GetComponent<Animation>().Play("RedFlash");
  }

  // Attributes
  private int _shooterHP;
  private int _shooterMaxHP;
  private float _distance;
  private float _activationRange;
  private float _shootInterval;
  private float _bulletSpeed;
  private float _bulletTimer;
  private bool _activated;
  private bool _lookingRight;
  private AudioSource _audioSource;

  public GameObject _bullet;
  public Transform _target;
  public Transform _shootPointRight;
  public Transform _shootPointLeft;
  private Animator _animator;
  public GameObject _coin;
  public AudioClip _hurtSound;
  public AudioClip _attackSound;
  public AudioClip _dieSound;

}
