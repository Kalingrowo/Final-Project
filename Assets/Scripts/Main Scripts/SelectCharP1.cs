using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharP1 : MonoBehaviour {

	private int activeChar;

	//public GameObject btnConfirm;

	Animator anim;
	private Gameplay gameplay;

	// Use this for initialization
	void Start () {
		// activeChar 0 is equal to empty
		//activeChar = 0;
		anim = GetComponent<Animator> ();

	}

	// charIndex 0 is unused
	public void ActivateChar(int charIndex){
		anim.SetTrigger ("Reset");
		anim.SetInteger ("IDWizard", charIndex);
		PlayerPrefs.SetInt ("SetPlayer1", charIndex);
		print("Player 1 : " + PlayerPrefs.GetInt("SetPlayer1"));
	}

	public void DeactivateAllChars(){
		anim.SetTrigger ("Reset");
	}

	public void Attack(){
		anim.SetTrigger ("Attack");
	}
}
