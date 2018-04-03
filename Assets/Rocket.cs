using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcesInput();
	}

    private void ProcesInput() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) {
            print("no rotate");
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            print("rotate left");
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            print("rotate right");
        }
    }
}
