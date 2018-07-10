using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharP2 : MonoBehaviour {

	//public GameObject btnConfirm;

	Animator anim;

	// Use this for initialization
	void Start () {

		// activeChar 0 is equal to empty
		//activeChar = 0;
		anim = GetComponent<Animator> ();

	}

	// charIndex 0 is unused
	public void ActivateChar(int charIndex){
		// re-declaration anim to give acces from gameplay script
		anim = GetComponent<Animator> ();

		anim.SetTrigger ("Reset");
		anim.SetInteger ("IDWizard", charIndex);
		PlayerPrefs.SetInt ("SetPlayer2", charIndex);
	}

	public void DeactivateAllChars(){
		anim.SetTrigger ("Reset");
	}

	public void Attack(){
		anim.SetTrigger ("Attack");
	}
}
