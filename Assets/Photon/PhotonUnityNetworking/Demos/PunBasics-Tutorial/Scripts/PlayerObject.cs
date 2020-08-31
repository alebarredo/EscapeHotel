using UnityEngine;
using Photon.Pun;

[System.Serializable]
public class PlayerObject : IPunObservable
{
	public GameObject playerPrefab;
	public int spawnId;
	public bool inRoom;

    #region IPunObservable implementation

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(inRoom);
            stream.SendNext(spawnId);
        }
        else
        {
            // Network player, receive data
            this.inRoom = (bool)stream.ReceiveNext();
            this.spawnId = (int)stream.ReceiveNext();
        }
    }

    #endregion
}
