using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attacking : MonoBehaviour {


	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	public int attackDamage = 2;               // The amount of health taken away per attack.


	Animator anim;                              // Reference to the animator component.
	public GameObject player;                          // Reference to the player GameObject.
	public PlayerHealth playerHealth;                  // Reference to the player's health.
	EnemyHealth enemyHealth;                    // Reference to this enemy's health.
	bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer; 							// Timer for counting up to the next attack.


	void Awake ()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponentInChildren <PlayerHealth> ();
		enemyHealth = GetComponent<EnemyHealth>();
		anim = GetComponentInChildren <Animator> ();
	}


	void OnTriggerEnter (Collider other)
	{
		// If the entering collider is the player...
		if(other.gameObject == player)
		{
			// ... the player is in range.
			playerInRange = true;
			print ("in range");
			anim.SetBool ("Attack", true);

		}
	}


	void OnTriggerExit (Collider other)
	{
		// If the exiting collider is the player...
		if(other.gameObject == player)
		{
			// ... the player is no longer in range.
			playerInRange = false;
			anim.SetBool ("Attack", false);
		}
	}

	void Attack ()
	{

		if (!anim.GetBool ("Attack"))
			anim.SetFloat ("AttackType", Random.Range (0, 1));
		// Reset the timer.
		anim.SetBool ("Attack", true);

		timer = 0f;

		// If the player has health to lose...
		if(playerHealth.currentHealth > 0)
		{
			// ... damage the player.
			playerHealth.TakeDamage (attackDamage);
			print ("making some damage");
		}
	}

	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;

		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
		{
			// ... attack.
			Attack ();
			print ("triggered attack");
		}

		// If the player has zero or less health...
		if(playerHealth.currentHealth <= 0)
		{
			// ... tell the animator the player is dead.
			anim.SetBool ("Attack", false);

			anim.SetTrigger ("PlayerDead");
		}
	}

}
