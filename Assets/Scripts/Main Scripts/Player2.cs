using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.SetInteger ("IDWizard", 3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Attack(){
		anim.SetTrigger ("Attack");
	}
}
