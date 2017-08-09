using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    public Text standingDisplay;

    private bool ballLeftBox = false;
    private int lastSettledCount = 10;
    private int lastStandingCount = -1;
    private float lastChangeTime;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballLeftBox) {
            standingDisplay.color = Color.red;
            UpdateStandingCountAndSettle();
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.GetComponent<Ball>()) {
            ballLeftBox = true;
        }
    }

    void UpdateStandingCountAndSettle() {
        int standingCount = CountStanding();

        if (standingCount != lastStandingCount) {
            lastStandingCount = standingCount;
            lastChangeTime = Time.time;
            return;
        }

        float changeInTime = Time.time - lastChangeTime;
        if (changeInTime >= 3f) {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled() {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        gameManager.Bowl(pinFall);

        lastStandingCount = -1;
        ballLeftBox = false;
        standingDisplay.color = Color.green;
    }

    public void Reset() {
        lastSettledCount = 10;
    }

    private int CountStanding() {
        int standingCount = 0;

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                standingCount++;
            }
        }
        return standingCount;
    }
}
