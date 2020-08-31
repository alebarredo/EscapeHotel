using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LaptopInteract : MonoBehaviour
{

    public CinemachineVirtualCamera laptopCam;

    public void InteractLaptop()
    {
        laptopCam.Priority = 11;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ExitLaptop()
    {
        laptopCam.Priority = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
