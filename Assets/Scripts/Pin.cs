using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3f;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        IsStanding();
    }

    public bool IsStanding () {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;
        float tiltInX = Mathf.Abs(rotationInEuler.x);
        float tiltInZ = Mathf.Abs(rotationInEuler.z);
        if (
            (tiltInX > standingThreshold && tiltInX < (360f - standingThreshold)) ||
            (tiltInZ > standingThreshold && tiltInZ < (360f - standingThreshold))
        ) {
            return false;
        }

        return true;
    }
}
