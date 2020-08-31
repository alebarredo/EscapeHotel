using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;


public class SkipDialogue : MonoBehaviour {

    public PlayableDirector director;

    public UnityEvent SkipTalk;

    public Canvas skipCanvas;

    void Update()
    {
        if (director.state == PlayState.Playing && Input.GetKeyDown(KeyCode.Space))
        {
            SkipTalk.Invoke();
            skipCanvas.enabled = false;
        }
    }

}