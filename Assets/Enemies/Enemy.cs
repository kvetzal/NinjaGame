/*using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	
	public GameObject groundPrefab;
	public LineRenderer linePrefab;
	public  GameObject enemyPrefab;
	
	public List<GameObject> enemies = new List<GameObject>();
	public List<GameObject> enemyBlocks = new List<GameObject>();
	
	private int numOfEnemies, maxEnemies = 1, minEnemies = 4;
	
	private bool enemyGeneratorCalled = false;
	
	int count = 0;
	
	private List<LineRenderer> lines = new List<LineRenderer>();
	
	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
	}
	
	void Update () {
		enemyUpdate();
	}
	
	private void enemyConstructor() {
		for (int i = 0; i < enemies.Count; i++) {
			if (enemies[i] != null) {
				//enemies[i] = (GameObject)Instantiate(enemyPrefab);
				enemies[i].rigidbody.isKinematic = true;
				enemies[i].transform.position = new Vector3(2f, 2f, 0);
			}
		}
	}
	
	public void enemyUpdate() {
		if (Ground.groundCubes[Ground.groundCubes.Length - 1] != null && enemyGeneratorCalled == false) {
			Debug.Log("Enemy Genertor called");
			EnemyGenerator();
		}
		
		foreach (GameObject enemy in enemies) {
			List<Ray> visionRays = new List<Ray>();
			if (enemy != null) {
				//math example
				//float num = Mathf.Sin(Mathf.PI * 90 / 180);
				// returns 1
				
				const int ENDINGDEGREE = -45;
				int degree = 45;
				
				do {
					Mathf works in radians
					see figure 1
					
					double rayVectorX = Mathf.Cos(Mathf.PI * degree / 180);
					double rayVectorY = Mathf.Sin(Mathf.PI * degree / 180);
					Vector3 rayVector = new Vector3((float)rayVectorX, (float)rayVectorY, 0f);
					Ray ray = new Ray(enemy.transform.position, rayVector);
					
					degree -= 10;
					visionRays.Add(ray);
				}while(degree > ENDINGDEGREE);
				
				for (int i = 0; i < lines.Count; i++) {
					if (lines[i] != null) {
						Destroy(lines[i].gameObject);
					}
				}
				
				foreach (Ray ray in visionRays) {
					if (ray.GetPoint(7).x > 0) {
						lines.Add((LineRenderer)Instantiate(linePrefab));
						lines.Reverse();
						lines[0].SetPosition(0, ray.origin);
						lines[0].SetPosition(1, ray.GetPoint(7));
						lines.Reverse();
						RaycastHit hit;
						if (Physics.Raycast(ray, out hit, ray.GetPoint(7).x)) {
							if (hit.collider.tag != "Ninja") {
							}
							else if (hit.collider.tag == "Ninja") {
								count++;
								if (count % 100 == 0) {
									Debug.Log("Ninja was seen " + count + " times");
								}
							}
						}
					}
				}
			}
		}
	}
	
	private void GameStart() {
		for (int i = 0; i < enemies.Count; i++) {
			if (enemies[i] != null) {
				Destroy(enemies[i].gameObject);
				enemies.Remove(enemies[i]);
			}
		}
		for (int i = 0; i < enemyBlocks.Count; i++) {
			if (enemyBlocks[i] != null) {
				Destroy(enemyBlocks[i].gameObject);
				enemyBlocks.Remove(enemyBlocks[i]);
			}
		}
		if (Ground.groundCubes[Ground.groundCubes.Length - 1] != null) {
			EnemyGenerator();
		}
		enemyConstructor();
		foreach (GameObject enemy in enemies) {
			if (enemy != null) {
				enemy.rigidbody.isKinematic = false;
			}
		}
	}
	
	private void EnemyGenerator() {
		numOfEnemies = Random.Range(minEnemies, maxEnemies);
		Debug.Log("Number of Enemies: " + numOfEnemies);
		int enemyCount = 0;
		for (int i = (Ground.groundCubes.Length - 1); i > -1; i--) {
			if (Ground.groundCubes[i].localScale.x > 10 && enemyCount < numOfEnemies) {
				GameObject firstBlock = (GameObject)Instantiate(groundPrefab);
				firstBlock.transform.localScale = new Vector3(2f, 2f, 1f);
				float firstBlockPositionX = Ground.groundCubes[i].position.x - (Ground.groundCubes[i].localScale.x / 2) + (firstBlock.transform.localScale.x / 2);
				float firstBlockPositionY = Ground.groundCubes[i].position.y + (Ground.groundCubes[i].localScale.y / 2) + (firstBlock.transform.localScale.y / 2);
				firstBlock.transform.position = new Vector3(firstBlockPositionX, firstBlockPositionY, 0f);
				enemyBlocks.Add(firstBlock);
				
				GameObject secondBlock = (GameObject)Instantiate(groundPrefab);
				secondBlock.transform.localScale = new Vector3(2f, 2f, 1f);
				float secondBlockPositionX = Ground.groundCubes[i].position.x + (Ground.groundCubes[i].localScale.x / 2) - (secondBlock.transform.localScale.x / 2);
				float secondBlockPositionY = Ground.groundCubes[i].position.y + (Ground.groundCubes[i].localScale.y / 2) + (secondBlock.transform.localScale.y / 2);
				secondBlock.transform.position = new Vector3(secondBlockPositionX, secondBlockPositionY, 0f);
				enemyBlocks.Add(secondBlock);
				
				GameObject newEnemy = (GameObject)Instantiate(enemyPrefab);
				newEnemy.transform.position = new Vector3 (Ground.groundCubes[i].position.x, 2f, 0f);
				enemies.Add(newEnemy);
				enemyCount++;
			}
		}
		enemyGeneratorCalled = true;
	}
	
	private void GameOver() {
		foreach (GameObject enemy in enemies) {
			if (enemy != null) {
				enemy.rigidbody.isKinematic = true;
			}
		}
		enemyGeneratorCalled = false;
	}
}*/