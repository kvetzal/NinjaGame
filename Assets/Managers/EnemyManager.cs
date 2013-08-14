using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	
	public List<EnemyClass> enemies = new List<EnemyClass>();
	
	int numOfEnemies;
	
	int[] enemyAreaLocations;
	
	public EnemyManager (Ground groundScript) {
		enemyAreaLocations = groundScript.GetEnemyAreaLocations();
		//enemyAreaLocations = GameObject.Find("GroundManager").GetComponent<Ground>().GetEnemyAreaLocations();
		numOfEnemies = enemyAreaLocations.Length;
		EnemyGenerator(groundScript);
	}
	
	private void EnemyGenerator(Ground groundScript) {
		for (int i = 0; i < numOfEnemies; i++) {
			EnemyClass newEnemy = new EnemyClass();
			float positionX = groundScript.groundCubes[enemyAreaLocations[i]].transform.position.x;
			positionX += groundScript.groundCubes[enemyAreaLocations[i]].transform.localScale.x / 2;
			
			newEnemy.transform.position = new Vector3 (positionX, 2f, 0f);
			enemies.Add(newEnemy);
		}
	}
}
