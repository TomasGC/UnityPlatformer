using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void DroppedFromMob(){
    Instantiate(_coin, this.transform.position, Quaternion.identity);
  }

  public GameObject _coin;
}
