using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    // TODO: Fix lighting bug
    Rigidbody rigidBody;
    AudioSource thrustSound;
    enum State { Alive, Dying, Transitioning }
    State state = State.Alive;
    public float thrustSpeed = 100f;
    [SerializeField] float rotationSpeed = 100f; // not accessible in other scripts, but accessabile in editor

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
        if (state == State.Alive) {
            Thrust();
            Rotate();
        }
    }

    void OnCollisionEnter(Collision collision) {
        print("collision");

        switch(collision.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transitioning;
                print("finish");
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel", 1f);
                break;
        }
    }

    private void LoadNextLevel() {
        state = State.Alive;
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel() {
        state = State.Alive;
        SceneManager.LoadScene(0);
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
