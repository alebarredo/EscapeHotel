using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMaterialSwitch : MonoBehaviour {

    public Material openDoorMaterial;

    public MeshRenderer renderer;

    void Start () {
    
    }
	
    void OnEnable () {

        renderer.material = openDoorMaterial;
	}
}
