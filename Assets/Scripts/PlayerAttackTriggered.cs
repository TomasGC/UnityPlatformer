using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTriggered : MonoBehaviour {

	// Use this for initialization
	void Start () {
    _damages = 20;
    _shooter = GameObject.FindGameObjectWithTag("Mob").GetComponent<Shooter>();
  }

  // See if there is a collision with the mob and send a message to create damages to it.
  void OnTriggerEnter2D(Collider2D collider2D){
    if(collider2D.isTrigger != true && collider2D.CompareTag("Mob")){
      _shooter.Damages(_damages);
    }
  }

  // Attributes.
  int _damages;
  Shooter _shooter;

}
