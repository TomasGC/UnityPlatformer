using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

  // Use this for initialization.
  void Start()
  {
    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
  }

  void OnTriggerEnter2D(Collider2D collider2D){
    if(collider2D.CompareTag("Player")){
      _player.Damages(1);
      Destroy(gameObject);
    }
  }

  // Attributes.
  Player _player;

}
