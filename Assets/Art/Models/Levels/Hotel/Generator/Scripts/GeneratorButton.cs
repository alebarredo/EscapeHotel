using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorButton : MonoBehaviour
{
    //GeneratorButtonManager generatorButtonManager;
    //GeneratorButtonReset generatorButtonReset;

    public bool isPressed;

    public Animator animator;

    public Material pressedMaterial;
    public Material neutralMaterial;

    public AudioSource audioSource;

    Renderer rend;

    ButtonPanelManager buttonPanelManager;

    void Awake()
    {
        isPressed = false;

        buttonPanelManager = GameObject.Find("ButtonPanelManager").GetComponent<ButtonPanelManager>();

        animator = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
    }

    void OnMouseDown()
    {
        if (!isPressed)
        {
            audioSource.Play();
            PressButton();
        }
    }

    void PressButton()
    {
        animator.SetBool("Pressed", true);
        rend.material = pressedMaterial;
        buttonPanelManager.pressed++;
        isPressed = true;
    }

    public void ResetButton()
    {
        animator.SetBool("Pressed", false);
        rend.material = neutralMaterial;
        isPressed = false;
    }
}
