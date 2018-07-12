using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioSource controlledAudioSource;
	public AudioClip[] audioClips;
	public bool loop = true;

	public static MusicManager musicInstance;

	private int lastScene, curScene;

	private float timer;
	private float curClipLength;
	private int iterator = 0;
	private bool playlistEnded = false;

	// Use this for initialization
	void Awake () {
		lastScene = SceneManager.GetActiveScene ().buildIndex ;
		InstatiateCheck ();
	}

	private void InstatiateCheck(){
		if (musicInstance == null) {
			musicInstance = this;
			DontDestroyOnLoad (this);
		} else if(this != musicInstance) {
			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		curScene = SceneManager.GetActiveScene ().buildIndex;

		if (lastScene != curScene) {
			
			if (curScene < 3) {
				iterator = 0;
				if (curScene == 0)
					PlayCurClips ();
			} else if (curScene > 3) {
				iterator = 1;
				PlayCurClips ();
			} else if (curScene == 3){
				iterator = 2;
				PlayCurClips ();
			}
			lastScene = curScene;

		}

		if (audioClips.Length > 0 && !playlistEnded) {
			timer += Time.deltaTime;
			if (timer > curClipLength) {
				if (iterator + 1 == audioClips.Length) {
					if (loop) {
						iterator = 2;
					} else {
						controlledAudioSource.Stop ();
						playlistEnded = true;
						return;
					}
				} else {
					if (iterator < 2) {
						iterator = iterator;
					} else {
						iterator++;
					}
				}
				PlayCurClips();
			}
		}
	}

	void PlayCurClips(){
		controlledAudioSource.Stop ();
		controlledAudioSource.clip = audioClips [iterator];
		curClipLength = audioClips [iterator].length;
		timer = 0;
		controlledAudioSource.Play ();
		Debug.Log ("It : " + iterator);
		Debug.Log ("Tim : " + timer);
	}

	void PlayFromIndex(int index){
		if (iterator + 1 <= audioClips.Length) {
			iterator = index;
			PlayCurClips ();
			playlistEnded = false;
		} else {
			Debug.Log ("Index " + index + "is out of the audio clip range");
		}
	}

}