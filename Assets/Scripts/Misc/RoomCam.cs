using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCam : MonoBehaviour {

    public GameObject vCam;

    void Awake()
    {
        vCam.SetActive(false);
    }
	
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
                vCam.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
    if (other.gameObject.tag == "Player")
   
        {
        vCam.SetActive(false);   
        }
    }
}
