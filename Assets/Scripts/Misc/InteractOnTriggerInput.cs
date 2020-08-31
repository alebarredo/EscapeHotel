using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class InteractOnTriggerInput : MonoBehaviour {

    public LayerMask layers;
    public UnityEvent OnEnter, OnExit;
    new Collider collider;

    void OnTriggerStay(Collider other)
    {
        if (0 != (layers.value & 1 << other.gameObject.layer) && Input.GetKeyDown(KeyCode.E))
        {
            ExecuteOnEnter(other);
        }
    }

    protected virtual void ExecuteOnEnter(Collider other)
    {
        OnEnter.Invoke();
    }


    void OnTriggerExit(Collider other)
    {
        if (0 != (layers.value & 1 << other.gameObject.layer))
        {
            ExecuteOnExit(other);
        }
    }

    protected virtual void ExecuteOnExit(Collider other)
    {
        OnExit.Invoke();
    }
}