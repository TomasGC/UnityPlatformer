using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

  // Use this for initialization.
	void Start(){
    _player = GameObject.FindGameObjectWithTag("Player");
    _bounds = true;
    _minPosCamera.Set(-100, 2.98f, -10);
    _maxPosCamera.Set(100, 100, -10);
    _smoothTimeX = 0.05f;
    _smoothTimeY = 0.05f;
  }
	
	// Update is called once per frame.
	void FixedUpdate(){
    float xPos = Mathf.SmoothDamp(transform.position.x, _player.transform.position.x, ref _velocity.x, _smoothTimeX);
    float yPos = Mathf.SmoothDamp(transform.position.y, _player.transform.position.y, ref _velocity.y, _smoothTimeY);

    transform.position = new Vector3(xPos, yPos, transform.position.z);

    // Bounds the camera to the player.
    if (_bounds){
      transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minPosCamera.x, _maxPosCamera.x),
                                        Mathf.Clamp(transform.position.y, _minPosCamera.y, _maxPosCamera.y),
                                        Mathf.Clamp(transform.position.z, _minPosCamera.z, _maxPosCamera.z));
    }
  }


  // Attributes.
  GameObject _player;
  bool _bounds;
  Vector3 _minPosCamera;
  Vector3 _maxPosCamera;
  float _smoothTimeX;
  float _smoothTimeY;
  Vector2 _velocity;

}
