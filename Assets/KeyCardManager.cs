using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCardManager : MonoBehaviour
{
    private string GreenId = "Green";
    public bool Green;

    private string YellowId = "Yellow";
    public bool Yellow;

    private string RedId = "Red";
    public bool Red;

    // Start is called before the first frame update
    public void UpdateCredentials(string Id)
    {
        if(Id == GreenId)
        {
            Green = true;
        }
        else if (Id == YellowId)
        {
            Yellow = true;
        }
        else if (Id == RedId)
        {
            Red = true;
        }

        CheckConsoleState();

        print("credentials updated");
    }

    public void CheckConsoleState()
    {
        if(Green && Yellow && Red)
        {
            ConsoleInteract console = GetComponentInParent<ConsoleInteract>();
            console.UnlockConsole();
        }
    }
}
