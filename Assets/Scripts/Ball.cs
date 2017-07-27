using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float launchSpeed;

    private Rigidbody rigidBody;
    private AudioSource audioSource;

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        Launch();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Launch()
    {
        // rigidBody.AddForce(transform.forward * 100000.0f);
        rigidBody.velocity = new Vector3(0, 0, launchSpeed);
        audioSource.Play();
    }
}
