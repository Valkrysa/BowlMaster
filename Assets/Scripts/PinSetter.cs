using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
    
    public GameObject pinSet;
    
    private Ball ball;
    private Animator animator;
    private ActionMaster actionMaster; // can't allow more than one instance
    private PinCounter pinCounter;
    
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
    }

    public void PerformAction (ActionMaster.Action action) {
        if (action == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
        } else if (action == ActionMaster.Action.Reset) {
            animator.SetTrigger("resetTrigger");
            pinCounter.Reset();
        } else if (action == ActionMaster.Action.EndTurn) {
            animator.SetTrigger("resetTrigger");
            pinCounter.Reset();
        } else if (action == ActionMaster.Action.EndGame) {
            throw new UnityException("Don't know how to handle end of game yet");
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
}
