using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class callAndroidPlugin : MonoBehaviour {

	public Text txtMenu, txtTittle;

	// Use this for initialization
	void Start () {
		TestOnCreate ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*
	public void MessageReceiver(string msg){
		txtTittle.text = msg;
	}
	*/

	public void TestOnCreate(){
		using (AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			using (AndroidJavaObject activity = javaClass.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				AndroidJavaObject JO = new AndroidJavaObject("com.nfc.kalingrowo.unityplugin.pluginClass");
				//JO.Call("getDataA", activity);
				txtMenu.text = JO.CallStatic<string> ("getDataB", activity);
			}
		}
	}
}
