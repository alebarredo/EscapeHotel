using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace VHS
{
    public class Dispenser : InteractableBase, IPunObservable
    {
        public GameObject dispensable;
        public Transform dispenser;
        public float randomForce;
        ////public AudioClip[] soundFX;
        ////private AudioSource audioSource;

        public override void OnInteract()
        {
            base.OnInteract();

            GameObject dispensableInstance = PhotonNetwork.Instantiate(dispensable.name, dispenser.transform.position, dispenser.transform.rotation);
            dispensableInstance.GetComponent<Rigidbody>().AddForce(Random.Range(0, randomForce), Random.Range(0, randomForce), Random.Range(0, randomForce));

        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                Vector3 pos = transform.localPosition;
                stream.Serialize(ref pos);
            }
            else
            {
                Vector3 pos = Vector3.zero;
                stream.Serialize(ref pos);  // pos gets filled-in. must be used somewhere
            }
        }

        //void RandomAudio()
        //{
        //    if (audio.isPlaying)
        //    {
        //        return;
        //    }
        //    audio.clip = soundToPlay[Random.Range(0, soundToPlay.Length)];
        //    audio.Play();

        //}
    }
}
