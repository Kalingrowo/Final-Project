using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharP2 : MonoBehaviour {

	private List<GameObject> charList;
	private int activeChar;

	public GameObject btnConfirm;

	// Use this for initialization
	void Start () {

		// activeChar 0 is equal to empty
		activeChar = 0;

		// character list for selecting character
		charList = new List<GameObject> ();
		foreach (Transform t in transform) {
			charList.Add (t.gameObject);
			t.gameObject.SetActive (false);
		}
			
		// Debug
		ActivateChar (3);
		// ---------------------
	}

	// charIndex 0 is unused
	public void ActivateChar(int charIndex){
		if (activeChar != charIndex) {
			charList [charIndex - 1].SetActive (true);
			activeChar = charIndex;

			btnConfirm.SetActive (true);
		}
	}

	public void DeactivateAllChars(){
		foreach (Transform t in transform) {
			t.gameObject.SetActive (false);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
