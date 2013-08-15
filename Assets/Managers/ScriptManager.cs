using UnityEngine;
using System.Collections.Generic;

public class ScriptManager : MonoBehaviour {
	
	public EnemyClass enemyBehaviourPrefab;
	public Ground groundBehaviourPrefab;
	
	private Ground groundManager;
	public List<EnemyClass> enemies;
	
	bool GamePlaying, GameEnded;
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		groundManager = (Ground)Instantiate(groundBehaviourPrefab);
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
		if (GamePlaying) {
			foreach (EnemyClass enemy in enemies) {
				if (enemy != null) {
					enemy.UpdateEnemy();
				}
			}
		}
	}
	
	private void EnemyGenerator() {
		int numOfEnemies;
		int[] enemyAreaLocations;
		enemies = new List<EnemyClass>();
		enemyAreaLocations = groundManager.GetEnemyAreaLocations();
		numOfEnemies = enemyAreaLocations.Length;
		for (int i = 0; i < numOfEnemies; i++) {
			EnemyClass newEnemy = (EnemyClass)Instantiate(enemyBehaviourPrefab);
			float positionX = groundManager.groundCubes[enemyAreaLocations[i]].transform.position.x;
			positionX += groundManager.groundCubes[enemyAreaLocations[i]].transform.localScale.x / 2;
			
			newEnemy.transform.position = new Vector3 (positionX, 2f, 0f);
			enemies.Add(newEnemy);
		}
	}
}



/*
 * public List<EnemyClass> enemies;
	
	int numOfEnemies;
	
	int[] enemyAreaLocations;
	
	public EnemyManager (EnemyClass newEnemyBehaviourPrefab, Ground groundScript, GameObject newEnemyPrefab, LineRenderer newLinePrefab) {
		enemyPrefab = newEnemyPrefab;
		linePrefab = newLinePrefab;
		enemyBehaviourPrefab = newEnemyBehaviourPrefab;
		
		
		EnemyGenerator(groundScript);
	}
	
	
	*/