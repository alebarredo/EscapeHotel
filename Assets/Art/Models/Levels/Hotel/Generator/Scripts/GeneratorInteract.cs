using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GeneratorInteract : MonoBehaviour
{

    public CinemachineVirtualCamera generatorCam;

    public void InteractGenerator()
    {
        generatorCam.enabled = true;
    }

    public void ExitGenerator()
    {
        generatorCam.enabled = false;
    }

}
