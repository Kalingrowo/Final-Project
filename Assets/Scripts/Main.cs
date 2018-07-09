using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Main : MonoBehaviour
{

	string qrString = "";
	bool background = true;

	public Text txtTitle, txtMenu;

	void Start(){
		AndroidNFCReader.enableBackgroundScan ();
		AndroidNFCReader.ScanNFC (gameObject.name, "OnFinishScan");
	}

	void Update(){
		
	}

	// NFC callback
	void OnFinishScan (string result)
	{
		// Cancelled
		if (result == AndroidNFCReader.CANCELLED) {
			txtMenu.text = "Cancelled !";
		
			// Error
		} else if (result == AndroidNFCReader.ERROR) {
			txtMenu.text = "Error !";

			// No hardware
		} else if (result == AndroidNFCReader.NO_HARDWARE) {
			txtMenu.text = "No Hardware !";
		} else {
			txtMenu.text = result;
		}

		qrString = getToyxFromUrl (result);
	}

	// Extract toyxId from url
	string getToyxFromUrl (string url)
	{		
		int index = url.LastIndexOf ('/') + 1;
		
		if (url.Length > index) {
			return url.Substring (index);		
		} 
		
		return url;
	}

	public void CekDebug(string x){
		txtTitle.text = x;
	}

}
