using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {
	
	public GameObject groundCubePrefab;
	private GameObject groundCube;
	
	bool StartCalled = false;
	
	void Start() {
		if (!StartCalled) {
			StartCalled = true;
			int minWidth = 30, maxWidth = 50;
			int width = Random.Range(30, 50);
			int height = (int)(Ninja.yLocationForGameOver + (Screen.height / 2));
			
			groundCube = (GameObject)Instantiate(groundCubePrefab);
			groundCube.transform.localScale = new Vector3(width, height, 1f);
			groundCube.transform.position = new Vector3(0 + (groundCube.transform.localScale.x / 2),
				0 - (groundCube.transform.localScale.y / 2),
				0);
		}
	}
	
	public void SetPosition(Vector3 newPosition) {
		if (!StartCalled) {
			Start();
		}
		groundCube.transform.position = new Vector3(newPosition.x,
			newPosition.y,
			newPosition.z);
	}
	
	public Vector3 GetPosition() {
		if (!StartCalled) {
			Start();
		}
		return groundCube.transform.position;
	}
	
	public Vector3 GetScale() {
		if (!StartCalled) {
			Start();
		}
		return groundCube.transform.localScale;
	}
}
