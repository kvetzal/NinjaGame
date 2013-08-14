using UnityEngine;
using System.Collections;

public class ScriptManager : MonoBehaviour {
	
	private Ground groundManager;
	private EnemyManager enemyManager;
	
	bool GamePlaying, GameEnded;
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		groundManager = new Ground();
		enemyManager = new EnemyManager(groundManager);
		GamePlaying = false;
		GameEnded = false;
	}
	
	private void GameStart() {
		GamePlaying = true;
		GameEnded = false;
	}
	
	private void GameOver() {
		GamePlaying = false;
		GameEnded = true;
	}
	
	void Update() {
		if (!GamePlaying) {
			if (Input.anyKeyDown)
				GameEventManager.TriggerGameStart();
		}
	}
}
