using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    public int lastStandingCount = -1;
    public GameObject pinSet;

    private Ball ball;
    private float lastChangeTime;
    private bool ballEnteredBox = false;
    private ActionMaster actionMaster;
    private int lastSettledCount = 10;
    private Animator animator;

	// Use this for initialization
	void Start () {
        ball = GameObject.FindObjectOfType<Ball>();

        animator = GetComponent<Animator>();

        actionMaster = new ActionMaster();
    }
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballEnteredBox) {
            UpdateStandingCountAndSettle();
        }
    }

    public void RenewPins () {
        GameObject newPins = Instantiate(pinSet);
        newPins.transform.position += new Vector3(0, 50, 0);
    }

    public void RaisePins() {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.RaiseIfStanding();
        }
    }

    public void LowerPins () {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.Lower();
        }
    }

    void UpdateStandingCountAndSettle () {
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

    void PinsHaveSettled () {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        ActionMaster.Action result = actionMaster.Bowl(pinFall);

        if (result == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
        } else if (result == ActionMaster.Action.Reset) {
            lastSettledCount = 10;
            animator.SetTrigger("resetTrigger");
        } else if (result == ActionMaster.Action.EndTurn) {
            lastSettledCount = 10;
            animator.SetTrigger("resetTrigger");
        } else if (result == ActionMaster.Action.EndGame) {
            throw new UnityException("Don't know how to handle end of game yet");
        }

        ball.Reset();
        lastStandingCount = -1;
        ballEnteredBox = false;
        standingDisplay.color = Color.green;
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
