using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackTriggered : MonoBehaviour {

	// Use this for initialization
	void Start () {
    _damages = 20;
    _shooter = GameObject.FindGameObjectWithTag("Mob").GetComponent<Shooter>();

  }
	
	// Update is called once per frame
	void Update () {
		
	}

  // See if there is a collision with the mob and send a message to create damages to it.
  void OnTriggerEnter2D(Collider2D collider2D){
    print("INNNNN1");
    if(collider2D.isTrigger != true && collider2D.CompareTag("Mob")){
      print("INNNNN2");
      _shooter.Damages(20);
    }

    print("isTrigger: " + collider2D.isTrigger);
    print("collider2D.CompareTag: " + collider2D.CompareTag("Mob"));

    if (collider2D.isTrigger != true && collider2D.CompareTag("Mob")){
      print("INNNNN3");
      //collider2D.SendMessageUpwards("Damage", _damages);
    }
  }

  // Attributes.
  private int _damages;
  private Shooter _shooter;
}
