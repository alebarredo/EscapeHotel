using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollSecondary : MonoBehaviour {

	public float ScrollX = 0.5f;
	public float ScrollY = 0.5f;

	Material materials;
	public Renderer rend;


	void Start()
	{
		rend = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update () {
		float OffsetX = Time.time * ScrollX;
		float OffsetY = Time.time * ScrollY;
		rend.materials[1].mainTextureOffset = new Vector2 (OffsetX, OffsetY);

	}
}
