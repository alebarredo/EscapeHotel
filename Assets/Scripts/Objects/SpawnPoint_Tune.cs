using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint_Tune : MonoBehaviour {

	void OnDrawGizmos () {
		Gizmos.color = Color.magenta;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireCube (Vector3.zero + Vector3.up * 1, Vector3.one + Vector3.up * 1);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
