using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class PoolCam : MonoBehaviour {

    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;

    public GameObject poolCam;

    void Start()
    {
        poolCam.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        poolCam.SetActive(true);

        if (other.gameObject.tag == "Player")
        {
            foreach (PlayableDirector playableDirector in playableDirectors)
            {
                playableDirectors[0].Play();
            }

        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (PlayableDirector playableDirector in playableDirectors)
            {
                playableDirectors[0].Stop();
            }
        }

        poolCam.SetActive(false);
    }
}
