using UnityEngine;
using System.Collections.Generic;
using System.Linq;
	
public class Ground : MonoBehaviour {

	public Transform groundCubePrefab;
	public float xLocationForTheWin;
	
	private int minCubeLength = 30, maxCubeLength = 50;
	private int minGap = 8, maxGap = 30;
	private const int MAXJUMP = 12;
	
	// max gap jumpable is 12
	private const int numberOfGaps = 6;
	
	public Transform[] groundCubes = new Transform[numberOfGaps + 1];
	public List<Transform> platformCubes = new List<Transform>();
	
	public int count = 0;
	
	void Start() {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		enabled = false;
	}
	
	private void GameStart() {
		GroundSetUp();
		
		enabled = true;
	}
	
	private void GroundSetUp() {
		for (int i = 0; i < groundCubes.Length; i++) {
			if (groundCubes[i] != null) {
				Destroy(groundCubes[i].gameObject);
			}
		}
		
		Vector3 cubePosition = new Vector3(0, 0, 0);
		float gap = 0f;
		
		float cubeHeight;
		
		float currentWidth;
		float oldWidth = 0f;
		
		
		
		for (int i = 0; i < groundCubes.Length; i++) {
			currentWidth = Random.Range(minCubeLength, maxCubeLength);
			cubeHeight = Ninja.yLocationForGameOver + (Screen.currentResolution.height / 2);
			
			if (oldWidth == 0) {
				cubePosition = new Vector3(cubePosition.x, cubePosition.y, cubePosition.z);
			}
			else {
				gap = Random.Range(minGap, maxGap);
				cubePosition = new Vector3(cubePosition.x + (oldWidth / 2) + (currentWidth / 2) + gap, cubePosition.y, cubePosition.z);	
			}
			groundCubes[i] = (Transform)Instantiate(groundCubePrefab);
			groundCubes[i].position = new Vector3(cubePosition.x, cubePosition.y - (cubeHeight / 2), cubePosition.z);
			groundCubes[i].localScale = new Vector3(currentWidth, cubeHeight, groundCubes[i].localScale.z);
			
			groundCubes[i].renderer.enabled = true;
			oldWidth = currentWidth;
			
			if (gap > MAXJUMP) {
				PlatformSetUp(i, gap);
			}
			
			if (i == numberOfGaps) {
				xLocationForTheWin = groundCubes[i].position.x;
			}
		}
	}
	
	private void PlatformSetUp(int groundCubeIndexID, float cubeGap) {
		float minPlatformDistanceX = 3, 
		maxPlatformDistanceX = 6, 
		minPlatformDistanceY = 1, 
		maxPlatformDistanceY = 3;
		const float platformWidth = 5;
		const float PLATFORMHEIGHT = 1;
		
		float gapX = Random.Range(minPlatformDistanceX, maxPlatformDistanceX);
		float gapY = Random.Range(minPlatformDistanceY, maxPlatformDistanceY);
		
		float gapLeftToJump = cubeGap;
		
		do {
			count++;
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
		for (int i = 0; i < platformCubes.Count; i++) {
			if (platformCubes[i] != null) {
				Destroy(platformCubes[i].gameObject);
			}
		}
		enabled = false;
	}
}
