using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    public GameObject pinSet;
    public bool ballLeftBox = false;

    private Ball ball;
    private Animator animator;
    private ActionMaster actionMaster; // can't allow more than one instance
    private float lastChangeTime;
    private int lastSettledCount = 10;
    private int lastStandingCount = -1;

    // Use this for initialization
    void Start () {
        ball = GameObject.FindObjectOfType<Ball>();
        actionMaster = new ActionMaster();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballLeftBox) {
            standingDisplay.color = Color.red;
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
        ballLeftBox = false;
        standingDisplay.color = Color.green;
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
