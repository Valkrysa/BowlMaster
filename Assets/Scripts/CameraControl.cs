﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public Ball ball;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - ball.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (ball.transform.position.z < 1829) // reached head pin
        {
            transform.position = ball.transform.position + offset;
        }
	}
}
