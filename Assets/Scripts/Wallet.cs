using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour {

	// Use this for initialization.
	void Start(){
    _nbCoins = 0;
	}
	
	// Update is called once per frame.
	void Update(){
    _textCoins.text = "Wallet: " + _nbCoins + " coins";
	}

  // To increase the number of coins the player have by coinsWin.
  public void IncreaseNbCoins(int coinsWin){
    _nbCoins += coinsWin;
  }

  // To decrease the number of coins the player have by coinsLost.
  public void DecreaseNbCoins(int coinsLost){
    _nbCoins -= coinsLost;
  }

  // Attributes.
  int _nbCoins;

  public Text _textCoins;
}
