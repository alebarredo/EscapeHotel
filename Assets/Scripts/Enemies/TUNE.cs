using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;


[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(NavMeshAgent))]

public class TUNE : MonoBehaviour {


	[SerializeField]Scanner playerScanner;

	//[SerializeField]Tune settings;

	public NavMeshAgent nav;
	public FirstPersonController[] players;               // Reference to the player's position.
	Pathfinder pathFinder;

    FirstPersonController priorityTarget;
    List<FirstPersonController> myTargets;

    public event System.Action<FirstPersonController> OnTargetSelected;

    //bool trespassing = false;
    //public PlayableDirector spottedTimeline;

    private EnemyHealth m_EnemyHealth;
	public EnemyHealth EnemyHealth
	{
		get
		{
			if (m_EnemyHealth == null)
				m_EnemyHealth = GetComponent<EnemyHealth> ();
			return m_EnemyHealth;
		}
	}

    private EnemyState m_EnemyState;
    public EnemyState EnemyState
    {
        get
        {
            if (m_EnemyState == null)
                m_EnemyState = GetComponent<EnemyState>();
            return m_EnemyState;
        }
    }

    void Start () 
	{
        //players = new Transform[players.length];

        //players = GameObject.FindGameObjectsWithTag("Player").;
        nav = GetComponent <NavMeshAgent> ();
		pathFinder = GetComponent<Pathfinder> ();
		//pathFinder.Agent.speed = settings.RunSpeed;

		playerScanner.OnScanReady += Scanner_OnScanReady;
		Scanner_OnScanReady ();
	}
	
	public void Scanner_OnScanReady () 
	{
		if (priorityTarget != null)
			return;

        myTargets = playerScanner.ScanForTargets<FirstPersonController> ();

        if(myTargets.Count == 1)
			priorityTarget = myTargets[0];
        else
            SelectClosestTarget();

        if (priorityTarget != null)
            SetDestinationToPriorityTarget();


        //if (priorityTarget != null && trespassing)
        //    //SetDestinationToPriorityTarget();
        //    Spotted();

    }

    public void SetDestinationToPriorityTarget()
    {
        pathFinder.SetTarget(priorityTarget.transform.position);
    }

    //public void Spotted()
    //{
    //    spottedTimeline.Play();
    //}

    public void SelectClosestTarget()
    {
        float closestTarget = playerScanner.ScanRange;
        foreach (var possibleTarget in myTargets)
        {
            if (Vector3.Distance(transform.position, possibleTarget.transform.position) < closestTarget)
                priorityTarget = possibleTarget;
        }
    }

    public void Update()
	{
		if (priorityTarget == null)
			return;

        if (!EnemyHealth.IsAlive)
            return;

        //nav.SetDestination(players[0].position);

    }
}
