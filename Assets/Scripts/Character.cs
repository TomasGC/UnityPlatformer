﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Character can't be instantiate, so it needs to be an abstract class.
public abstract class Character : MonoBehaviour {

  // Play a sound given by parameter.
  protected void PlaySound(AudioClip audioClip){
    _audioSource.PlayOneShot(audioClip);
  }

  // Take care of the player's HP.
  protected void DealWithLife(){
    if(_currentHP > _maxHP){
      _currentHP = _maxHP;
    }

    CheckDeath();
  }

  // Check if the character has to die, to be overriden.
  protected abstract void CheckDeath();

  // Die method, to be overriden.
  protected abstract void Die();

  // To get the current HPs of the character.
  public int GetHP(){
    return _currentHP;
  }

  // To get the max HPs of the character.
  public int GetMaxHP(){
    return _maxHP;
  }
  // To set the current HPs to a certain value.
  public void SetHP(int playerHP){
    _currentHP = playerHP;
  }

  // To decrease the current HPs by hpLost heart.
  public void Damages(int hpLost){
    PlaySound(_hurtSound);
    _currentHP -= hpLost;
    gameObject.GetComponent<Animation>().Play("RedFlash");
  }

  // To increase the current HPs by hpWin heart.
  public void Heal(int hpWin){
    _currentHP += hpWin;
  }

  protected int _currentHP;
  protected int _maxHP;
  protected AudioSource _audioSource;
  protected Animator _animator;
   

  public AudioClip _hurtSound;
  public AudioClip _dieSound;

}
