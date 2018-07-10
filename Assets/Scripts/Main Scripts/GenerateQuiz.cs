using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateQuiz : MonoBehaviour {

	int[] result = new int[3];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string SetQuiz(int x){
		int resultTemp;
		int randNumb = Random.Range (5, 9);
		//string quiz = x + " x " + randNumb + " = ";
		string quiz = x + "," + randNumb + ",";

		resultTemp = randNumb * x;
		if (resultTemp < 10) {
			result [0] = 10;
			result [1] = 0;
			result [2] = int.Parse(resultTemp.ToString () [0].ToString());
		} else if (resultTemp < 100) {
			result [0] = 10;
			result [1] = int.Parse(resultTemp.ToString () [0].ToString());
			result [2] = int.Parse(resultTemp.ToString () [1].ToString());
		} else if (resultTemp < 1000) {
			result [0] = int.Parse(resultTemp.ToString () [0].ToString());
			result [1] = int.Parse(resultTemp.ToString () [1].ToString());
			result [2] = int.Parse(resultTemp.ToString () [2].ToString());
		}

		return (quiz);
	}

	public int[] GetQuizResult(){
		return result;
	}
}
