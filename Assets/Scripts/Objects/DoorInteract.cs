using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class DoorInteract : MonoBehaviour
{
    public Animator anim;
    public float timer = 3.0f;

    AudioSource audio;
    public AudioClip openSound;
    public AudioClip closeSound;

    public bool locked;
    public string unlockedTooltip = "open";
    public string lockedTooltip = "it's locked";

    public EventInteractable[] eventInteractables;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void InteractDoor()
    {
        if (locked)
        {
            foreach (var eventInteractable in eventInteractables)
            {
                eventInteractable.tooltipMessage = lockedTooltip;
            }
        }
        else
        {
            foreach (var eventInteractable in eventInteractables)
            {
                eventInteractable.tooltipMessage = unlockedTooltip;
            }
           
            anim.SetBool("Open", true);
            audio.PlayOneShot(openSound);
            StartCoroutine(Close());
        }
    }

    public void UnlockDoor(bool lockState)
    {
        locked = lockState;

        anim.SetBool("Open", true);
        audio.PlayOneShot(openSound);
        StartCoroutine(Close());
    }

    public IEnumerator Close()
    {
        yield return new WaitForSeconds(timer);
        anim.SetBool("Open", false);
        audio.PlayOneShot(closeSound);
    }

    //public IEnumerator OnTriggerExit(Collider c)
    //{
    //    yield return new WaitForSeconds(timer);

    //    if (c.tag == "Player")
    //    {
    //        anim.SetBool("Open", false);
    //        audio.PlayOneShot(closeSound);
    //    }
    //}
}