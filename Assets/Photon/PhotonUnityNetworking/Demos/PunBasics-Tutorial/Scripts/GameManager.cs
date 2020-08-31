// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Launcher.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in "PUN Basic tutorial" to handle typical game management requirements
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

/// <summary>
/// Game manager.
/// Connects and watch Photon Status, Instantiate Player
/// Deals with quiting the room and the game
/// Deals with level loading (outside the in room synchronization)
/// </summary>
public class GameManager : MonoBehaviourPunCallbacks
{

	#region Public Fields

	static public GameManager Instance;

	#endregion

	#region Private Fields

	[Header("Players")]
	[Tooltip("The prefab to use for representing the player")]
	[SerializeField]
	public PlayerObject[] playerObjects;

	[Header("Spawn Points")]
	public Transform[] spawner;

	[Space, Header("Spawn Index")]
	public int spawnIndex = 0;

	#endregion

	#region MonoBehaviour CallBacks

	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity during initialization phase.
	/// </summary>
	void Start()
	{
		Instance = this;

		// in case we started this demo with the wrong scene being active, simply load the menu scene
		if (!PhotonNetwork.IsConnected)
		{
			SceneManager.LoadScene("Launcher");

			return;
		}

		//if (PhotonNetwork.IsMasterClient)
		//{
		//	PhotonNetwork.Instantiate(playerObjects[0].playerPrefab.name,
		//				   spawner[playerObjects[spawnIndex].spawnId].transform.position,
		//				   Quaternion.identity);

		//	playerObjects[0].inRoom = true;
		//	spawnIndex++;

		//	object[] datas = new object[] { spawnIndex };
		//	RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
		//	PhotonNetwork.RaiseEvent(CheckPlayersInRoomEvent, datas, raiseEventOptions, SendOptions.SendReliable);

		//}

  //      else
		//{
  //          PhotonNetwork.Instantiate(playerObjects[spawnIndex].playerPrefab.name,
		//		spawner[playerObjects[spawnIndex].spawnId].transform.position,
		//		Quaternion.identity);

  //          playerObjects[spawnIndex].inRoom = true;
  //      }

  //      Debug.LogFormat("Instantiating LocalPlayer in: {0} ", SceneManagerHelper.ActiveSceneName);
		//Debug.LogFormat("Current player count: {0}", PhotonNetwork.CountOfPlayersInRooms);
		//Debug.LogFormat("Current Spawn Index: " + spawnIndex);

	}

	public void SpawnPlayer(int playerId)
    {
        PhotonNetwork.Instantiate(playerObjects[playerId].playerPrefab.name,
                       spawner[playerId].transform.position,
                       Quaternion.identity);
    }

	/// <summary>
	/// MonoBehaviour method called on GameObject by Unity on every frame.
	/// </summary>
	void Update()
    {
        //// "back" button of phone equals "Escape". quit app if that's pressed
        //if (Input.GetMouseButtonDown(1))
        //{
        //    QuitApplication();
        //}
	}

	#endregion

	#region Photon Callbacks

	public const byte CheckPlayersInRoomEvent = 1;

	/// <summary>
	/// Called when a Photon Player got connected. We need to then load a bigger scene.
	/// </summary>
	/// <param name="other">Other.</param>

	//private void OnEnable()
	//{
	//	PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
	//}

 //   private void OnDisable()
	//{
	//	PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
	//}

	//private void OnEvent(EventData photonEvent)
	//{
	//	if (photonEvent.Code == CheckPlayersInRoomEvent)
	//	{
	//		object[] datas = (object[])photonEvent.CustomData;

	//		int spawnIndex = (int)datas[0];

	//		spawnIndex++;
	//	}
	//}

	public override void OnPlayerEnteredRoom( Player other  )
	{
		//spawnIndex++;

		//object[] datas = new object[] { spawnIndex };
		//RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All }; // You would have to set the Receivers to All in order to receive this event on the local client as well
		//PhotonNetwork.RaiseEvent(CheckPlayersInRoomEvent, datas, raiseEventOptions, SendOptions.SendReliable);

		Debug.Log( "NEW PLAYER JOINED: " + other.NickName); // not seen if you're the player connecting

		if ( PhotonNetwork.IsMasterClient )
		{
			Debug.LogFormat( "MASTER CLIENT CONNECTED", PhotonNetwork.IsMasterClient ); // called before OnPlayerLeftRoom
		}
	}

    /// <summary>
    /// Called when a Photon Player got disconnected. We need to load a smaller scene.
    /// </summary>
    /// <param name="other">Other.</param>
    public override void OnPlayerLeftRoom(Player other)
    {
		Debug.Log("A PLAYER LEFT: " + other.NickName); // seen when other disconnects
		PhotonNetwork.DestroyPlayerObjects(other);

		//if(other.NickName == playerObjects[0].playerPrefab.GetComponent<PhotonView>().name)
  //      {
		//	playerObjects[0].inRoom = false;
  //      }
  //      else if (other.NickName == playerObjects[1].playerPrefab.GetComponent<PhotonView>().name)
		//{
		//	playerObjects[1].inRoom = false;
		//}

		
	}

    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
	{
		SceneManager.LoadScene("Launcher");
	}

	#endregion

	#region Public Methods

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	public void QuitApplication()
	{
		Application.Quit();
	}

	#endregion

	#region Private Methods

	void LoadArena()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
		}

		Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);

		//PhotonNetwork.LoadLevel("PunBasics-Room for"+PhotonNetwork.CurrentRoom.PlayerCount);
		PhotonNetwork.LoadLevel("Escape");
	}

	#endregion

}