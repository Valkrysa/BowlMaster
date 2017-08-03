using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3f;
    public float distanceToRaise = 40.0f;

    private Rigidbody myRigidBody;

    void Awake() {
        this.GetComponent<Rigidbody>().solverVelocityIterations = 10;
    }

    // Use this for initialization
    void Start () {
        myRigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        IsStanding();
    }

    public void RaiseIfStanding () {
        if (IsStanding()) {
            myRigidBody.useGravity = false;
            myRigidBody.velocity = Vector3.zero;
            myRigidBody.angularVelocity = Vector3.zero;
            transform.Translate(new Vector3(0, distanceToRaise, 0), Space.World);
        } 
    }

    public void Lower () {
        transform.Translate(new Vector3(0, -distanceToRaise, 0), Space.World);
        myRigidBody.useGravity = true;
        myRigidBody.velocity = Vector3.zero;
        myRigidBody.angularVelocity = Vector3.zero;
    }

    public bool IsStanding () {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;
        float tiltInX = Mathf.Abs(270 - rotationInEuler.x);
        float tiltInZ = Mathf.Abs(rotationInEuler.z);

        if (tiltInX < standingThreshold && tiltInZ < standingThreshold) {
            return true;
        } else {
            return false;
        }
    }
}
