using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPoints : MonoBehaviourPunCallbacks, IPunObservable
{
    [Header("Spawn Points")]
    public Transform[] spawner;

    [Space, Header("Spawn Index")]
    public int spawnIndex = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(spawnIndex);
        }
        else
        {
            // Network player, receive data
            this.spawnIndex = (int)stream.ReceiveNext();
        }
    }

    #endregion
}
