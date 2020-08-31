using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PortToCheckpoint : MonoBehaviour {

    public GameObject Elvis;

    public void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "Player")
                Elvis.transform.position = this.transform.position;
    }

    public void Translate()
    {
        Elvis.transform.position = this.transform.position;
    }

}
