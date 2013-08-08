using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GUIText gameOverTxt, instructionsTxt, ninjaAdventureTxt;
	
	void Start() {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverTxt.enabled = false;
	}
	
	void Update() {
		if (Input.anyKeyDown)
			GameEventManager.TriggerGameStart();
	}
	
	private void GameStart() {
		gameOverTxt.enabled = false;
		instructionsTxt.enabled = false;
		ninjaAdventureTxt.enabled = false;
		enabled = false;
	}
	
	private void GameOver() {
		gameOverTxt.enabled = true;
		instructionsTxt.enabled = true;
		enabled = true;
	}
}
