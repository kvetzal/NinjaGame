using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GroundManager : MonoBehaviour {

	public Ground groundCubePrefab;
	public Platform platformCubePrefab;
	public EnemyAreaBlocks enemyAreaCubePrefab;
	
	public float xLocationForTheWin;
	private int minGap = 8, maxGap = 30;
	private const int MAXJUMP = 12;
	// max gap jumpable in the x direction is 12
	private const int numberOfGaps = 6;
	
	public Ground[] groundCubes;
	public EnemyAreaBlocks[] enemyAreaCubes;
	public List<Platform> platformCubes = new List<Platform>();
	
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
		
		int minEnemyAreas = 1, maxEnemyAreas = 4;
		enemyAreas = Random.Range(minEnemyAreas, maxEnemyAreas);
		
		enemyAreaLocations = new int[enemyAreas];
		groundCubes = new Ground[numberOfGaps + 1];
		enemyAreaCubes = new EnemyAreaBlocks[enemyAreaLocations.Length * 2];
		
		int enemyAreaLocationIndexID = 0, enemyAreaCubesIndexID = 0;
		
		//Vector3 cubePosition = new Vector3(0, 0, 0);
		float cubeHeight;
		bool firstGroundBlock = true;
		
		float gap = 0f;
		
		for (int i = 0; i < groundCubes.Length; i++) {
			groundCubes[i] = (Ground)Instantiate(groundCubePrefab);
			
			if (!firstGroundBlock) {
				gap = Random.Range(minGap, maxGap);
				groundCubes[i].SetPosition(new Vector3((float)(groundCubes[i - 1].GetPosition().x + (groundCubes[i - 1].GetScale().x / 2) + gap + (groundCubes[i].GetScale().x / 2)),
					groundCubes[i].GetPosition().y, groundCubes[i].GetPosition().z));
			}
			else {
				firstGroundBlock = false;
			}
			
			if (gap > MAXJUMP) {
				PlatformSetUp(i, gap);
			}
			
			GenerateEnemyAreas(enemyAreaLocationIndexID, enemyAreaCubesIndexID, i);
			
			if (i == numberOfGaps) {
				xLocationForTheWin = groundCubes[i].GetPosition().x;
			}
		}
	}
	
	private void DestroyOldGround() {
		for (int i = 0; i < groundCubes.Length; i++) {
			if (groundCubes[i] != null) {
				groundCubes = null;
			}
		}
		for (int i = 0; i < platformCubes.Count; i++) {
			if (platformCubes[i] != null) {
				Destroy(platformCubes[i].gameObject);
				platformCubes = null;
			}
		}
		for (int i = 0; i < enemyAreaCubes.Length; i++) {
			if (enemyAreaCubes[i] != null) {
				enemyAreaCubes = null;
			}
		}
	}
	
	private void GenerateEnemyAreas(int enemyAreaLocationIndexID, int enemyAreaCubesIndexID, int groundCubeIndexID) {
		int enemyAreaPercentChance = 25;
		int createEnemyArea = Random.Range(0, 100);
			
		if (createEnemyArea < enemyAreaPercentChance) {
			enemyAreaLocations[enemyAreaLocationIndexID] = groundCubeIndexID;
			
			enemyAreaCubes[enemyAreaCubesIndexID] = (EnemyAreaBlocks)Instantiate(enemyAreaCubePrefab);
			enemyAreaCubes[enemyAreaCubesIndexID].SetPosition(groundCubes[groundCubeIndexID].GetPosition(),
				groundCubes[groundCubeIndexID].GetScale());
			
			enemyAreaPercentChance = 25;
			enemyAreaLocationIndexID++;
			enemyAreaCubesIndexID += 2;
		}
		else
			enemyAreaPercentChance += 25;
	}
	
	private void PlatformSetUp(int groundCubeIndexID, float cubeGap) {
		float minPlatformDistanceX = 3, maxPlatformDistanceX = 6, 
		minPlatformDistanceY = 1, maxPlatformDistanceY = 3;
		
		float gapX = Random.Range(minPlatformDistanceX, maxPlatformDistanceX);
		float gapY = Random.Range(minPlatformDistanceY, maxPlatformDistanceY);
		
		float gapLeftToJump = cubeGap;
		
		do {
			Platform platform = (Platform)Instantiate(platformCubePrefab);
			
			if (Random.Range(0, 100) > 50) {
				gapY = gapY * -1;
			}
			
			if (gapLeftToJump == cubeGap) {
				platform.SetPosition(new Vector3(groundCubes[groundCubeIndexID - 1].GetPosition().x + (groundCubes[groundCubeIndexID - 1].GetScale().x / 2) + gapX, 
					groundCubes[groundCubeIndexID - 1].GetPosition().y + (groundCubes[groundCubeIndexID - 1].GetScale().y / 2) + gapY,
					0f));
			}
			else {
				platform.SetPosition(new Vector3(platformCubes.Last().GetPosition().x + gapX, 
					platformCubes.Last().GetPosition().y + gapY,
					0f));
			}
			
			platformCubes.Add(platform);
				
			gapLeftToJump -= (gapX + platform.GetScale().x);
				
		}while(gapLeftToJump > maxPlatformDistanceX);
	}
	
	private void GameOver() {
	}
	
	public int[] GetEnemyAreaLocations() {
		return enemyAreaLocations;
	}
}