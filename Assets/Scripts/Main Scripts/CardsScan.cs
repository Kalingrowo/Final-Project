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

		gameplay = GameManager.GetComponent<Gameplay> ();

		/*
		using (AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject activity = javaClass.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				AndroidJavaObject JO = new AndroidJavaObject("com.nfc.kalingrowo.unityplugin.pluginClass");
				//JO.Call("getDataA", activity);
				//txtMenu.text = JO.CallStatic<string> ("getDataB", activity);
				//gameState = JO.Call<string> ("getDataB", activity);
			}
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		
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
					gameplay.setPlayer (0, 1, 5);
				} 
				if (result == "Fire Wizard") {
					charGOP1.GetComponent<SelectCharP1> ().ActivateChar (2);
					gameplay.setPlayer (0, 2, 5);
				} 
			}
				
		} else if (PlayerPrefs.GetString ("gameState") == "S02") {
			
			if (result != null) {
				txtCharPlayer2.gameObject.SetActive (false);
				charGOP2.GetComponent<SelectCharP2> ().DeactivateAllChars ();

				if (result == "Ice Wizard") {
					charGOP2.GetComponent<SelectCharP2> ().ActivateChar (1);
					gameplay.setPlayer (1, 1, 5);
				} 
				if (result == "Fire Wizard") {
					charGOP2.GetComponent<SelectCharP2> ().ActivateChar (2);
					gameplay.setPlayer (1, 2, 5);
				}	
			}
			 
		} else if (PlayerPrefs.GetString ("gameState") == "G01") {
			if (gameplay.getScanCount () == 0) {
				// player 1 scan quiz's reference
				gameplay.setQuiz (0, int.Parse(result));
			} else if (gameplay.getScanCount () == 1) {
				// player 2 input the answer
				gameplay.setAnswer(1, int.Parse(result));
			} else if (gameplay.getScanCount () == 2) {
				// player 1 input the answer
				gameplay.setAnswer(0, int.Parse(result));
			}

		} else if (PlayerPrefs.GetString ("gameState") == "G02") {
			if (gameplay.getScanCount () == 0) {
				// player 2 scan quiz's reference
				gameplay.setQuiz (1, int.Parse(result));
			} else if (gameplay.getScanCount () == 1) {
				// player 1 input the answer
				gameplay.setAnswer(0, int.Parse(result));
			} else if (gameplay.getScanCount () == 2) {
				// player 2 input the answer
				gameplay.setAnswer(1, int.Parse(result));
			}
		}
	}



}
