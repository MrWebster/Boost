using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// prevents this script from acting on the same component twice, if it was attached twice.
[DisallowMultipleComponent]

public class BlockMover : MonoBehaviour {

    [SerializeField] Vector3 movementVector;

    [Range(0, 1)] [SerializeField] float movementFactor;

    Vector3 startingPos;
    // Transform obsticalTransform;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
	}
}
