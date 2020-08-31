using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;


public class OnInputDirectorFinish : MonoBehaviour {

    public UnityEvent OnEnd;
    public PlayableDirector director;
    bool hasPlayed = false;

    public int timelineDuration;

    void OnTriggerStay (Collider other) 
    {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine("End");
        }
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(timelineDuration);
        OnEnd.Invoke();
    }

}