using System.Collections;
using ProgressBar;
using UnityEngine;

public class SetProgress : MonoBehaviour {

	ProgressRadialBehaviour radialBehaviour;
	[SerializeField] float updateDelay = 1f;
	float val;
	private bool startCount = false;

	public void StartCount(float x){
		val = 0f;
		startCount = true;
		StartCoroutine ("Count", x);
	}

	public void StopCount(){
		val = 0f;
		startCount = false;
	}

	IEnumerator Count(int param){
		radialBehaviour = GetComponent<ProgressRadialBehaviour> ();
		while (true) {
			yield return new WaitForSeconds (updateDelay);
			radialBehaviour.Value = (val / param) * 100;
			if (radialBehaviour.isDone) {
				startCount = false;
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (startCount == true) {
			val += Time.deltaTime;
		}
	}
}
