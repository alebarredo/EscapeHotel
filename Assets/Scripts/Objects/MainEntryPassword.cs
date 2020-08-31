using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class MainEntryPassword : MonoBehaviour {


    public TMP_InputField inputPassword1;
    public TMP_InputField inputPassword2;

    public string password1 = "flower";
    public string password2 = "power";

    public Animator windowHigh;
    public Animator windowLow;

    public UnityEvent Granted, Denied;

    public GameObject player;

    public void Password1Correct()
    {
        if(inputPassword1.text == password1)
        {
            windowHigh.SetBool("Open", true);
        }
    }

    public void Password2Correct()
    {
        if (inputPassword1.text == password1)
        {
            windowLow.SetBool("Open", true);
        }
    }

    public void CheckPassword() {
        if (inputPassword1.text == password1 && inputPassword2.text == password2)
        {
            EntryGranted();
        }
        else if (inputPassword1.text != password1 && inputPassword2.text != password2)
        {
            EntryDenied();
        }
	}

    public void EntryGranted()
    {
        Granted.Invoke();
        player.transform.Translate(new Vector3(0, 0, -2) * Time.deltaTime, Space.World);
    }

    public void EntryDenied()
    {
        Denied.Invoke();
        player.transform.Translate(new Vector3(0, 0, 2) * Time.deltaTime, Space.World);
    }
}
