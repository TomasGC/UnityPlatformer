using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

  // Use this for initialization.
  void Start(){
    _isAttacking = false;
    _timerAttack = 0f;
    _attackCoolDown = 0.3f;
    _audioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame.
  void Update(){
    if(Input.GetButtonDown("Attack") && !_isAttacking){
      _isAttacking = true;
      _timerAttack = _attackCoolDown;
      _attackTriggered.enabled = true;
      _audioSource.PlayOneShot(_attackSound);
    }

    if(_isAttacking){
      if(_timerAttack > 0){
        _timerAttack -= Time.deltaTime;
      } else{
        _isAttacking = false;
        _attackTriggered.enabled = false;
      }
    }
    _animator.SetBool("Attacking", _isAttacking);
  }

  void Awake(){
    _animator = gameObject.GetComponent<Animator>();
    // (De)Activate the Collider2D box of the attackTrigger.
    _attackTriggered.enabled = false;
  }

  // Attributes.
  private bool _isAttacking;
  private float _timerAttack;
  private float _attackCoolDown;
  private Animator _animator;
  private AudioSource _audioSource;

  public Collider2D _attackTriggered;
  public AudioClip _attackSound;
}
