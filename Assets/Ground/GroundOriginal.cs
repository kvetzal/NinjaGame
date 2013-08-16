using UnityEngine;
using System.Collections.Generic;
using System.Linq;
	
public class GroundOriginal : MonoBehaviour {

	public Transform groundCubePrefab;
	public float xLocationForTheWin;
	
	private int minCubeLength = 30, maxCubeLength = 50;
	private int minGap = 8, maxGap = 30;
	private const int MAXJUMP = 12;
	
	// max gap jumpable in the x direction is 12
	private const int numberOfGaps = 6;
	
	public Transform[] groundCubes;
	public Transform[] enemyAreaCubes;
	public List<Transform> platformCubes = new List<Transform>();
	
	private int minEnemyAreas = 1, maxEnemyAreas = 4;
	
	private int enemyAreas;
	
	private int[] enemyAreaLocations;
	
	void Start() {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
	}
	
	private void GameStart() {
		GroundSetUp();
	}
	
	private void GroundSetUp() {
		DestroyOldGround();
		
		enemyAreas = Random.Range(minEnemyAreas, maxEnemyAreas);
		enemyAreaLocations = new int[enemyAreas];
		groundCubes = new Transform[numberOfGaps + 1];
		enemyAreaCubes = new Transform[enemyAreaLocations.Length * 2];
		
		int enemyAreaLocationIndexID = 0, enemyAreaCubesIndexID = 0;
		
		Vector3 cubePosition = new Vector3(0, 0, 0);
		float cubeHeight;
		float currentCubeWidth;
		float oldCubeWidth = 0f;
		
		float gap = 0f;
		
		for (int i = 0; i < groundCubes.Length; i++) {
			currentCubeWidth = Random.Range(minCubeLength, maxCubeLength);
			cubeHeight = Ninja.yLocationForGameOver + (Screen.currentResolution.height / 2);
			
			if (oldCubeWidth == 0) {
				cubePosition = new Vector3(cubePosition.x, cubePosition.y, cubePosition.z);
			}
			else {
				gap = Random.Range(minGap, maxGap);
				cubePosition = new Vector3(cubePosition.x + (oldCubeWidth / 2) + (currentCubeWidth / 2) + gap, cubePosition.y, cubePosition.z);	
			}
			
			groundCubes[i] = (Transform)Instantiate(groundCubePrefab);
			groundCubes[i].position = new Vector3(cubePosition.x, cubePosition.y - (cubeHeight / 2), cubePosition.z);
			groundCubes[i].localScale = new Vector3(currentCubeWidth, cubeHeight, groundCubes[i].localScale.z);
			
			groundCubes[i].renderer.enabled = true;
			oldCubeWidth = currentCubeWidth;
			
			if (gap > MAXJUMP) {
				PlatformSetUp(i, gap);
			}
			
			GenerateEnemyAreas(enemyAreaLocationIndexID, enemyAreaCubesIndexID, i);
			enemyAreaLocationIndexID++;
			enemyAreaCubesIndexID += 2;
			
			if (i == numberOfGaps) {
				xLocationForTheWin = groundCubes[i].position.x;
			}
		}
	}
	
	private void DestroyOldGround() {
		for (int i = 0; i < groundCubes.Length; i++) {
			if (groundCubes[i] != null) {
				Destroy(groundCubes[i].gameObject);
			}
		}
		for (int i = 0; i < platformCubes.Count; i++) {
			if (platformCubes[i] != null) {
				Destroy(platformCubes[i].gameObject);
				platformCubes.Remove(platformCubes[i]);
			}
		}
		for (int i = 0; i < enemyAreaCubes.Length; i++) {
			if (enemyAreaCubes[i] != null) {
				Destroy(enemyAreaCubes[i].gameObject);
			}
		}
	}
	
	private void GenerateEnemyAreas(int enemyAreaLocationIndexID, int enemyAreaCubesIndexID, int groundCubeIndexID) {
		int enemyAreaPercentChance = 25;
		int createEnemyArea = Random.Range(0, 100);
			
		if (createEnemyArea < enemyAreaPercentChance) {
			enemyAreaLocations[enemyAreaLocationIndexID] = groundCubeIndexID;
			
			float positionX;
			float positionY;
			
			Transform firstCube = (Transform)Instantiate(groundCubePrefab);
			firstCube.transform.localScale = new Vector3(1f, 1f, 1f);
			
			positionX = groundCubes[groundCubeIndexID].transform.position.x - (groundCubes[groundCubeIndexID].transform.localScale.x / 2) + (firstCube.transform.localScale.x / 2);
			positionY = groundCubes[groundCubeIndexID].transform.position.y + (groundCubes[groundCubeIndexID].transform.localScale.y / 2) + (firstCube.transform.localScale.y / 2);
			
			firstCube.transform.position = new Vector3(positionX, positionY, 0f);
			enemyAreaCubes[enemyAreaCubesIndexID] = firstCube;
			enemyAreaCubesIndexID++;
			
			Transform secondCube = (Transform)Instantiate(groundCubePrefab);
			positionX = groundCubes[groundCubeIndexID].transform.position.x + (groundCubes[groundCubeIndexID].transform.localScale.x / 2) - (secondCube.transform.localScale.x / 2);
			secondCube.transform.position = new Vector3(positionX, positionY, 0f);
			enemyAreaCubes[enemyAreaCubesIndexID] = secondCube;
			
			enemyAreaPercentChance = 25;
		}
		else
			enemyAreaPercentChance += 25;
	}
	
	private void PlatformSetUp(int groundCubeIndexID, float cubeGap) {
		float minPlatformDistanceX = 3, maxPlatformDistanceX = 6, 
		minPlatformDistanceY = 1, maxPlatformDistanceY = 3;
		const float platformWidth = 5;
		const float PLATFORMHEIGHT = 1;
		
		float gapX = Random.Range(minPlatformDistanceX, maxPlatformDistanceX);
		float gapY = Random.Range(minPlatformDistanceY, maxPlatformDistanceY);
		
		float gapLeftToJump = cubeGap;
		
		do {
			Transform platform = (Transform)Instantiate(groundCubePrefab);
			
			platform.localScale = new Vector3(platformWidth, PLATFORMHEIGHT, 1f);
			
			if (Random.Range(0, 100) > 50) {
				gapY = gapY * -1;
			}
			
			if (gapLeftToJump == cubeGap) {
				platform.position = new Vector3(groundCubes[groundCubeIndexID - 1].position.x + (groundCubes[groundCubeIndexID - 1].localScale.x / 2) + gapX + (platformWidth / 2), 
					groundCubes[groundCubeIndexID - 1].position.y + (groundCubes[groundCubeIndexID - 1].localScale.y / 2) + gapY - (PLATFORMHEIGHT / 2),
					0f);
			}
			else {
				platform.position = new Vector3(platformCubes.Last().position.x + (platformCubes.Last().localScale.x / 2) + gapX + (platformWidth / 2), 
					platformCubes.Last().position.y + (platformCubes.Last().localScale.y / 2) + gapY - (PLATFORMHEIGHT / 2),
					0f);
			}
			
			platformCubes.Add(platform);
				
			gapLeftToJump -= (gapX + platformWidth);
				
		}while(gapLeftToJump > maxPlatformDistanceX);
	}
	
	private void GameOver() {
	}
	
	public int[] GetEnemyAreaLocations() {
		return enemyAreaLocations;
	}
}