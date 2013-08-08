using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	
	// Hey Mom!
	
	public Camera mainCamera;
	
	public delegate void ControllerEventManager();
	
	public static event ControllerEventManager swipe, touchLeftEdge, touchRightEdge, tapMiddle;
	
	void Start() {
		foreach (Touch touch in Input.touches) {
			TouchAI(touch);
		}
	}
	
	void Update() {
		foreach (Touch touch in Input.touches) {
			TouchAI(touch);
		}
	}
	
	private void TouchAI(Touch CurrentTouch) {
		float screenWidth = Screen.currentResolution.width;
		
		float CurrentTouchPositionX = CurrentTouch.position.x;
		float EdgeOfScreenBoundry = 100f;
		
		if (CurrentTouchPositionX > EdgeOfScreenBoundry &&
			CurrentTouchPositionX < screenWidth - EdgeOfScreenBoundry) {
			if (CurrentTouch.phase == TouchPhase.Ended) {
				TriggerTapMiddle();
			}
		}
		else if (CurrentTouchPositionX < EdgeOfScreenBoundry) {
			TriggerTouchLeftEdge();
		}
		else if (CurrentTouchPositionX > screenWidth - EdgeOfScreenBoundry) {
			TriggerTouchRightEdge();
		}
	}
	
	public static void TriggerSwipe() {
		if (swipe != null)
			swipe();
	}
	
	public static void TriggerTouchLeftEdge() {
		if (touchLeftEdge != null)
			touchLeftEdge();
	}
	
	public static void TriggerTouchRightEdge() {
		if (touchRightEdge != null)
			touchRightEdge();
	}
	
	public static void TriggerTapMiddle() {
		if (tapMiddle != null)
			tapMiddle();
	}
	
	private void GameStart() {
		enabled = true;
	}
	
	private void GameOver() {
		enabled = false;
	}
}
