using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]
public class EnemyIA : MonoBehaviour {

	// What to chase?
	public Transform target;

	// How many times ech secont we will update our path
	public float updateRate = 2f;

	private Seeker seeker;
	private Rigidbody2D rb;

	// The calculated path
	public Path path;

	// The AI's speed per second
	public float speed = 300f;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;

	// The max distant from the AI to a waypoint for it to continue to the next waypoint
	public float nextWaypointDistant = 3;

	// The waypoint we are currently moving towards
	private int currentWaypoint =  0;

	private bool searchingForPlayer = false;

	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}

			return;
		}
		//Start a new path to the target position, return the result to the OnPathComplet method
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());

	}

	IEnumerator SearchForPlayer (){
		
		GameObject sResult = GameObject.FindWithTag ("Player");
		if (sResult == null) {
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (SearchForPlayer ());
		} else {
			target = sResult.transform;
			searchingForPlayer = false;
			StartCoroutine (UpdatePath ());
			yield return false;
		}

	}

	IEnumerator UpdatePath () {

  if (target == null){
   if (!searchingForPlayer){
    searchingForPlayer = true;
    StartCoroutine (SearchForPlayer ());
   }
   yield return false;
  }

  else {
   seeker.StartPath (transform.position, target.position, OnPathComplete);
   yield return new WaitForSeconds(1f/updateRate); 
   StartCoroutine (UpdatePath ());
  }
 }﻿

	public void OnPathComplete(Path p){
		// Debug.Log ("We got a path. Did it have an error? " + p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	void FixedUpdate(){
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}

			return;
		}

		//TODO: Always look at player
		if (path == null) {
			return;
		}
		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded)
				return;
			// Debug.Log ("End of path reached.");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		//Move the IA
		rb.AddForce (dir, fMode);

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]);
		if (dist < nextWaypointDistant) {
			currentWaypoint++;
			return;
		}

	}
	// Update is called once per frame
	void Update () {
		
	}
}
