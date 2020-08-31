using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(TUNE))]
[RequireComponent(typeof(EnemyHealth))]

public class TUNE_Patrol : MonoBehaviour {

	[SerializeField] WaypointController waypointController;

	[SerializeField] float waitTimeMin;

	[SerializeField] float waitTimeMax;

	Pathfinder pathFinder;

	public Timer timer;

	private TUNE m_EnemyPlayer;
	public TUNE EnemyPlayer
	{
		get
		{
			if (m_EnemyPlayer == null)
				m_EnemyPlayer = GetComponent<TUNE> ();
			return m_EnemyPlayer;
		}
	}


	void Start () 
	{
		waypointController.SetNextWaypoint ();

	}

	void Awake () 
	{
		pathFinder = GetComponent<Pathfinder> ();

        //	EnemyPlayer.EnemyHealth.OnDeath += EnemyHealth_OnDeath;
        EnemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;

	}

    public void EnemyPlayer_OnTargetSelected (FirstPersonController obj)
	{
        pathFinder.Agent.isStopped = true;
    }

    void EnemyHealth_OnDeath()
    {
        pathFinder.Agent.isStopped = true;
    }

    void OnEnable ()
	{
		pathFinder.OnDestinationReached += PathFinder_OnDestinationReached;
		waypointController.OnWaypointChanged += WaypointController_OnWaypointChanged;

        waypointController.SetNextWaypoint();
    }


    void OnDisable ()
	{
		pathFinder.OnDestinationReached += PathFinder_OnDestinationReached;
		waypointController.OnWaypointChanged += WaypointController_OnWaypointChanged;
	}

	private void WaypointController_OnWaypointChanged (Waypoint waypoint)
	{
		pathFinder.SetTarget (waypoint.transform.position);
		//print("tune patrol - on waypoint changed");
	}

	private void PathFinder_OnDestinationReached ()
	{
		timer.Add (waypointController.SetNextWaypoint, Random.Range (waitTimeMin, waitTimeMax));
		//print("tune patrol - on waypoint reached");


	}
}
