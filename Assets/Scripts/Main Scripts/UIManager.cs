using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public GameObject btnConfirm1, btnConfirm2, btnPlay;
	public GameObject panelMenu, panelSelectChar, panelGame, panelTentang;
	private int x;

	public GameObject GameManager;
	private Gameplay gameplay;

	// Use this for initialization
	void Start () {
		x = 50;
		if (GameManager != null) {
			gameplay = GameManager.GetComponent<Gameplay> ();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void ConfirmCharP1(){
		PlayerPrefs.SetString ("gameState", "S02");
		btnConfirm1.SetActive (false);
	}

	public void ConfirmCharP2(){
		PlayerPrefs.SetString ("gameState", "Play");
		btnConfirm2.SetActive (false);
		btnPlay.SetActive (true);
	}

	public void goTentang(){
		SceneManager.LoadScene ("Tentang");
	}

	public void goKeluar(){
		
	}

	public void goGame(){
		SceneManager.LoadScene ("Game");
	}

	public void goMulai(){
		SceneManager.LoadScene ("PilihKarakter");
	}

	public void goMenu(){
		SceneManager.LoadScene ("MainMenu");
	}
}
