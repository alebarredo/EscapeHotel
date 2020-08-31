using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DoorKeyPress : MonoBehaviour
{

    public Animator anim;
    public float timer = 3.0f;
    public Canvas instruction;

    AudioSource audio;
    public AudioClip openSound;
    public AudioClip closeSound;

    private void Start()
    {
        instruction.enabled = false;
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            instruction.enabled = true;
        }
    }

    void OnTriggerStay(Collider c)
    {
    
        if (c.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            instruction.enabled = false;
            anim.SetBool("Open", true);
            audio.PlayOneShot(openSound);

        }

    }


    IEnumerator OnTriggerExit(Collider c)
    {
        instruction.enabled = false;

        yield return new WaitForSeconds(timer);

        if (c.gameObject.tag == "Player")
        {
            anim.SetBool("Open", false);
            audio.PlayOneShot(closeSound);
        }
    }

}