using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour {
	// 0,0 - 0,x = Player 1
	// 1,0 - 1,x = Player 1
	// data ke 0 = id char		: 1 - 5 (ice, fire, dkk)
	// data ke 1 = curNegPoint	: 1 - 10 
	int[,] players;

	int numOfPlayer = 2;
	int playersData = 3;

	int scanCount = 0;

	float gameTime, scanTime;
	bool startScanTime = false;

	int curCardP1;
	int curCardP2;

	SelectCharP1 player1Script;
	SelectCharP2 player2Script;

	public Sprite[] answerSprites;

	public GameObject[] answer0;
	public Text txtQuiz, txtTest, txtCardsP1, txtCardsP2;
	public GameObject panelCharP1, panelCharP2, panelPointP1, panelPointP2;
	private GenerateQuiz generateQuiz;
	public GameObject[] txtAnswer;
	public Text txtScanTime, txtGameTime;

	public Button btnSkipAnswer;


	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		for (int i = 0; i < answer0.Length; i++) {
			answer0 [i].SetActive (false);
		}

		answerSprites = Resources.LoadAll<Sprite> ("Images/Lava Maps/Numbers");
		players = new int[numOfPlayer, playersData];
		generateQuiz = this.GetComponent<GenerateQuiz> ();

		player1Script = GameObject.Find ("Player 1").GetComponent<SelectCharP1> ();
		player2Script = GameObject.Find ("Player 2").GetComponent<SelectCharP2> ();

		InitializeGame ();
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

		txtScanTime.text = ((int)scanTime).ToString();
		txtGameTime.text = ((int)gameTime).ToString();
	}

	public void InitializeGame(){
		// test curcards temp
		SetPlayer (0, PlayerPrefs.GetInt("SetPlayer1"), 7);
		SetPlayer (1, PlayerPrefs.GetInt("SetPlayer2"), 7);
		// --

		PlayerPrefs.SetString ("gameState", "G01");
		// call prefab char and display player's char
		panelCharP1.SetActive(true);
		panelCharP2.SetActive(true);

		GenerateHUD ();
	}

	public void SetPlayer(int idPlayer, int idChar, int curCards){
		players [idPlayer, 0] = idChar;
		players [idPlayer, 1] = curCards;

		if (idPlayer == 0)
			player1Script.ActivateChar (idChar);
		else if (idPlayer == 1)
			player2Script.ActivateChar (idChar);
	}

	public void SetCurCards(int idPlayer, int point){
		int temp = players [idPlayer, 1];
		players [idPlayer, 1] = temp + point;
	}

	public int GetCurCards(int idPlayer){
		return (players [idPlayer, 1]);
	}
		
	public void SetQuiz(int idPlayer, int x){
		scanCount += 1;
		txtQuiz.text = generateQuiz.SetQuiz (x);

		GenerateHUD ();
	}

	public void SetAnswer(int idPlayer, int answer){
		scanCount += 1;
		int wrong = 0;
		int[] quizResult = new int[3];
		quizResult = generateQuiz.GetQuizResult();

		if (idPlayer == 0) {
			player1Script.Attack ();
		} else if (idPlayer == 1) {
			player2Script.Attack ();
		}

		for (int i = 0; i < 3; i++) {
			if (answer == quizResult [i]) {
				if (txtAnswer [i].GetComponent<Text> ().text == "x") {
					// answer true
					txtAnswer [i].GetComponent<Text> ().text = answer.ToString (); 
					answer0 [i].GetComponent<SpriteRenderer> ().sprite = answerSprites [answer];
					answer0 [i].gameObject.transform.localPosition = new Vector3 (0.5f, 0f, 0f);
					answer0 [i].SetActive (true);
					break;
				} else {
					// answer has been used
					wrong++;
				}
			} else {
				wrong++;
			}

		}

		if (wrong >= 2) {
			// wrong answer
			SetCurCards (idPlayer, 1);
			if (idPlayer == 0) {
				SetCurCards (1, -1);
			} else {
				SetCurCards (0, -1);
			}
		} else {
			SetCurCards (idPlayer, -1);
		}
		wrong = 0;

		GenerateHUD ();

	}

	public void SkipAnswer(){
		scanCount += 1;
		/*
		//Debug
		player1Script.Attack ();
		answer0 [0].gameObject.transform.localPosition = new Vector3 (0.4f, 0f, 0f);
		answer0 [0].SetActive (true);
		//-----
		*/
		GenerateHUD ();
	}

	public void GenerateHUD(){
		txtCardsP1.text = GetCurCards (0).ToString();
		txtCardsP2.text = GetCurCards (1).ToString();

		startScanTime = false;
		scanTime = 0f;
		StartCoroutine ("WaitX", 2f);

		if (scanCount == 1) {
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

			if (PlayerPrefs.GetString ("gameState") == "G01") {
				PlayerPrefs.SetString ("gameState", "G02");
				txtTest.text = "Player 2 turn \n (Generate Quiz)";
			} else if (PlayerPrefs.GetString ("gameState") == "G02") {
				PlayerPrefs.SetString ("gameState", "G01");
				txtTest.text = "Player 1 turn \n (Generate Quiz)";
			}
		}
	}

	public int GetScanCount(){
		return scanCount;
	}

	IEnumerator WaitX(float times){
		if (scanCount > 0 && scanCount < 3) {
			yield return new WaitForSeconds(times-1f);
			scanTime = 17f;
			startScanTime = true;
		} else if (scanCount >= 3) {
			yield return new WaitForSeconds(3f);
			scanCount = 0;
			for (int i = 0; i < txtAnswer.Length; i++) {
				txtAnswer [i].GetComponent<Text> ().text = "x";
				//answer0 [i].GetComponent<SpriteRenderer> ().sprite = answerSprites [0];
				answer0 [i].SetActive (false);
			}
		}
	}

}
