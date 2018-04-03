﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// prevents this script from acting on the same component twice, if it was attached twice.
[DisallowMultipleComponent]

public class BlockMover : MonoBehaviour {

    [SerializeField] Vector3 movementVector;
    // Protect against period being 0 or NaN
    [SerializeField] float period = 2f;

    [Range(0, 1)] [SerializeField] float movementFactor;

    Vector3 startingPos;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        // print(rawSinWave);
        movementFactor = rawSinWave / 2f + 0.5f;

        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
	}
}
