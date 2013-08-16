using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {
	
	public GameObject platformCubePrefab;
	private GameObject platfromCube;
	
	bool StartCalled = false;
	
	void Start () {
		if (!StartCalled) {
			StartCalled = true;
			int minWidth = 2, maxWidth = 5;
			int width = Random.Range(minWidth, maxWidth);
			
			platfromCube = (GameObject)Instantiate(platformCubePrefab);
			platfromCube.transform.localScale = new Vector3(width, 1f, 1f);
		}
	}
	
	public void SetPosition(Vector3 newPosition) {
		if (!StartCalled) {
			Start();
		}
		platfromCube.transform.position = new Vector3(newPosition.x + (platfromCube.transform.localScale.x / 2),
			newPosition.y + (platfromCube.transform.localScale.y / 2),
			newPosition.z);
	}
	
	public Vector3 GetScale() {
		if (!StartCalled) {
			Start();
		}
		return platfromCube.transform.localScale;
	}
	
	public Vector3 GetPosition() {
		if (!StartCalled) {
			Start();
		}
		return platfromCube.transform.position;
	}
}
