using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GeneratorButtonLid : MonoBehaviour
{

    Animator animator;
    AudioSource audioSource;
    public CinemachineVirtualCamera generatorCam;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenLid()
    {
        generatorCam.enabled = true;
        StartCoroutine(OpenCamera());

    }

    public IEnumerator OpenCamera()
    {
        yield return new WaitForSeconds(2.0f);
        animator.SetBool("Open", true);
        audioSource.Play();
        StartCoroutine(GeneratorOpened());
    }

    public IEnumerator GeneratorOpened()
    {
        yield return new WaitForSeconds(1.0f);
        generatorCam.enabled = false;
    }

    public void CloseLid()
    {
        animator.SetBool("Open", false);
        audioSource.Play();
    }
}
