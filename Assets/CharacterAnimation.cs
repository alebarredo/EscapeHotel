using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterAnimation : MonoBehaviourPun
{
	public Animator animator;
	FirstPersonController fps;

	void Awake()
	{
		fps = GetComponent<FirstPersonController>();
	}

	// Update is called once per frame
	void Update()
	{
		if(animator == null)
        {
			return;
        }

		if (!photonView.IsMine)
		{
			return;
		}

		animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
		animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
		animator.SetBool("IsSprinting", fps.movementInputData.IsRunning);
        animator.SetBool("IsCrouched", fps.movementInputData.IsCrouching);
    }
}
