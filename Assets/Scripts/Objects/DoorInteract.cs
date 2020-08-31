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

    public CinemachineVirtualCamera lockCam;

    public EventInteractable[] eventInteractables;
    private int oldMask;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        oldMask = Camera.main.cullingMask;
    }

    public void InteractDoor()
    {

        if (locked)
        {
            foreach (var eventInteractable in eventInteractables)
            {
                eventInteractable.tooltipMessage = lockedTooltip;
            }

            CamOn();

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

            CamOff();

        }
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    lockCam.Priority = 0;
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //    Camera.main.cullingMask = oldMask | (1 << 15);
        //}
    }

    void CamOn()
    {
        if (lockCam != null)
        {
            lockCam.Priority = 11;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Camera.main.cullingMask = oldMask & ~(1 << 15);
        }
    }

    void CamOff()
    {
        if (lockCam != null)
        {
            lockCam.Priority = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Camera.main.cullingMask = oldMask | (1 << 15);

        }
    }

    public void UnlockDoor(bool lockState)
    {
        locked = lockState;

        anim.SetBool("Open", true);
        audio.PlayOneShot(openSound);
        StartCoroutine(Close());

        CamOff();
    }

    public IEnumerator Close()
    {
        yield return new WaitForSeconds(timer);
        anim.SetBool("Open", false);
        audio.PlayOneShot(closeSound);
    }

    public IEnumerator OnTriggerExit(Collider c)
    {
        yield return new WaitForSeconds(timer);

        if (c.tag == "Player")
        {
            anim.SetBool("Open", false);
            audio.PlayOneShot(closeSound);
        }
    }
}