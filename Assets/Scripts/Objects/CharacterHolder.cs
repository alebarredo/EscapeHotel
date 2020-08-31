using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHolder : MonoBehaviour {

	// Use this for initialization
    void OnTriggerEnter (Collider collider) {
        collider.transform.parent = gameObject.transform;
	}
	
	// Update is called once per frame
    void OnTriggerExit (Collider collider) {
        collider.transform.parent = null;
	}
}
