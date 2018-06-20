using UnityEngine;
using System.Collections;

public class HeightSync : MonoBehaviour {

	public GameObject PlatformObject;

	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		position.y = PlatformObject.transform.position.y +0.7f;	
		gameObject.transform.position = position;
	}
}
