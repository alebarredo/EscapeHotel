using UnityEngine;
using UnityEngine.Events;

public class TimelineActivation : MonoBehaviour {

    public UnityEvent OnEnd;

    void OnEnable()
    {
        OnEnd.Invoke();
        print("EndInvoked");
    }
}