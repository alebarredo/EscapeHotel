using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : Destructable {

	[SerializeField] SpawnPoint[] spawnPoints;

	[SerializeField] Slider healthSlider;                                 // Reference to the UI's health bar.
	static bool isDead;                                                // Whether the player is dead.

	Animator anim;                                              // Reference to the Animator component.

	//public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	//AudioSource playerAudio;                                    // Reference to the AudioSource component.

	bool damaged;  									// True when the player gets damaged.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.

	void Awake ()
	{
		currentHealth = startingHealth;
		healthSlider.value = startingHealth;
		anim = GetComponentInChildren <Animator> ();

		//playerAudio = GetComponent <AudioSource> ();

	}

	void Update ()
	{
		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;

	}


	public override void TakeDamage (float amount)
	{

		// Set the damaged flag so the screen will flash.
		damaged = true;

		// Reduce the current health by the damage amount.
		currentHealth -= amount;

		// Increase damagetaken amount.
		damageTaken += amount;

		// Set the health bar's value to the current health.
		healthSlider.value = currentHealth;

		// Play the hurt sound effect.
		//playerAudio.Play ();

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if(currentHealth <= 0 && !IsAlive)
		{
			// ... it should die.
			Die ();
		}
	}

	public override void Die () 
	{
		isDead = true;

		base.Die ();
		anim.SetTrigger ("Dead");

		// Turn off any remaining shooting effects.
		//playerShooting.DisableEffects ();

		// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();

		StartCoroutine ("WaitAndRespawn");
	}


	IEnumerator WaitAndRespawn() {
		yield return new WaitForSeconds(3.0f);
		Reset ();
        healthSlider.value = startingHealth;
        SpawnAtNewSpawnpoint();
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	void SpawnAtNewSpawnpoint(){

		anim.SetTrigger ("Respawn");

		int spawnIndex = Random.Range (0, spawnPoints.Length - 1);
		transform.position = spawnPoints [spawnIndex].transform.position;
		transform.rotation = spawnPoints [spawnIndex].transform.rotation;
	}

}
