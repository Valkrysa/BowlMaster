using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public Vector3 launchVelocity;
    private Rigidbody myRigidBody;
    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myRigidBody.useGravity = false;

        audioSource = GetComponent<AudioSource>();
        //Launch(launchVelocity);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Launch(Vector3 velocity)
    {
        // myRigidBody.AddForce(transform.forward * 100000.0f);
        myRigidBody.useGravity = true;
        myRigidBody.velocity = velocity;

        audioSource.Play();
    }
}
