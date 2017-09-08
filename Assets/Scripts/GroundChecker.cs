using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour{

	// Use this for initialization.
	void Start(){
    _player = gameObject.GetComponentInParent<Player>();
	}
	
	// Called when the player touch the ground.
  void OnTriggerEnter2D(Collider2D collider2D){
    _player.SetGrounded(true);
	}

  // Called when the player leave the ground.
  void OnTriggerExit2D(Collider2D collider2D){
    _player.SetGrounded(false);
  }

  // Called while the player stays on the ground.
  void OnTriggerStay2D(Collider2D collider2D){
    _player.SetGrounded(true);
  }


  // Attributes.
  private Player _player;

}
