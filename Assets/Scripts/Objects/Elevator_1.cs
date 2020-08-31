using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class Elevator_1 : MonoBehaviour {

    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;

    public GameObject vCam1;
    public GameObject vCam2;
    public GameObject trigger;

    //public GameObject cinemachineBrain;

    public GameObject blackBG;

    void Start()
    {
        vCam1.SetActive(false);
        vCam2.SetActive(false);
        blackBG.SetActive(false);
        //cinemachineBrain.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {

        vCam1.SetActive(true);
        vCam2.SetActive(true);
        blackBG.SetActive(true);
        //cinemachineBrain.SetActive(true);

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.DownArrow))
        {
            foreach (PlayableDirector playableDirector in playableDirectors)
            {
                playableDirectors[0].Play();
            }

        }

    }

    void Update()
    {
        if (playableDirectors[0].state != PlayState.Playing)
        {
            trigger.SetActive(true);
        }
        else trigger.SetActive(false);
    }

}
