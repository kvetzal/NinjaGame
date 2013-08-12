using UnityEngine;
using System.Collections;

public class Ninja : MonoBehaviour {
	
	private float verticalJumpVelocity;
	
	public static float yLocationForGameOver;
	private bool touchingPlatform;
	
	private float movementSpeed;
	
	void Start() {
		// Instantiates the GameEvents in GameEventManager
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		
		Controls.tapMiddle += TapMiddle;
		Controls.touchLeftEdge += MoveLeft;
		Controls.touchRightEdge += MoveRight;
		// sets the position of the object to the position stated in the inspector and stops the player character from appearing during start screen
		gameObject.renderer.enabled = false;
		rigidbody.isKinematic = true;	//	rigidbody.IsKinematic look into physics tutorial
		rigidbody.useGravity = true;
		
		collider.tag = "Ninja";
		
		movementSpeed = 0.25f;
		yLocationForGameOver = -6f;
		verticalJumpVelocity = 8f;
	}
	
	void Update() {
		// Ends the game whem the player falls below the y Location for Game over
		if (transform.position.y < yLocationForGameOver)
			GameEventManager.TriggerGameOver();
		/*if (Input.anyKeyDown) {
			GameEventManager.TriggerGameOver();
		}*/
		
	}
	
	void OnCollisionEnter() {
		// when the player sets touches the platform this allows them to jump again
		touchingPlatform = true;
	}
	
	void OnCollisionExit() {
		// this stops the player from jumping when they are mid air
		touchingPlatform = false;
	}
	
	public Vector3 GetNinjaPosition() {
		return transform.position;
	}
	
	private void GameStart() {
		rigidbody.isKinematic = false; 	// look into physics tutorial
		rigidbody.useGravity = true;
		transform.position = new Vector3(0f, 2f, 0f);
		gameObject.renderer.enabled = true;
		enabled = true;
	}
	
	private void GameOver() {
		rigidbody.isKinematic = true;
		
		enabled = false;	//disables the player when the Game Over Screen is up
	}
	
	private void MoveLeft() {
		Vector3 NewPosition = new Vector3(transform.position.x - movementSpeed, transform.position.y, transform.position.z);
		transform.position = NewPosition;
	}
	
	private void MoveRight() {
		Vector3 NewPosition = new Vector3(transform.position.x + movementSpeed, transform.position.y, transform.position.z);
		transform.position = NewPosition;
	}
	
	private void TapMiddle() {
		rigidbody.AddForce(new Vector3(0f, verticalJumpVelocity, 0f), ForceMode.VelocityChange);
	}
}