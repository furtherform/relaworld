using UnityEngine;
using System.Collections;

public class AdaptationOnMarkerScript : MonoBehaviour {
	int AdaptationOn = 1;


	// Use this for initialization
	void Start () {
	
		if (PlayerPrefs.HasKey ("AdaptationOnStored")) 
		{ AdaptationOn = PlayerPrefs.GetInt ("AdaptationOnStored");		
			Debug.Log(AdaptationOn + "Adaptation from save noted at marker Script");
		}
		if (AdaptationOn == 1) {
			gameObject.SetActive(false);
		} else gameObject.SetActive(true);



	}
	
	// Update is called once per frame
	void Update () {
		if (AdaptationOn == 1) {
			gameObject.SetActive(false);
		} else
			gameObject.SetActive(true);


	
	}
}
