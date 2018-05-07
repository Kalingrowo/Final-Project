using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour {

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.SetInteger ("IDWizard", 2);
	}

	public void Attack(){
		anim.SetTrigger ("Attack");
	}
}
