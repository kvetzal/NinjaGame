using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	
	public GameObject enemyPrefab;
	public GameObject groundPrefab;
	public LineRenderer linePrefab;
	
	public List<GameObject> Enemies = new List<GameObject>();
	public List<GameObject> EnemyBlocks = new List<GameObject>();
	
	int NumOfEnemies, MaxEnemies = 0, MinEnemies = 1;
	
	bool enemyGeneratorCalled = false;
	
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
		for (int i = 0; i < Enemies.Count; i++) {
			if (Enemies[i] != null) {
				//Enemies[i] = (GameObject)Instantiate(enemyPrefab);
				Enemies[i].rigidbody.isKinematic = true;
				Enemies[i].transform.position = new Vector3(2f, 2f, 0);
			}
		}
	}
	
	private void enemyUpdate() {
		if (Ground.groundCubes[Ground.groundCubes.Length - 1] != null && enemyGeneratorCalled == false) {
			EnemyGenerator();
		}
		
		foreach (GameObject enemy in Enemies) {
			List<Ray> visionRays = new List<Ray>();
			if (enemy != null) {
				//enemy.transform.position = new Vector3(2f, 2f, 0);
				//math example
				//float num = Mathf.Sin(Mathf.PI * 90 / 180);
				// returns 1
				
				/*for (int i = 0; i < lines.Count; i++) {
					if (lines[i] != null) {
						Destroy(lines[i].gameObject);
					}
				}*/
				
				const int ENDINGDEGREE = -45;
				int degree = 45;
				
				do {
					/*Debug.Log (rayVector);
					Mathf works in radians
					see figure 1*/
					
					double rayVectorX = Mathf.Cos(Mathf.PI * degree / 180);
					double rayVectorY = Mathf.Sin(Mathf.PI * degree / 180);
					Vector3 rayVector = new Vector3((float)rayVectorX, (float)rayVectorY, 0f);
					Ray ray = new Ray(enemy.transform.position, rayVector);
					
					/*LineRenderer line = (LineRenderer)Instantiate(linePrefab);
					line.SetPosition(0, enemy.transform.position);
					line.SetPosition(1, ray.GetPoint(7));
					lines.Add(line);*/
					
					degree -= 10;
					visionRays.Add(ray);
				}while(degree > ENDINGDEGREE);
				
				foreach (Ray ray in visionRays) {
					if (ray.GetPoint(7).x > 0) {
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
		for (int i = 0; i < Enemies.Count; i++) {
			if (Enemies[i] != null) {
				Destroy(Enemies[i].gameObject);
				Enemies.Remove(Enemies[i]);
			}
		}
		if (Ground.groundCubes[Ground.groundCubes.Length - 1] != null) {
			EnemyGenerator();
		}
		enemyConstructor();
		foreach (GameObject enemy in Enemies) {
			if (enemy != null) {
				enemy.rigidbody.isKinematic = false;
			}
		}
	}
	
	private void EnemyGenerator() {
		NumOfEnemies = Random.Range(MinEnemies, MaxEnemies);
		Debug.Log("Number of Enemies: " + NumOfEnemies);
		int EnemyCount = 0;
		for (int i = (Ground.groundCubes.Length - 1); i > -1; i--) {
			if (Ground.groundCubes[i].localScale.x > 10 && EnemyCount < NumOfEnemies) {
				GameObject firstBlock = (GameObject)Instantiate(groundPrefab);
				firstBlock.transform.localScale = new Vector3(2f, 2f, 1f);
				float firstBlockPositionX = Ground.groundCubes[i].position.x - (Ground.groundCubes[i].localScale.x / 2) + (firstBlock.transform.localScale.x / 2);
				float firstBlockPositionY = Ground.groundCubes[i].position.y + (Ground.groundCubes[i].localScale.y / 2) + (firstBlock.transform.localScale.y / 2);
				firstBlock.transform.position = new Vector3(firstBlockPositionX, firstBlockPositionY, 0f);
				EnemyBlocks.Add(firstBlock);
				
				GameObject secondBlock = (GameObject)Instantiate(groundPrefab);
				secondBlock.transform.localScale = new Vector3(2f, 2f, 1f);
				float secondBlockPositionX = Ground.groundCubes[i].position.x + (Ground.groundCubes[i].localScale.x / 2) - (secondBlock.transform.localScale.x / 2);
				float secondBlockPositionY = Ground.groundCubes[i].position.y + (Ground.groundCubes[i].localScale.y / 2) + (secondBlock.transform.localScale.y / 2);
				secondBlock.transform.position = new Vector3(secondBlockPositionX, secondBlockPositionY, 0f);
				EnemyBlocks.Add(secondBlock);
				
				GameObject newEnemy = (GameObject)Instantiate(enemyPrefab);
				newEnemy.transform.position = new Vector3 (Ground.groundCubes[i].position.x, 2f, 0f);
				Enemies.Add(newEnemy);
				EnemyCount++;
			}
		}
		enemyGeneratorCalled = true;
	}
	
	private void GameOver() {
		foreach (GameObject enemy in Enemies) {
			if (enemy != null) {
				enemy.rigidbody.isKinematic = true;
			}
		}
	}
}
