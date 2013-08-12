using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	
	public GameObject enemyPrefab;
	public LineRenderer linePrefab;
	
	public List<GameObject> Enemies = new List<GameObject>();
	
	private List<Ray> visionRays = new List<Ray>();
	
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
			Enemies[i] = (GameObject)Instantiate(enemyPrefab);
			Enemies[i].rigidbody.isKinematic = true;
			Enemies[i].transform.position = new Vector3(2f, 2f, 0);
		}
	}
	
	private void enemyUpdate() {
		foreach (GameObject enemy in Enemies) {
			//enemy.transform.position = new Vector3(2f, 2f, 0);
			//math example
			//float num = Mathf.Sin(Mathf.PI * 90 / 180);
			// returns 1
			
			for (int i = 0; i < lines.Count; i++) {
				if (lines[i] != null) {
					Destroy(lines[i].gameObject);
				}
			}
			
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
				
				LineRenderer line = (LineRenderer)Instantiate(linePrefab);
				line.SetPosition(0, enemy.transform.position);
				
				line.SetPosition(1, ray.GetPoint(7));
				lines.Add(line);
				
				degree -= 5;
				visionRays.Add(ray);
			}while(degree > ENDINGDEGREE);
			
			foreach (Ray ray in visionRays) {
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, ray.GetPoint(7).x)) {
	
					if (hit.collider.tag == "Ninja") {
						count++;
						Debug.Log("Ninja was seen " + count + " times");
					}
				}
			}
		}
	}
	
	private void GameStart() {
		Enemies.Add(new GameObject("Enemy"));
		enemyConstructor();
		foreach (GameObject enemy in Enemies) {
			enemy.rigidbody.isKinematic = false;
		}
	}
	
	private void GameOver() {
		foreach (GameObject enemy in Enemies) {
			enemy.rigidbody.isKinematic = true;
		}
	}
}
