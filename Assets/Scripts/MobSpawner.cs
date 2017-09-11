using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {

  // Use this for initialization
  void Start(){
    _currentNbMobs = 0;
    _maxNbMob = 3;
    _spawnTime = 3f;
    if(_currentNbMobs < _maxNbMob){
      // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
      InvokeRepeating("Spawn", _spawnTime, _spawnTime);
    }
  }
	
  void Spawn() {
    // If the player has no health left exit the function.
    if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetHP() <= 0) {
      return;
    }

    // Possible places to spawn.
    Vector3[] spawnPlaces = new[] {
      new Vector3(-14.5f, 1.92f, 0f),
      new Vector3(-11.32f, 0.94f, 0f),
      new Vector3(-6.11f, 1.92f, 0f),
      new Vector3(-0.7f, 0.94f, 0f),
      new Vector3(4.821f, 1.92f, 0f),
      new Vector3(9.39f, 0.94f, 0f),
      new Vector3(13.27f, 1.92f, 0f),
    };

    int index = Random.Range(0, spawnPlaces.Length);

    // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
    Instantiate(_shooter, spawnPlaces[index], Quaternion.identity);
    _currentNbMobs++;
  }

  // Attributes.
  private int _currentNbMobs;
  private int _maxNbMob;
  private float _spawnTime;            // How long between each spawn.

  public GameObject _shooter;                // The enemy prefab to be spawned.

}
