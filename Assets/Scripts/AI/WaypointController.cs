﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour {
	
	public Waypoint[] waypoints; 

	int currentWaypointIndex = -1;
	public event System.Action<Waypoint> OnWaypointChanged;

	
	// Update is called once per frame
	void Awake () 
	{
		waypoints = GetWaypoints ();
	}

	public void SetNextWaypoint () 
	{
		currentWaypointIndex++;

		if (currentWaypointIndex == waypoints.Length)
			currentWaypointIndex = 0;
			
		if(OnWaypointChanged != null)
			OnWaypointChanged(waypoints[currentWaypointIndex]);
	}
	
	Waypoint[] GetWaypoints()
	{
		return GetComponentsInChildren<Waypoint> ();
	}

	void OnDrawGizmos () 
	{
		Gizmos.color = Color.blue;

		Vector3 previousWaypoint = Vector3.zero;
		foreach(var waypoint in GetWaypoints())
		{
			Vector3 waypointPosition = waypoint.transform.position;
			Gizmos.DrawWireSphere(waypoint.transform.position, .2f);
			if (previousWaypoint != Vector3.zero)
				Gizmos.DrawLine (previousWaypoint, waypointPosition);
			previousWaypoint = waypointPosition;
		}
	}
}
