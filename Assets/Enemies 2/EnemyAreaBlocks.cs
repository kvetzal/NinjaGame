using UnityEngine;
using System.Collections;

public class EnemyAreaBlocks : MonoBehaviour {

	public GameObject enemyAreaCubePrefab;
	private GameObject firstEnemyAreaCube;
	private GameObject secondEnemyAreaCube;
	
	bool StartCalled = false;
	
	void Start () {
		if (!StartCalled) {
			StartCalled = true;
			firstEnemyAreaCube = (GameObject)Instantiate(enemyAreaCubePrefab);
			firstEnemyAreaCube.transform.localScale = new Vector3(1f, 1f, 1f);
			
			secondEnemyAreaCube = (GameObject)Instantiate(enemyAreaCubePrefab);
			secondEnemyAreaCube.transform.localScale = new Vector3(1f, 1f, 1f);
		}
	}
	
	public void SetPosition(Vector3 groundCubePosition, Vector3 groundCubeScale) {
		if (!StartCalled) {
			Start();
		}
		int positionX = (int)(groundCubePosition.x - (groundCubeScale.x / 2) + (firstEnemyAreaCube.transform.localScale.x / 2));
		int positionY = (int)(groundCubePosition.y + (groundCubeScale.y / 2) + (firstEnemyAreaCube.transform.localScale.y / 2));
		
		firstEnemyAreaCube.transform.position = new Vector3(positionX, positionY, 0f);
		
		positionX = (int)(groundCubePosition.x + (groundCubeScale.x / 2) - (secondEnemyAreaCube.transform.localScale.x / 2));
		secondEnemyAreaCube.transform.position = new Vector3(positionX, positionY, 0f);
	}
}
