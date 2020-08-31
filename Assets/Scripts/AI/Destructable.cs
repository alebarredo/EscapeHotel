using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Destructable : MonoBehaviour {

	[SerializeField]public float startingHealth;
	public float currentHealth;
	public float damageTaken;

	public event System.Action OnDeath;
	public event System.Action OnDamageReceived;
    public event System.Action OnReSpawn;

	void Awake (){
		// Set the initial health of the destructable.
		currentHealth = startingHealth;
	
	}

	public float HitPointsRemaining {
		get {
			return startingHealth - damageTaken;
		}
	}

	public bool IsAlive {
		get {
			return HitPointsRemaining > 0;
		}
	}

	public virtual void Die() {
		//if (!IsAlive)
		//	return;

		if (OnDeath != null)
			OnDeath ();
	}

	public virtual void TakeDamage(float amount) {
		damageTaken += amount;

		if (OnDamageReceived != null)
			OnDamageReceived ();

		if(HitPointsRemaining <=0) {
			Die ();
		}
	}

	public void Reset() {
		damageTaken = 0;
		currentHealth = startingHealth;
	    if (OnReSpawn != null)
	        OnReSpawn();
	}

	void Update (){
		currentHealth = startingHealth - damageTaken;
	}

}
