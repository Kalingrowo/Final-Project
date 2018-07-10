using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour {
	int[,] players;
	int numOfPlayer = 2;
	int playersData = 3;
	int scanCount = 0;
	float gameTime, scanTime;
	bool endGame = false;
	bool startScanTime = false;

	private GenerateQuiz generateQuiz;

	public Sprite[] answerCapsules;
	public Sprite[] cardNumbers;

	public GameObject player1;
	public GameObject player2;
	public GameObject oprd1, oprt, oprd2;
	public GameObject[] answer;
	public Text txtQuiz, txtTest, txtCardsP1, txtCardsP2;
	public GameObject cardsP1, cardsP2;
	public GameObject btnGenerate;
	//public GameObject[] txtAnswer;
	public Text txtScanTime, txtGameTime;

	// Use this for initialization
	void Start () {
		// initiate game state
		PlayerPrefs.SetString ("gameState", "G01");
		endGame = false;

		// initiate
		players = new int[numOfPlayer, playersData];
		generateQuiz = this.GetComponent<GenerateQuiz> ();

		// set player char and cards
		SetPlayer (0, PlayerPrefs.GetInt("SetPlayer1"), 7);
		SetPlayer (1, PlayerPrefs.GetInt("SetPlayer2"), 7);

		// get sprite from folder
		answerCapsules = Resources.LoadAll<Sprite> ("Images/Capsules");
		cardNumbers = Resources.LoadAll<Sprite> ("Images/Numbers");

		GenerateHUD ();
		CleanGameSpace ();
	}

	void Update(){
		if (startScanTime) {
			if (scanTime > 0) {
				scanTime -= Time.deltaTime;
			}
			if (scanTime <= 0) {
				scanTime = 0f;
				SkipAnswer ();
			}
		}
		gameTime += Time.deltaTime;
		if (gameTime > 600f || GetCurCards(0) <= 0 || GetCurCards(1) <= 0 ) {
			endGame = true;
			EndCondition ();
		}
		Debug.Log ("Game Time : " + gameTime);			
	}

	public void SetPlayer(int idPlayer, int idChar, int curCards){
		players [idPlayer, 0] = idChar;
		players [idPlayer, 1] = curCards;

		if (idPlayer == 0) {
			player1.GetComponent<SelectCharP1> ().ActivateChar (idChar);
		} else if (idPlayer == 1){
			player2.GetComponent<SelectCharP2> ().ActivateChar (idChar);
		}
	}

	public void SetCurCards(int idPlayer, int cards){
		int temp = players [idPlayer, 1];
		players [idPlayer, 1] = temp + cards;
	}

	public int GetCurCards(int idPlayer){
		return (players [idPlayer, 1]);
	}
		
	public void SetQuiz(){
		int _Opr1, _Opr2;

		if (endGame == true) {
			EndGame ();
		} else {
			if (scanCount == 0) {
				scanCount += 1;
				txtQuiz.text = generateQuiz.SetQuiz (Random.Range (1, 9));
				_Opr1 = int.Parse (txtQuiz.text.Substring (0, 1));
				_Opr2 = int.Parse (txtQuiz.text.Substring (2, 1));
				Debug.Log ("ScanCount : " + scanCount);
				Debug.Log ("_Opr1 : " + _Opr1);
				Debug.Log ("_Opr2 : " + _Opr2);
				UpdateQuizNumb (_Opr1, _Opr2);

				GenerateHUD ();
			} else {
				SkipAnswer ();
			}
		}
	}

	public void UpdateQuizNumb(int _Opr1, int _Opr2){
		switch (_Opr1) {
		case 0:
			oprd1.GetComponent<SpriteRenderer> ().sprite = cardNumbers [0];
			break;
		case 1:
			oprd1.GetComponent<SpriteRenderer> ().sprite = cardNumbers [1];
			break;
		case 2:
			oprd1.GetComponent<SpriteRenderer> ().sprite = cardNumbers [2];
			break;
		case 3:
			oprd1.GetComponent<SpriteRenderer> ().sprite = cardNumbers [3];
			break;
		case 4:
			oprd1.GetComponent<SpriteRenderer> ().sprite = cardNumbers [4];
			break;
		case 5:
			oprd1.GetComponent<SpriteRenderer> ().sprite = cardNumbers [5];
			break;
		case 6:
			oprd1.GetComponent<SpriteRenderer> ().sprite = cardNumbers [6];
			break;
		case 7:
			oprd1.GetComponent<SpriteRenderer> ().sprite = cardNumbers [7];
			break;
		case 8:
			oprd1.GetComponent<SpriteRenderer> ().sprite = cardNumbers [8];
			break;
		case 9:
			oprd1.GetComponent<SpriteRenderer> ().sprite = cardNumbers [9];
			break;
		}

		switch (_Opr2) {
		case 0:
			oprd2.GetComponent<SpriteRenderer> ().sprite = cardNumbers [0];
			break;
		case 1:
			oprd2.GetComponent<SpriteRenderer> ().sprite = cardNumbers [1];
			break;
		case 2:
			oprd2.GetComponent<SpriteRenderer> ().sprite = cardNumbers [2];
			break;
		case 3:
			oprd2.GetComponent<SpriteRenderer> ().sprite = cardNumbers [3];
			break;
		case 4:
			oprd2.GetComponent<SpriteRenderer> ().sprite = cardNumbers [4];
			break;
		case 5:
			oprd2.GetComponent<SpriteRenderer> ().sprite = cardNumbers [5];
			break;
		case 6:
			oprd2.GetComponent<SpriteRenderer> ().sprite = cardNumbers [6];
			break;
		case 7:
			oprd2.GetComponent<SpriteRenderer> ().sprite = cardNumbers [7];
			break;
		case 8:
			oprd2.GetComponent<SpriteRenderer> ().sprite = cardNumbers [8];
			break;
		case 9:
			oprd2.GetComponent<SpriteRenderer> ().sprite = cardNumbers [9];
			break;
		}
	}

	public void SetTestAns(int x){
		SetAnswer (0, x);
	}

	public void SetAnswer(int idPlayer, int answerNumb){
		scanCount += 1;
		int wrong = 0;
		int[] quizResult = new int[3];
		quizResult = generateQuiz.GetQuizResult();

		if (idPlayer == 0) {
			player1.GetComponent<SelectCharP1>().Attack ();
		} else if (idPlayer == 1) {
			player2.GetComponent<SelectCharP2>().Attack ();
		}

		for (int i = 0; i < 3; i++) {
			if (answerNumb == quizResult [i]) {
				answer [i].GetComponent<SpriteRenderer> ().sprite = answerCapsules [answerNumb];
				SetCurCards (idPlayer, -1);
				break;
			} else {
				wrong++;
			}
		}

		// if wrong answer, player's current cards will increase
		// and another player's current cards will decrease
		if (wrong > 2) {
			SetCurCards (idPlayer, 1);
			if (idPlayer == 0) {
				SetCurCards (1, -1);
			} else {
				SetCurCards (0, -1);
			}
		} 
		wrong = 0;
		GenerateHUD ();
	}

	public void SkipAnswer(){
		scanCount += 1;
		GenerateHUD ();
	}

	public void GenerateHUD(){
		// update each player's cards
		SetCards (0);
		SetCards (1);

		// set a wait time to change state
		startScanTime = false;
		scanTime = 0f;
		StartCoroutine ("WaitX", 2f);

		if (scanCount == 1) {
			// change generate button to skip button
			btnGenerate.GetComponent<Image> ().sprite = btnGenerate.transform.GetChild (0).GetComponent<Image> ().sprite;
			if (PlayerPrefs.GetString ("gameState") == "G01") {
				txtTest.text = "Player 2 turn \n (Set Answer)";
			} else if (PlayerPrefs.GetString ("gameState") == "G02") {
				txtTest.text = "Player 1 turn \n (Set Answer)";
			}
		} else if (scanCount == 2) {
			if (PlayerPrefs.GetString ("gameState") == "G01") {
				txtTest.text = "Player 1 turn \n (Set Answer)";
			} else if (PlayerPrefs.GetString ("gameState") == "G02") {
				txtTest.text = "Player 2 turn \n (Set Answer)";
			}
		} else if (scanCount >= 3) {
			scanTime = 0f;
			// change skip button to generate button 
			btnGenerate.GetComponent<Image> ().sprite = btnGenerate.transform.GetChild (1).GetComponent<Image> ().sprite;

			if (PlayerPrefs.GetString ("gameState") == "G01") {
				PlayerPrefs.SetString ("gameState", "G02");
				txtTest.text = "Player 2 turn \n (Generate Quiz)";
			} else if (PlayerPrefs.GetString ("gameState") == "G02") {
				PlayerPrefs.SetString ("gameState", "G01");
				txtTest.text = "Player 1 turn \n (Generate Quiz)";
			}
		}
	}

	// update player's cards
	public void SetCards(int i){
		if (i == 0) {
			switch (GetCurCards (0)) {
			case 0:
				cardsP1.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [0];
				break;
			case 1:
				cardsP1.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [1];
				break;
			case 2:
				cardsP1.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [2];
				break;
			case 3:
				cardsP1.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [3];
				break;
			case 4:
				cardsP1.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [4];
				break;
			case 5:
				cardsP1.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [5];
				break;
			case 6:
				cardsP1.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [6];
				break;
			case 7:
				cardsP1.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [7];
				break;
			case 8:
				cardsP1.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [8];
				break;
			case 9:
				cardsP1.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [9];
				break;
			}
		} else if (i == 1) {
			switch (GetCurCards (1)) {
			case 0:
				cardsP2.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [0];
				break;
			case 1:
				cardsP2.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [1];
				break;
			case 2:
				cardsP2.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [2];
				break;
			case 3:
				cardsP2.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [3];
				break;
			case 4:
				cardsP2.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [4];
				break;
			case 5:
				cardsP2.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [5];
				break;
			case 6:
				cardsP2.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [6];
				break;
			case 7:
				cardsP2.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [7];
				break;
			case 8:
				cardsP2.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [8];
				break;
			case 9:
				cardsP2.gameObject.GetComponent<SpriteRenderer> ().sprite = cardNumbers [9];
				break;
			}
		}
	}

	public void CleanGameSpace(){
		scanCount = 0;
		UpdateQuizNumb (0, 0);
		for (int i = 0; i < answer.Length; i++) {
			Debug.Log (i);
			answer [i].GetComponent<SpriteRenderer> ().sprite = answerCapsules [10];
		}
	}

	public int GetScanCount(){
		return scanCount;
	}

	public void EndCondition(){
		// check who is the winner by the less current cards
		if (GetCurCards (0) < GetCurCards (1))
			PlayerPrefs.SetInt ("Winner", 0);
		else if (GetCurCards (1) < GetCurCards (0))
			PlayerPrefs.SetInt ("Winner", 1);
		else
			PlayerPrefs.SetInt ("Winner", 7);
		CleanGameSpace();
		btnGenerate.GetComponent<Image> ().sprite = btnGenerate.transform.GetChild (2).GetComponent<Image> ().sprite;
		btnGenerate.GetComponent<Button> ().interactable = true;
	}

	public void EndGame(){
		if(PlayerPrefs.GetInt("Winner") == 0)
			SceneManager.LoadScene ("Win 1");
		else if(PlayerPrefs.GetInt("Winner") == 1)
			SceneManager.LoadScene ("Win 2");
		else if(PlayerPrefs.GetInt("Winner") == 7)
			SceneManager.LoadScene ("Win Draw");
	}

	IEnumerator WaitX(float times){
		if (scanCount > 0 && scanCount < 3) {
			btnGenerate.GetComponent<Button> ().interactable = false;
			yield return new WaitForSeconds(times-1f);
			btnGenerate.GetComponent<Button> ().interactable = true;
			scanTime = 17f;
			startScanTime = true;
		} else if (scanCount >= 3) {
			btnGenerate.GetComponent<Button> ().interactable = false;
			yield return new WaitForSeconds(3f);
			btnGenerate.GetComponent<Button> ().interactable = true;

			// clean game space
			CleanGameSpace();
		}
	}

}
