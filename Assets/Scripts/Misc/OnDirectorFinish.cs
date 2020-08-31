using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;


public class OnDirectorFinish : MonoBehaviour {

    public UnityEvent OnEnd;
    public PlayableDirector director;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (director.state != PlayState.Playing)
        {
            OnEnd.Invoke();
        }
    }
}
