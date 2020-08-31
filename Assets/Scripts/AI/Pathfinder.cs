using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pathfinder : MonoBehaviour {

	public NavMeshAgent Agent;
	public EnemyHealth enemyHealth;

	[SerializeField]float distanceRemainingTreshold;

	bool m_destinationReached;
	bool destinationReached
	{
		get {return m_destinationReached;}
		set
		{
			m_destinationReached = value;
			if(m_destinationReached)
			{
				if(OnDestinationReached!= null)
					OnDestinationReached();
			}
		}
	}

	public event System.Action OnDestinationReached;

	// Use this for initialization
	void Awake () 
	{
		Agent = GetComponent<NavMeshAgent> ();
		enemyHealth = GetComponent<EnemyHealth> ();
	}
	
	public void SetTarget (Vector3 target) 
	{
		if (!enemyHealth.IsAlive)
			return;

		Agent.SetDestination (target);
		destinationReached = false;
		//print("pathfinder - set target");
	}

	void Update () 
	{
		if (destinationReached || !Agent.hasPath)
			return;

		if (Agent.remainingDistance < distanceRemainingTreshold)
			destinationReached = true;
	}
}
