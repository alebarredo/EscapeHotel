using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(TUNE))]

public class TUNE_Animation : MonoBehaviour {

	[SerializeField] Animator animator;

	Vector3 lastPosition;

	Pathfinder pathFinder;
	TUNE enemyPlayer;

	void Awake () 
	{
        pathFinder = GetComponentInParent<Pathfinder> ();
        enemyPlayer = GetComponentInParent<TUNE> ();
	}

	void Update () 
	{
		float velocity = (transform.position - lastPosition).magnitude / Time.deltaTime;
		lastPosition = transform.position;
		animator.SetBool ("IsWalking", enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.AWARE);
		animator.SetBool("IsSprinting", enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.UNAWARE);
		animator.SetFloat("Vertical", velocity / 3);

		if (animator.GetFloat("Vertical") == 0)
		{
			animator.SetBool("IsWalking", false);
			animator.SetBool("IsSprinting", false);

		}
	}
}
