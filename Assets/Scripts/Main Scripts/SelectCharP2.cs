using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharP2 : MonoBehaviour {

	private List<GameObject> charList;
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
		PlayerPrefs.SetInt ("SetPlayer2", charIndex);
		print("Player 2 : " + PlayerPrefs.GetInt("SetPlayer2"));

	}

	public void DeactivateAllChars(){
		anim.SetTrigger ("Reset");

	}

	public void Attack(){
		anim.SetTrigger ("Attack");
	}
}
