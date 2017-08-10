using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Ball))]
public class BallDragLaunch : MonoBehaviour {

    private Ball ball;
    private float startSwipeTime;
    private Vector3 startCursorPosition;

	// Use this for initialization
	void Start () {
        ball = GetComponent<Ball>();
	}

    public void MoveStart (float amount) {
        if (!ball.HasLaunched()) {
            if (
                amount > 0 && ball.transform.position.x <= 45 ||
                amount < 0 && ball.transform.position.x >= -45
            ) {
                ball.transform.Translate(new Vector3(amount, 0, 0), Space.World);
            }
        }
    }

    public void DragStart () {
        // Capture time and postion of drag start
        startSwipeTime = Time.time;
        startCursorPosition = Input.mousePosition;
    }

    public void DragEnd () {
        // launch ball
        float endSwipeTime = Time.time;
        Vector3 endCursorPosition = Input.mousePosition;

        float swipeDuration = endSwipeTime - startSwipeTime;
        float launchSpeedX = (endCursorPosition.x - startCursorPosition.x) / swipeDuration;
        float launchSpeedZ = (endCursorPosition.y - startCursorPosition.y) / swipeDuration;

        Vector3 launchVector = new Vector3(launchSpeedX, 0f, launchSpeedZ);

        if (!ball.HasLaunched()) {
            ball.Launch(launchVector);
        }
    }
}
