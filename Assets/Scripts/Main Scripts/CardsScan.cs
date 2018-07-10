using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsScan : MonoBehaviour {

	public Text txtTittle, txtCharPlayer1, txtCharPlayer2;
	public GameObject GameManager;
	private Gameplay gameplay;

	public GameObject charGOP1, charGOP2;

	// Use this for initialization
	void Start () {
		// debug
		PlayerPrefs.SetString ("gameState", "G01");
		// --------------
		if(GameObject.Find("Game Manager")){
			gameplay = GameManager.GetComponent<Gameplay> ();
		}


	}

	// NFC callback
	void MessageReceiver (string result)
	{
		// Player 1 scan character card
		if (PlayerPrefs.GetString ("gameState") == "S01") {

			if (result != null) {
				charGOP1.GetComponent<SelectCharP1> ().DeactivateAllChars ();
				txtCharPlayer1.gameObject.SetActive (false);

				// change player 1's character
				if (result == "Ice Wizard") {
					charGOP1.GetComponent<SelectCharP1> ().ActivateChar (1);
					gameplay.SetPlayer (0, 1, 5);
				} 
				if (result == "Fire Wizard") {
					charGOP1.GetComponent<SelectCharP1> ().ActivateChar (2);
					gameplay.SetPlayer (0, 2, 5);
				} 
			}
				
		} else if (PlayerPrefs.GetString ("gameState") == "S02") {
			
			if (result != null) {
				txtCharPlayer2.gameObject.SetActive (false);
				charGOP2.GetComponent<SelectCharP2> ().DeactivateAllChars ();

				if (result == "Ice Wizard") {
					charGOP2.GetComponent<SelectCharP2> ().ActivateChar (1);
					gameplay.SetPlayer (1, 1, 5);
				} 
				if (result == "Fire Wizard") {
					charGOP2.GetComponent<SelectCharP2> ().ActivateChar (2);
					gameplay.SetPlayer (1, 2, 5);
				}	
			}
			 
		} else if (PlayerPrefs.GetString ("gameState") == "G01") {
			if (gameplay.GetScanCount () == 0) {
				// player 1 scan quiz's reference
				//gameplay.SetQuiz ();
			} else if (gameplay.GetScanCount () == 1) {
				// player 2 input the answer
				gameplay.SetAnswer(1, int.Parse(result));
			} else if (gameplay.GetScanCount () == 2) {
				// player 1 input the answer
				gameplay.SetAnswer(0, int.Parse(result));
			}

		} else if (PlayerPrefs.GetString ("gameState") == "G02") {
			if (gameplay.GetScanCount () == 0) {
				// player 2 scan quiz's reference
				//gameplay.SetQuiz ();
			} else if (gameplay.GetScanCount () == 1) {
				// player 1 input the answer
				gameplay.SetAnswer(0, int.Parse(result));
			} else if (gameplay.GetScanCount () == 2) {
				// player 2 input the answer
				gameplay.SetAnswer(1, int.Parse(result));
			}
		}
	}



}
