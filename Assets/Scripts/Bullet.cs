using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

  // Use this for initialization.
  void Start(){
    _damages = 1;
  }

  void OnTriggerEnter2D(Collider2D collider2D){
    if(collider2D.CompareTag("Player")){
      collider2D.SendMessageUpwards("Damages", 1);
      Destroy(gameObject);
    }
  }

  // Attributes.
  int _damages;

}
