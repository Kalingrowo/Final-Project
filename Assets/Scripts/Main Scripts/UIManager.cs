using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private GameObject btnPlay, btnSound, btnExit;
	private GameObject btnConfirm1, btnConfirm2;

	GameObject OnBGM, OffBGM;

	bool BGM = true;

	public GameObject GameManager;
	private Gameplay gameplay;

	// Use this for initialization
	void Start () {

		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		if (GameManager != null) {
			gameplay = GameManager.GetComponent<Gameplay> ();
		}

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

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (SceneManager.GetActiveScene ().name == "Main Menu") {
				SceneManager.LoadScene ("Select Char (Single) 1");
			} 
		}

		if (Input.GetKey (KeyCode.Escape))
			Application.Quit ();
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

	public void GoKeluar(){
		
	}

	public void GoGame(){
		SceneManager.LoadScene ("Game");
	}

	public void GoMulai(){
		SceneManager.LoadScene ("Select Char");
	}

	public void GoMenu(){
		SceneManager.LoadScene ("Main Menu");
	}

}
