using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Vector3 launchVelocity;

    private Rigidbody myRigidBody;
    private AudioSource audioSource;
    private bool inPlay;
    private Vector3 startPosition;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody>();
        myRigidBody.useGravity = false;

        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;

        inPlay = false;
    }

    // Update is called once per frame
    void Update () {

    }

    public void Reset() {
        myRigidBody.useGravity = false;
        myRigidBody.velocity = Vector3.zero;
        myRigidBody.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;

        inPlay = false;
    }

    public bool HasLaunched () {
        return inPlay;
    }

    public void Launch (Vector3 velocity) {
        inPlay = true;

        // myRigidBody.AddForce(transform.forward * 100000.0f);
        myRigidBody.useGravity = true;
        myRigidBody.velocity = velocity;

        audioSource.Play();
    }
}
