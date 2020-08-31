using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventInteractable : InteractableBase
{
    public UnityEvent Interacted;

    public override void OnInteract()
    {
        base.OnInteract();
        Interacted.Invoke();
    }
}