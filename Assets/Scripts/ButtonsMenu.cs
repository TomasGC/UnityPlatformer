using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMenu : MonoBehaviour {

  // Use this for initialization.
  void Start(){
    _paused = false;

    // To avoid displaying the pause menu when we start the game.
    _pauseUI.SetActive(false);
	}

  // Update is called once per frame.
  void Update(){
    if(Input.GetButtonDown("Pause")){
      _paused = !_paused;
    }

    int timeScale = (_paused) ? 0 : 1;
    PauseTheGame(timeScale);
    
  }

  // Pause the game and display the pause menu.
  void PauseTheGame(int FreezeGame){
    // Display the Pause Menu if _paused is true.
    _pauseUI.SetActive(_paused);

    // Freeze the game during the game is paused.
    Time.timeScale = FreezeGame;
  }

  // When we push the Pause button, this method will be triggered and pause the game.
  public void Pause()
  {
    _paused = !_paused;
  }

  // When we push the Replay button, this method will be triggered and replay the game.
  public void Replay()
  {
    SceneManager.LoadScene(0);
  }


  // Attributes.
  public GameObject _pauseUI;
  private bool _paused;

}
