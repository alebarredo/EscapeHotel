using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoiceMessage : MonoBehaviour {

    Collider messageButton;
    public UnityEvent messagePlayed;

	// Use this for initialization
	void Start () {
        messageButton = GameObject.Find("Message").GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == messageButton && Input.GetMouseButtonDown(0))
                {
                    messagePlayed.Invoke();
                }
            }
        }
    }
}
