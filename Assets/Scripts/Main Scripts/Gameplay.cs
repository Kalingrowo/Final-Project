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

	int curCardP1;
	int curCardP2;

	public Text txtQuiz, txtTest, txtCardsP1, txtCardsP2;
	public GameObject panelCharP1, panelCharP2, panelPointP1, panelPointP2;
	private GenerateQuiz generateQuiz;
	public GameObject[] txtAnswer;

	public Button btnSkipAnswer;


	// Use this for initialization
	void Start () {

		players = new int[numOfPlayer, playersData];
		generateQuiz = this.GetComponent<GenerateQuiz> ();

		initializeGame ();
	}

	public void initializeGame(){
		// test curcards temp
		setPlayer (0, 1, 5);
		setPlayer (1, 1, 5);
		// --

		PlayerPrefs.SetString ("gameState", "G01");
		// call prefab char and display player's char
		panelCharP1.SetActive(true);
		panelCharP2.SetActive(true);

		generateHUD ();
	}

	public void setPlayer(int idPlayer, int idChar, int curCards){
		players [idPlayer, 0] = idChar;
		players [idPlayer, 1] = curCards;
	}

	public void setCurCards(int idPlayer, int point){
		int temp = players [idPlayer, 1];
		players [idPlayer, 1] = temp + point;
	}

	public int getCurCards(int idPlayer){
		return (players [idPlayer, 1]);
	}
		
	public void setQuiz(int idPlayer, int x){
		scanCount += 1;
		txtQuiz.text = generateQuiz.setQuiz (x);

		generateHUD ();
	}

	public void setAnswer(int idPlayer, int answer){
		scanCount += 1;
		int wrong = 0;
		int[] quizResult = new int[3];
		quizResult = generateQuiz.getQuizResult();

		for (int i = 0; i < 3; i++) {
			if (answer == quizResult [i]) {
				if (txtAnswer [i].GetComponent<Text> ().text == "x") {
					// answer true
					txtAnswer [i].GetComponent<Text> ().text = answer.ToString (); 
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
			setCurCards (idPlayer, 1);
			if (idPlayer == 0) {
				setCurCards (1, -1);
			} else {
				setCurCards (0, -1);
			}
		} else {
			setCurCards (idPlayer, -1);
		}
		wrong = 0;

		generateHUD ();

	}

	public void skipAnswer(){
		scanCount += 1;
		generateHUD ();
	}

	public void generateHUD(){

		txtCardsP1.text = getCurCards (0).ToString();
		txtCardsP2.text = getCurCards (1).ToString();

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
			scanCount = 0;

			for (int i = 0; i < txtAnswer.Length; i++) {
				txtAnswer [i].GetComponent<Text> ().text = "x";
			}

			if (PlayerPrefs.GetString ("gameState") == "G01") {
				PlayerPrefs.SetString ("gameState", "G02");
				txtTest.text = "Player 2 turn \n (Generate Quiz)";
			} else if (PlayerPrefs.GetString ("gameState") == "G02") {
				PlayerPrefs.SetString ("gameState", "G01");
				txtTest.text = "Player 1 turn \n (Generate Quiz)";
			}
		}
	}

	public int getScanCount(){
		return scanCount;
	}
}
