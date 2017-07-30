using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;

    private bool ballEnteredBox = false;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();
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
