using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class ConsoleInteract : MonoBehaviour
{
    public Animator canvasAnimator;
    public TextMeshProUGUI textMessage;
    public string unlocked;
    public string locked;
    public float waitTime;

    public bool consoleUnlocked;
    //public bool consoleLocked;

    Collider collider;

    public CinemachineVirtualCamera consoleCam;

    private void Awake()
    {
        collider = gameObject.GetComponent<Collider>();
    }

    public void InteractConsole()
    {
        consoleCam.Priority = 11;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        collider.enabled = false;

        StartCoroutine(Authenticate());
    }

    public IEnumerator Authenticate()
    {
        canvasAnimator.SetBool("Startup", true);
        canvasAnimator.SetBool("Idle", false);

        yield return new WaitForSeconds(waitTime);


        if (consoleUnlocked)
        {
            canvasAnimator.SetBool("Granted", true);
            canvasAnimator.SetBool("Startup", false);
            textMessage.text = unlocked;
        }
        else
        {
            canvasAnimator.SetBool("Denied", true);
            canvasAnimator.SetBool("Startup", false);
            textMessage.text = locked;

            StartCoroutine(ExitConsole());
        }
    }

    public void Exit()
    {
        StartCoroutine(ExitConsole());
    }

    public IEnumerator ExitConsole()
    {
        yield return new WaitForSeconds(waitTime / 2);

        canvasAnimator.SetBool("Idle", true);
        canvasAnimator.SetBool("Startup", false);
        canvasAnimator.SetBool("Granted", false);
        canvasAnimator.SetBool("Denied", false);

        consoleCam.Priority = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        collider.enabled = true;
    }

    public void UnlockConsole()
    {
        consoleUnlocked = true;
    }
}
