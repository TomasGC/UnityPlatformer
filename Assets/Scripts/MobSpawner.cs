﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour {

  // Use this for initialization
  void Start(){
    _maxNbMob = 4;
    _spawnTime = 3f;
   
    // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
    InvokeRepeating("CheckIfCanSpawn", _spawnTime, _spawnTime);
  }
  void CheckIfCanSpawn(){
    // If the player has no health left exit the function.
    if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GetHP()<=0){
      // No needs to destroy all the mobs, as the game will reboot.
      return;
    }
    GameObject[] getCountMob = GameObject.FindGameObjectsWithTag("Mob");
    if(getCountMob.Length <= _maxNbMob){
      Spawn();
    }

  }
  void Spawn() {
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
  }

  // Attributes.
  private int _maxNbMob;    // Max number of mob instance.
  private float _spawnTime; // How long between each spawn.

  public GameObject _shooter; // The enemy prefab to be spawned.

}
