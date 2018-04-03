using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource thrustSound;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        thrustSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcesInput();
	}

    private void ProcesInput() {
        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!thrustSound.isPlaying) thrustSound.Play();
        } else {
            thrustSound.Stop();
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            print("no rotate");
        } else if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward);
            print("rotate left");
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back);
            print("rotate right");
        }
    }
}
