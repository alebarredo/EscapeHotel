using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using Cinemachine;

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

	public Menu menu;
	public Canvas roomCodeCanvas;
	public Canvas roomSelectCanvas;
	public CinemachineVirtualCamera laptopCam;

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

	}

	public void SpawnPlayer(int playerId)
    {
        PhotonNetwork.Instantiate(playerObjects[playerId].playerPrefab.name,
                       spawner[playerId].transform.position,
                       Quaternion.identity);
    }

	public void ResetPlayer()
    {
   //     foreach (var player in playerObjects)
   //     {
			//if (player = PhotonNetwork.LocalPlayer.IsLocal)
   //         {
			//	player.transform.position = spawner[Random.Range(0, spawner.Length)].transform.position;
			//	menu.Pause();
			//}
   //     }		
	}

	public void ChangePlayer()
    {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		PhotonNetwork.Destroy(player);
		menu.Pause();

		laptopCam.Priority = 11;
		roomCodeCanvas.enabled = false;
		roomSelectCanvas.enabled = true;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;

	}

	#endregion

	#region Photon Callbacks

	public override void OnPlayerEnteredRoom( Player other  )
	{
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