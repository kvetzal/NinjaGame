using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	
	public List<EnemyClass> enemies = new List<EnemyClass>();
	
	int numOfEnemies;
	
	int[] enemyAreaLocations;
	
	private void Start() {
		enemyAreaLocations = GameObject.Find("EnemyManager").GetComponent<Ground>().GetEnemyAreaLocations();
		numOfEnemies = enemyAreaLocations.Length;
		EnemyGenerator();
	}
	
	private void EnemyGenerator() {
		for (int i = 0; i < numOfEnemies; i++) {
			EnemyClass newEnemy = new EnemyClass();
			float positionX = GameObject.Find("EnemyManager").GetComponent<Ground>().groundCubes[enemyAreaLocations[i]].transform.position.x;
			positionX += GameObject.Find("EnemyManager").GetComponent<Ground>().groundCubes[enemyAreaLocations[i]].transform.localScale.x / 2;
			
			newEnemy.transform.position = new Vector3 (positionX, 2f, 0f);
			enemies.Add(newEnemy);
		}
	}
}
