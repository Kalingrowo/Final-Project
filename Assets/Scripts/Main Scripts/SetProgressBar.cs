using System.Collections;
using ProgressBar;
using UnityEngine;

public class SetProgressBar : MonoBehaviour {

	ProgressBarBehaviour barBehaviour;
	[SerializeField] float updateDelay = 1f;
	float val;
	private bool startCount = false;

	public GameObject gameManager;

	public void StartCount(float x){
		val = 0f;
		startCount = true;
		StartCoroutine ("Count", Mathf.RoundToInt(x));
	}

	public void StopCount(){
		val = 0f;
		startCount = false;
	}

	IEnumerator Count(int param){
		barBehaviour = GetComponent<ProgressBarBehaviour> ();
		while (true) {
			yield return new WaitForSeconds (updateDelay);
			barBehaviour.Value = (val / param) * 100;
			if (barBehaviour.isDone) {
				startCount = false;
				gameManager.GetComponent<Gameplay> ().EndCondition ();
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (startCount == true) {
			val += Time.deltaTime;
		}
		Debug.Log ("---" + val.ToString ());
	}
}
