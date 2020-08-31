using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;


public class OnEnableDirectorFinish : MonoBehaviour {

    public UnityEvent OnEnd;

    public int timelineDuration;

    void OnEnable()
    {
        StartCoroutine("End");
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(timelineDuration);
        OnEnd.Invoke();
    }

}