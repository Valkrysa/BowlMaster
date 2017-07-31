using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    public int lastStandingCount = -1;

    private Ball ball;
    private float lastChangeTime;
    private bool ballEnteredBox = false;

	// Use this for initialization
	void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
    }
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballEnteredBox) {
            CheckStandingCount();
        }
    }

    void CheckStandingCount () {
        int standingCount = CountStanding();

        if (standingCount != lastStandingCount) {
            lastStandingCount = standingCount;
            lastChangeTime = Time.time;
            return;
        }

        float changeInTime = Time.time - lastChangeTime;
        if (changeInTime >= 3) {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled () {
        ball.Reset();
        lastStandingCount = -1;
        ballEnteredBox = false;
        standingDisplay.color = Color.green;
    }

    void OnTriggerExit (Collider other) {
        GameObject thingLeft = other.gameObject;

        if (thingLeft.GetComponent<Pin>()) {
            Destroy(thingLeft);
        }
    }

    void OnTriggerEnter (Collider other) {
        GameObject thingHit = other.gameObject;

        if (thingHit.GetComponent<Ball>()) {
            standingDisplay.color = Color.red;
            ballEnteredBox = true;
        }
    }

    private int CountStanding () {
        int standingCount = 0;
        
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                standingCount++;
            }
        }
        return standingCount;
    }
}
