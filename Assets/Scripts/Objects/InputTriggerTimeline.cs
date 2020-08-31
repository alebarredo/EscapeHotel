using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[RequireComponent(typeof(Collider))]
public class InputTriggerTimeline : MonoBehaviour
{
    public enum TriggerType
    {
        Once, Everytime,
    }

    [Tooltip("This is the gameobject which will trigger the director to play.  For example, the player.")]
    public GameObject triggeringGameObject;
    public PlayableDirector director;
    public TriggerType triggerType;
    public UnityEvent OnDirectorPlay;
    public UnityEvent OnDirectorFinish;
    //public Animator m_Animator;
    public BoxCollider trigger;

    public Canvas instruction;

    bool hasPlayed = false;

    protected bool m_AlreadyTriggered;

    void OnTriggerStay(Collider other)
    {
        instruction.enabled = true;

        if (other.gameObject != triggeringGameObject)
            return;

        if (triggerType == TriggerType.Once && m_AlreadyTriggered)
            return;

        if (other.gameObject == triggeringGameObject && Input.GetKeyDown(KeyCode.E))
        {
            //m_Animator.SetBool("Open", true);
            OnDirectorPlay.Invoke();
            director.Play();
            hasPlayed = true;
            m_AlreadyTriggered = true;
            instruction.enabled = false;
        }

        if (director.state != PlayState.Playing && hasPlayed)
        {
            trigger.enabled = true;
            Invoke("FinishInvoke", (float)director.duration);
        }
        else trigger.enabled = false;

    }

    void OnTriggerExit(Collider other)
        {
        instruction.enabled = false;
        }

    void FinishInvoke()
    {
        OnDirectorFinish.Invoke();
    }

    void Update()
    {

    }
}