using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
    _heartUI.sprite = _hpSprites[_player.GetHP()];
	}

  // Attributes.
  public Sprite[] _hpSprites;
  public Image _heartUI;
  private Player _player;

}
