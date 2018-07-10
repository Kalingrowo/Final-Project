using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

	[SerializeField]
	private Transform[] waypoints;

	Animator anim;

	private bool touched;

	[SerializeField]
	private float moveSpeed = 2f;

	private int waypointIndex = 0;

	private void Start(){

		anim = this.gameObject.GetComponent<Animator> ();
		this.transform.position = waypoints [waypointIndex].transform.position;
		touched = false;
		waypointIndex = 0;
	}

	private void Update(){
		Move ();
	}

	private void Move(){
		if (waypointIndex <= waypoints.Length - 1) {
			this.transform.position = Vector2.MoveTowards (this.transform.position, 
				waypoints [waypointIndex].transform.position, moveSpeed * Time.deltaTime);
			
			if (Vector2.Distance(this.transform.position, waypoints [waypointIndex].transform.position) < 0.5f) {
				waypointIndex += 1;
				if (waypointIndex == 2)
					moveSpeed = 4f;
				else if (waypointIndex == 4)
					anim.SetBool ("Shrink", true);
			}

		}
	}

	private void EndAnimation(){
		anim.SetBool ("Shrink", false);
		this.gameObject.SetActive (false);
		this.gameObject.transform.localScale = new Vector3 (40f, 40f, 1f);
		this.transform.position = waypoints [0].transform.position;
		waypointIndex = 0;
	}

}
