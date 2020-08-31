using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Door : MonoBehaviour
{

    public Animator anim;

    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            anim.SetBool("Open", false);
            audioSource.PlayOneShot(openSound);
        }
    }
    
    void OnTriggerEnter (Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            anim.SetBool("Open", true);
            audioSource.PlayOneShot(closeSound);
        }
    }
}

