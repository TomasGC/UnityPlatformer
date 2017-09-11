using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTriggered : MonoBehaviour {

	// Use this for initialization
	void Start () {
    _damages = 20;
  }

  // See if there is a collision with the mob and send a message to create damages to it.
  void OnTriggerEnter2D(Collider2D collider2D){
    if(collider2D.isTrigger != true && collider2D.CompareTag("Mob")){
      collider2D.SendMessageUpwards("Damages", _damages);
    }
  }

  // Attributes.
  int _damages;
  //Shooter _shooter;

}
