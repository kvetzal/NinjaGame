using UnityEngine;
using System.Collections.Generic;

public class EnemyClass : MonoBehaviour {
	public GameObject enemyPrefab;
	public LineRenderer linePrefab;
	
	private GameObject enemyCharacter;
	private List<Ray> visionRays;
	private int directionX;
	
	void Start() {
		enemyCharacter = (GameObject)Instantiate(enemyPrefab);
		visionRays = new List<Ray>();
	}
	
	public bool PlayerSightedTest() {
		const int ENDINGDEGREE = -45;
		int degree = 45;
		
		do {
			double rayVectorX = Mathf.Cos(Mathf.PI * degree / 180);	// Mathf works in radians
			double rayVectorY = Mathf.Sin(Mathf.PI * degree / 180);
			
			Vector3 rayVector = new Vector3((float)rayVectorX, (float)rayVectorY, 0f);
			Ray ray = new Ray(enemyCharacter.transform.position, rayVector);
			visionRays.Add(ray);
			
			degree -= 10;
		}while(degree > ENDINGDEGREE);
		
		List<LineRenderer> lines = new List<LineRenderer>();
		foreach (Ray ray in visionRays) {
			lines.Add((LineRenderer)Instantiate(linePrefab));
			lines.Reverse();
			lines[0].SetPosition(0, ray.origin);
			lines[0].SetPosition(1, ray.GetPoint(7));
			lines.Reverse();
			
			if (ray.GetPoint(7).x > 0) {
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, ray.GetPoint(7).x)) {
					if (hit.collider.tag == "Ninja") {
						Debug.Log("Ninja was seen");
						return true;
					}
					else
						return false;
				}
				else
					return false;
			}
			else
				return false;
		}
		return false;
	}
	
	public void UpdateEnemy() {
		if (enemyCharacter != null) {
			enemyCharacter.transform.position = new Vector3(enemyCharacter.transform.position.x + (float)directionX,
				enemyCharacter.transform.position.y, 
				enemyCharacter.transform.position.z);
		}
	}
	
	private void OnCollisionEnter(Collision collision) {
		if (collision.collider.name == "Ninja") {
			
		}
		else {
			directionX = directionX * -1;
		}
	}
}
