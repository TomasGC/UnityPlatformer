using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

  // Use this for initialization.
  void Start(){
    _isAttacking = false;
    _timerAttack = 0f;
    _attackCoolDown = 0.3f;

  }

  // Update is called once per frame.
  void Update(){
    if(Input.GetButtonDown("Attack") && !_isAttacking){
      _isAttacking = true;
      _timerAttack = _attackCoolDown;
      _attackTriggered.enabled = true;
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
  public Collider2D _attackTriggered;
  private Animator _animator;

}
