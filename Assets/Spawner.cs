using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviourPun
{
    GameManager gameManager;
    public int spawnId;

    // Start is called before the first frame update
    void Awake()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    return;
        //}

        if (!photonView.IsMine)
        {
            return;
        }

        //// set same spawnId as playerObject - if the network works fine this id should match the player objects spawnId
        //gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        //spawnId = gameManager.playerObjects[gameManager.spawnIndex].spawnId;

        //// translate player to its spawn position index by 1
        //gameObject.transform.position = gameManager.spawner[spawnId].transform.position;
        //print("Spawning at Hotel Room: " + spawnId);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawner"))
        {
            gameObject.transform.position = gameManager.spawner[spawnId].transform.position;
        }
    }
}
