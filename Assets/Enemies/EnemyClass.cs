using UnityEngine;
using System.Collections.Generic;

public class EnemyClass : MonoBehaviour {
	
	public GameObject enemyPrefab;
	public LineRenderer linePrefab;
	
	private List<Ray> visionRays = new List<Ray>();
	
	private GameObject enemy;
	
	int count = 0;
	
	//private List<LineRenderer> lines = new List<LineRenderer>();
	
	void Start () {
		enemy = (GameObject)Instantiate(enemyPrefab);
		enemy.transform.position = new Vector3(2, 2, 0);
	}
	
	public void EnemyUpdate () {
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
			Vector3 rayVector = new Vector3(Mathf.Cos(Mathf.PI * degree / 180), Mathf.Sin(Mathf.PI * degree / 180), 0f);
			//Debug.Log (rayVector);
			// Mathf works in radians
			// see figure 1
			
			/*LineRenderer line = (LineRenderer)Instantiate(linePrefab);
			line.SetPosition(0, enemy.position);
			
			Vector3 lineEnding = new Vector3(rayVector.x + enemy.position.x,
				rayVector.y + enemy.position.y,
				0f);
			
			line.SetPosition(1, lineEnding);*/
			Ray ray = new Ray(enemy.transform.position, rayVector);
			
			degree -= 5;
			
			//lines.Add(line);
			visionRays.Add(ray);
		}while(degree > ENDINGDEGREE);
		
		foreach (Ray ray in visionRays) {
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, ray.GetPoint(7).x)) {
				Debug.Log("ray hit something");
				if (hit.collider.tag == "Ninja") {
					count++;
					Debug.Log("Ninja was seen " + count + " times");
				}
			}
		}
	}
}
