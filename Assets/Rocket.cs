using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    // TODO: Fix lighting bug
    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transitioning }
    State state = State.Alive;

    public float thrustSpeed = 100f;
    [SerializeField] float rotationSpeed = 100f; // not accessible in other scripts, but accessabile in editor

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip landingSound;

    [SerializeField] ParticleSystem firePS;
    [SerializeField] ParticleSystem succesPS;
    [SerializeField] ParticleSystem crashPS;


    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
                SuccessSequence();
                break;
            default:
                failureSequence();
                break;
        }
    }

    private void failureSequence() {
        state = State.Dying;
        crashPS.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        Invoke("LoadFirstLevel", 1f);
    }

    private void SuccessSequence() {
        state = State.Transitioning;
        succesPS.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(landingSound);
        Invoke("LoadNextLevel", 1f);
    }

    private void LoadNextLevel() {
        state = State.Alive;
        audioSource.Stop();
        SceneManager.LoadScene(1);
    }

    private void LoadFirstLevel() {
        state = State.Alive;
        audioSource.Stop();
        SceneManager.LoadScene(0);
    }

    private void Thrust() {
        float thrustThisFrame = thrustSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space)) {
            ApplyThrust();
        } else {
            audioSource.Stop();
            firePS.Stop();
        }
    }

    private void ApplyThrust() {
        float thrustThisFrame = thrustSpeed * Time.deltaTime;

        if (firePS.isStopped) firePS.Play();

        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
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
