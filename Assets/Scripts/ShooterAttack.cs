using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
    _isRight = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void Awake(){
    _shooter = gameObject.GetComponentInParent<Shooter>();
  }

  void OnTriggerStay2D(Collider2D collider2D){
    if(collider2D.CompareTag("Player")){
      _shooter.Attack(_isRight);
    }
  }

  // Attributes.
  private Shooter _shooter;
  private bool _isRight;

}
