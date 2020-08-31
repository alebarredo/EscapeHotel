using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonPanelManager : MonoBehaviour
{
    [Space, Header("Panel 1")]
    public GeneratorCode Panel1Code;
    public GeneratorButton[] Panel1;
    private Animator[] Panel1Animators;
    public int[] Panel1Digits;

    [Space, Header("Panel 2")]
    public GeneratorCode Panel2Code;
    public GeneratorButton[] Panel2;
    private Animator[] Panel2Animators;
    public int[] Panel2Digits;

    [Space, Header("Panel 3")]
    public GeneratorCode Panel3Code;
    public GeneratorButton[] Panel3;
    private Animator[] Panel3Animators;
    public int[] Panel3Digits;

    [Space]
    public int pressed;

    [Space]
    public UnityEvent Unlock;

    [Space]
    bool poolUnlocked = false;
    public AudioClip unlocked1;
    AudioSource audioSource;

    //// Start is called before the first frame update
    void Start()
    {
        RandomizeCode();

        Panel1Animators = new Animator[Panel1.Length];
        Panel2Animators = new Animator[Panel2.Length];
        Panel3Animators = new Animator[Panel3.Length];

        audioSource = GetComponent<AudioSource>();

        SetButtons();

        Panel1Code.SetCode(Panel1Digits[0], Panel1Digits[1], Panel1Digits[2]);
        Panel2Code.SetCode(Panel2Digits[0], Panel2Digits[1], Panel2Digits[2]);
        Panel3Code.SetCode(Panel3Digits[0], Panel3Digits[1], Panel3Digits[2]);
    }

    void RandomizeCode()
    {
        Panel1Digits[0] = Random.Range(0, 8);
        Panel1Digits[1] = Random.Range(0, 8);
        Panel1Digits[2] = Random.Range(0, 8);

        Panel2Digits[0] = Random.Range(0, 8);
        Panel2Digits[1] = Random.Range(0, 8);
        Panel2Digits[2] = Random.Range(0, 8);

        Panel3Digits[0] = Random.Range(0, 8);
        Panel3Digits[1] = Random.Range(0, 8);
        Panel3Digits[2] = Random.Range(0, 8);
    }

    void Update()
    {
        if (pressed == 9 && poolUnlocked == false)
        {
            Validate();
        } 
       
        else if (pressed > 9)
        {
            for (int i = 0; i < Panel1.Length; i++)
            {
                ResetButtons();
            }

            for (int i = 0; i < Panel2.Length; i++)
            {
                ResetButtons();
            }

            for (int i = 0; i < Panel3.Length; i++)
            {
                ResetButtons();
            }

            pressed = 0;
        }

    }

    public void Validate()
    {
        if (Panel1Animators[Panel1Digits[0]].GetBool("Pressed") == true
        && Panel1Animators[Panel1Digits[1]].GetBool("Pressed") == true
        && Panel1Animators[Panel1Digits[2]].GetBool("Pressed") == true

        && Panel2Animators[Panel2Digits[0]].GetBool("Pressed") == true
        && Panel2Animators[Panel2Digits[1]].GetBool("Pressed") == true
        && Panel2Animators[Panel2Digits[2]].GetBool("Pressed") == true

        && Panel3Animators[Panel3Digits[0]].GetBool("Pressed") == true
        && Panel3Animators[Panel3Digits[1]].GetBool("Pressed") == true
        && Panel3Animators[Panel3Digits[2]].GetBool("Pressed") == true)

        {
            Unlocked1();
            poolUnlocked = true;
        } 
    }

    public void ResetButtons()
    {
        foreach (var button in Panel1)
        {
            button.ResetButton();
        }

        foreach (var button in Panel2)
        {
            button.ResetButton();
        }

        foreach (var button in Panel3)
        {
            button.ResetButton();
        }

        audioSource.Play();
    }

    public void SetButtons()
    {
        for (int i = 0; i < Panel1.Length; i++)
        {
            Panel1Animators[i] = Panel1[i].GetComponent<Animator>();
            Panel1Animators[i].SetBool("Pressed", false);
        }

        for (int i = 0; i < Panel2.Length; i++)
        {
            Panel2Animators[i] = Panel2[i].GetComponent<Animator>();
            Panel2Animators[i].SetBool("Pressed", false);
        }

        for (int i = 0; i < Panel3.Length; i++)
        {
            Panel3Animators[i] = Panel3[i].GetComponent<Animator>();
            Panel3Animators[i].SetBool("Pressed", false);
        }
    }

    public void Unlocked1()
    {
        print("TRIGGER-1");
        //audioSource.PlayOneShot(unlocked1, 1);
        Unlock.Invoke();

        poolUnlocked = false;

    }

    //public void Unlocked2()
    //{
    //    print("TRIGGER-2");
    //    Event2.Invoke();
    //}
}
