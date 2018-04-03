using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource thrustSound;
    public float thrustSpeed = 100f;
    public float rotationSpeed = 100f;

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
        Thrust();
        Rotate();
    }

    void OnCollisionEnter(Collision collision) {
        print("collision");

        switch(collision.gameObject.tag) {
            case "Friendly":
                break;
            default:
                print("dead");
                break;
        }
    }

    private void Thrust() {
        float thrustThisFrame = thrustSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)) {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            if (!thrustSound.isPlaying) thrustSound.Play();
        } else {
            thrustSound.Stop();
        }
    }

    private void Rotate() {
        float rotationThisFrame = rotationSpeed * Time.deltaTime;
        rigidBody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) {
            print("no rotate");
        } else if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.back * rotationThisFrame);
        }

        rigidBody.freezeRotation = false;
    }

    
}
