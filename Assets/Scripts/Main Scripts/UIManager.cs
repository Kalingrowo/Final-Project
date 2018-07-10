using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private GameObject btnPlay, btnSound, btnExit;
	private GameObject btnConfirm1, btnConfirm2;

	GameObject OnBGM, OffBGM;

	bool BGM = true;

	public GameObject gameManager;
	private Gameplay gameplay;

	private float waitTime;

	// Use this for initialization
	void Start () {

		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		if (gameManager != null) 
			gameplay = gameManager.GetComponent<Gameplay> ();

		if (SceneManager.GetActiveScene ().name == "Win 2" || SceneManager.GetActiveScene ().name == "Win 1")
			waitTime = 3f;
		else
			waitTime = 0f;
		
		/*
		//====
		if (GameObject.Find ("OnBGM") != null) {
			OnBGM = GameObject.Find ("OnBGM");
			Debug.Log ("OnBGM found !");
		}
		if (GameObject.Find ("OffBGM") != null) {
			OffBGM = GameObject.Find ("OffBGM");
			Debug.Log ("OffBGM found !");
			SetBGM ();
		}

		//====
		if (GameObject.Find ("btnPlay") != null) {
			btnPlay = GameObject.Find ("btnPlay");
			//btnPlay.SetActive (false);
		}
		if (GameObject.Find ("btnSound") != null) {
			btnSound = GameObject.Find ("btnSound");
		}
		if (GameObject.Find ("btnExit") != null) {
			btnExit = GameObject.Find ("btnExit");
		}

		//====
		if (GameObject.Find ("btnConfirm1") != null) {
			btnConfirm1 = GameObject.Find ("btnConfirm1");
		}
		if (GameObject.Find ("btnConfirm2") != null) {
			btnConfirm2 = GameObject.Find ("btnConfirm2");
		}
		*/
	}

	void FixedUpdate(){
		if (waitTime > -1f)
			waitTime -= Time.deltaTime;
		else if (waitTime <= -1f)
			waitTime = -1f;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (SceneManager.GetActiveScene ().name == "Main Menu") {
				SceneManager.LoadScene ("Select Char (Single) 1");
			} else if (SceneManager.GetActiveScene ().name == "Win 1" || SceneManager.GetActiveScene ().name == "Win 2" || SceneManager.GetActiveScene ().name == "Win Draw") {
				if (waitTime < 0f) {
					SceneManager.LoadScene ("Main Menu");
				}
			}
		}

		if (Input.GetKey (KeyCode.Escape)) {
			if (SceneManager.GetActiveScene ().name == "Main Menu") {
				Application.Quit ();
			} else {
				SceneManager.LoadScene ("Main Menu");
			}
		}
	}

	public void ConfirmCharP1(){
		SceneManager.LoadScene ("Select Char (Single) 2");
	}

	public void ConfirmCharP2(){
		SceneManager.LoadScene ("Game");
	}

	public void SetBGM(){
		BGM = !BGM;
		if (BGM == true) {
			OffBGM.gameObject.SetActive (false);
			OnBGM.gameObject.SetActive (true);
		}
		else{
			OnBGM.gameObject.SetActive (false);
			OffBGM.gameObject.SetActive (true);
		}
	}

	public void GoGame(){
		SceneManager.LoadScene ("Game");
	}

	public void GoMenu(){
		SceneManager.LoadScene ("Main Menu");
	}

}
